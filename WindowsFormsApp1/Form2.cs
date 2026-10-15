using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private TcpListener videoListener;
        private volatile bool isVideoRunning = true;

        public Form2()
        {
            InitializeComponent();
            picCamera.SizeMode = PictureBoxSizeMode.Zoom; // vừa khung, giữ tỉ lệ
            StartVideoServer();
        }

        private void Form2_Load(object sender, EventArgs e) { }

        // =========================================
        // TCP SERVER NHẬN LUỒNG CAMERA (PORT 8889)
        // =========================================

        // Đọc đủ count byte, trả về false nếu kết nối bị đóng
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
                        // Chờ Python kết nối đến
                        using (TcpClient client = videoListener.AcceptTcpClient())
                        using (NetworkStream stream = client.GetStream())
                        {
                            byte[] sizeBuffer = new byte[4];
                            byte[] imageBuffer = new byte[0];

                            while (isVideoRunning)
                            {
                                // Đọc 4 byte kích thước ảnh
                                if (!ReadExactly(stream, sizeBuffer, 4)) break;

                                // Python gửi big-endian (">L") nên phải đảo byte
                                if (BitConverter.IsLittleEndian) Array.Reverse(sizeBuffer);
                                int imageSize = BitConverter.ToInt32(sizeBuffer, 0);

                                if (imageSize <= 0 || imageSize > 10000000) break; // dữ liệu rác

                                if (imageBuffer.Length < imageSize)
                                    imageBuffer = new byte[imageSize];

                                // Đọc nội dung ảnh JPEG
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
                                    // Hiển thị lên picCamera
                                    this.BeginInvoke((MethodInvoker)(() =>
                                    {
                                        var old = picCamera.Image;
                                        picCamera.Image = bmpCopy;
                                        if (old != null) old.Dispose();
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
                catch (SocketException) { /* listener bị Stop khi đóng form */ }
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
        // CÁC HÀM SỰ KIỆN NÚT BẤM
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

        private void picCamera_Click(object sender, EventArgs e)
        {

        }
    }
}