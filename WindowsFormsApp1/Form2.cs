using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private TcpListener videoListener;
        private volatile bool isVideoRunning = true;

        private const int STABLE_FRAMES = 15;   // số khung liên tiếp để coi là ổn định
        private string candidate = "";
        private int candidateCount = 0;
        private string lastLogged = "";
        private string[] slotShape = new string[9];   // null = ô trống
        private Button[] slotButtons = new Button[9];

        public Form2()
        {
            InitializeComponent();
            picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            // Tìm 9 nút ô kho theo tên btnSlot01..btnSlot09
            for (int i = 0; i < 9; i++)
            {
                Control[] found = this.Controls.Find("btnSlot" + (i + 1).ToString("00"), true);
                if (found.Length > 0) slotButtons[i] = found[0] as Button;
            }

            ResetInfo();
            StartVideoServer();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Apply UI theme
            try
            {
                ThemeManager.ApplyThemeToForm(this);
            }
            catch { }

            // Restore slot button visuals based on current slotShape array (if any)
            for (int i = 0; i < slotButtons.Length; i++)
            {
                var b = slotButtons[i];
                if (b == null) continue;
                if (!string.IsNullOrEmpty(slotShape[i]))
                {
                    b.BackColor = ShapeColor(slotShape[i]);
                    b.ForeColor = Color.White;
                    b.Text = (i + 1).ToString("00") + "\n" + slotShape[i];
                }
                else
                {
                    b.BackColor = ThemeManager.SlotEmpty;
                    b.ForeColor = Color.Black;
                    b.Text = (i + 1).ToString("00");
                }
            }

            // initial control states
            try { btnStop.Enabled = false; } catch { }
        }

        // =========================================
        // XỬ LÝ THÔNG TIN PHÔI
        // =========================================
        private static string Pretty(string s)
        {
            switch (s)
            {
                case "Hinh tron": return "Tròn";
                case "Tam giac": return "Tam giác";
                case "Hinh vuong": return "Vuông";
                case "Hinh chu nhat": return "Chữ nhật";
                default: return "";
            }
        }

        private static Color ShapeColor(string p)
        {
            switch (p)
            {
                case "Tròn": return Color.FromArgb(33, 150, 243);
                case "Vuông": return Color.FromArgb(46, 160, 67);
                case "Tam giác": return Color.FromArgb(255, 152, 0);
                default: return Color.FromArgb(142, 68, 173);
            }
        }

        private void ResetInfo()
        {
            lblObjectType.Text = "Loại phôi: ---";
            lblConfidence.Text = "Độ tin cậy: ---";
            lblTarget.Text = "Vị trí kho: ---";
        }

        // Gọi mỗi khung hình nhận được (chạy trên luồng UI)
        private void ProcessShape(string shape)
        {
            if (shape == candidate) candidateCount++;
            else { candidate = shape; candidateCount = 1; }

            if (candidate == "")
            {
                if (candidateCount >= STABLE_FRAMES)
                {
                    lastLogged = "";   // bàn trống -> sẵn sàng phôi mới
                    ResetInfo();
                }
                return;
            }

            string pretty = Pretty(candidate);
            lblObjectType.Text = "Loại phôi: " + pretty;
            lblConfidence.Text = "Độ tin cậy: " + Math.Min(100, candidateCount * 100 / STABLE_FRAMES) + " %";

            // Ổn định đủ số khung -> ghi nhận phôi, gán ô kho trống đầu tiên
            if (candidateCount == STABLE_FRAMES && candidate != lastLogged)
            {
                lastLogged = candidate;
                int slot = Array.FindIndex(slotShape, x => x == null);
                if (slot >= 0)
                {
                    slotShape[slot] = pretty;
                    Button b = slotButtons[slot];
                    if (b != null)
                    {
                        b.BackColor = ShapeColor(pretty);
                        b.ForeColor = Color.White;
                        b.Text = (slot + 1).ToString("00") + "\n" + pretty;
                    }
                    lblTarget.Text = "Vị trí kho: " + (slot + 1).ToString("00");
                }
                else
                {
                    lblTarget.Text = "Vị trí kho: Đầy";
                }
            }
        }

        // =========================================
        // TCP SERVER NHẬN CAMERA (PORT 8889)
        // Gói tin: [4 byte size ảnh][2 byte size meta][meta][ảnh JPEG] (big-endian)
        // =========================================
        private static bool ReadExactly(NetworkStream stream, byte[] buffer, int count)
        {
            int total = 0;
            while (total < count)
            {
                int n = stream.Read(buffer, total, count - total);
                if (n == 0) return false;
                total += n;
            }
            return true;
        }

        private void StartVideoServer()
        {
            Task.Run(() =>
            {
                try
                {
                    videoListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8889);
                    videoListener.Start();

                    while (isVideoRunning)
                    {
                        using (TcpClient client = videoListener.AcceptTcpClient())
                        using (NetworkStream stream = client.GetStream())
                        {
                            byte[] header = new byte[6];
                            byte[] metaBuf = new byte[256];
                            byte[] imageBuffer = new byte[0];

                            while (isVideoRunning)
                            {
                                if (!ReadExactly(stream, header, 6)) break;
                                int imageSize = (header[0] << 24) | (header[1] << 16) | (header[2] << 8) | header[3];
                                int metaLen = (header[4] << 8) | header[5];
                                if (imageSize <= 0 || imageSize > 10000000 || metaLen > 256) break;

                                string shape = "";
                                if (metaLen > 0)
                                {
                                    if (!ReadExactly(stream, metaBuf, metaLen)) break;
                                    shape = Encoding.UTF8.GetString(metaBuf, 0, metaLen);
                                }

                                if (imageBuffer.Length < imageSize) imageBuffer = new byte[imageSize];
                                if (!ReadExactly(stream, imageBuffer, imageSize)) break;

                                Bitmap bmpCopy;
                                using (var ms = new MemoryStream(imageBuffer, 0, imageSize))
                                using (var bmp = new Bitmap(ms))
                                {
                                    bmpCopy = new Bitmap(bmp);
                                }

                                if (IsDisposed || !isVideoRunning)
                                {
                                    bmpCopy.Dispose();
                                    break;
                                }

                                try
                                {
                                    string s = shape;
                                    this.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        var old = picCamera.Image;
                                        picCamera.Image = bmpCopy;
                                        if (old != null) old.Dispose();
                                        ProcessShape(s);
                                    }));
                                }
                                catch (InvalidOperationException)
                                {
                                    bmpCopy.Dispose();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (SocketException) { }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Video server loi: " + ex);
                }
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isVideoRunning = false;
            try { if (videoListener != null) videoListener.Stop(); } catch { }
            base.OnFormClosing(e);
        }

        // =========================================
        // CÁC HÀM SỰ KIỆN NÚT BẤM (giữ nguyên để Designer không lỗi)
        // =========================================
        private void btnConnect_Click(object sender, EventArgs e) { }
        private void btnStop_Click(object sender, EventArgs e) { }
        private void btnStart_Click(object sender, EventArgs e) { }
        private void btnHome_Click(object sender, EventArgs e) { }
        private void btnReset_Click(object sender, EventArgs e) { }
        private void btnSlot01_Click(object sender, EventArgs e) { }
        private void btnSlot02_Click(object sender, EventArgs e) { }
        private void btnSlot03_Click(object sender, EventArgs e) { }
        private void btnSlot04_Click(object sender, EventArgs e) { }
        private void btnSlot05_Click(object sender, EventArgs e) { }
        private void btnSlot06_Click(object sender, EventArgs e) { }
        private void btnSlot07_Click(object sender, EventArgs e) { }
        private void btnSlot08_Click(object sender, EventArgs e) { }
        private void btnSlot09_Click(object sender, EventArgs e) { }

        private void picCamera_Click(object sender, EventArgs e) { }
    }
}