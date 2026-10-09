
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private SerialPort serialPort = new SerialPort();
        private bool[] slotOccupied = new bool[9];
        private readonly string[] slotNames =
        {
            "btnSlot01", "btnSlot02", "btnSlot03",
            "btnSlot04", "btnSlot05", "btnSlot06",
            "btnSlot07", "btnSlot08", "btnSlot09"
        };

        public Form2()
        {
            InitializeComponent();

            // Hiển thị trạng thái ban đầu
            for (int i = 0; i < slotOccupied.Length; i++)
            {
                slotOccupied[i] = false;
            }

            if (picCamera != null)
            {
                picCamera.BackColor = Color.FromArgb(35, 35, 35);
                picCamera.SizeMode = PictureBoxSizeMode.Zoom;
                picCamera.Paint += PicCamera_Paint;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshComPorts();
            ResetInterface();
        }

        private void RefreshComPorts()
        {
            string currentPort = cbPort.Text;

            cbPort.Items.Clear();
            cbPort.Items.AddRange(SerialPort.GetPortNames());

            if (cbPort.Items.Contains(currentPort))
                cbPort.SelectedItem = currentPort;

            lblConnection.Text = serialPort.IsOpen
                ? "Trạng thái: ONLINE"
                : "Trạng thái: OFFLINE";
        }

        private void PicCamera_Paint(object sender, PaintEventArgs e)
        {
            if (picCamera.Image != null)
                return;

            string message = "CAMERA CHUA KET NOI";

            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF size = e.Graphics.MeasureString(message, font);

                e.Graphics.DrawString(
                    message,
                    font,
                    brush,
                    (picCamera.Width - size.Width) / 2,
                    (picCamera.Height - size.Height) / 2
                );
            }
        }

        // Xử lý trạng thái 9 ô kho
        private void Slot_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;

            int index = Array.IndexOf(slotNames, btn.Name);
            if (index < 0)
                return;

            slotOccupied[index] = !slotOccupied[index];

            UpdateSlot(index);

            lblTarget.Text = "Vị trí kho: Ô " +
                (index + 1).ToString("00");
        }

        private void UpdateSlot(int index)
        {
            Control[] controls = Controls.Find(
                slotNames[index], true);

            if (controls.Length == 0)
                return;

            Button btn = controls[0] as Button;
            if (btn == null)
                return;

            string number = (index + 1).ToString("00");

            if (slotOccupied[index])
            {
                btn.Text = number + " - DAY";
                btn.BackColor = Color.Gold;
            }
            else
            {
                btn.Text = number;
                btn.BackColor = SystemColors.Control;
            }
        }

        private void btnSlot01_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot02_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot03_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot04_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot05_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot06_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot07_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot08_Click(object sender, EventArgs e) => Slot_Click(sender, e);
        private void btnSlot09_Click(object sender, EventArgs e) => Slot_Click(sender, e);

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    lblConnection.Text = "Trạng thái: OFFLINE";
                    btnConnect.Text = "KẾT NỐI";
                    return;
                }

                RefreshComPorts();

                if (string.IsNullOrWhiteSpace(cbPort.Text))
                {
                    MessageBox.Show(
                        "Chưa tìm thấy cổng COM. Hãy kiểm tra thiết bị.",
                        "Thông báo");
                    return;
                }

                serialPort.PortName = cbPort.Text;
                serialPort.BaudRate = 115200;
                serialPort.NewLine = "\n";
                serialPort.Open();

                lblConnection.Text = "Trạng thái: ONLINE";
                btnConnect.Text = "NGẮT KẾT NỐI";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối COM:\n" + ex.Message,
                    "Lỗi kết nối");
            }
        }

        private void SendCommand(string command)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show(
                    "Chưa kết nối STM32. Lệnh chưa được gửi.",
                    "Thông báo");
                return;
            }

            try
            {
                serialPort.WriteLine(command);
                lblConnection.Text = "Trạng thái: Đã gửi " + command;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi lệnh: " + ex.Message);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SendCommand("HOME");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Dữ liệu mô phỏng, chưa phải kết quả nhận dạng thật
            lblObjectType.Text = "Loại phôi: Phôi A";
            lblConfidence.Text = "Độ tin cậy: Chưa đo";
            lblTarget.Text = "Vị trí kho: Ô 05";

            if (serialPort.IsOpen)
                SendCommand("START");
            else
                lblConnection.Text = "Trạng thái: Mô phỏng";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SendCommand("STOP");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetInterface();
        }

        private void ResetInterface()
        {
            lblObjectType.Text = "Loại phôi: ---";
            lblConfidence.Text = "Độ tin cậy: ---";
            lblTarget.Text = "Vị trí kho: ---";

            for (int i = 0; i < slotOccupied.Length; i++)
            {
                slotOccupied[i] = false;
                UpdateSlot(i);
            }

            if (lblConnection != null)
            {
                lblConnection.Text = serialPort.IsOpen
                    ? "Trạng thái: ONLINE"
                    : "Trạng thái: OFFLINE";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();

            serialPort.Dispose();
            base.OnFormClosing(e);
        }
    }
}