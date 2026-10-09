
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private SerialPort serialPort = new SerialPort();
        private Button selectedSlot = null;
        private bool[] slotOccupied = new bool[9];

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cbPort.Items.Clear();
            cbPort.Items.AddRange(SerialPort.GetPortNames());
        }

        // Chọn ô kho
        // Chọn ô kho và đổi trạng thái trống/đầy
        private void Slot_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int index = int.Parse(btn.Name.Substring(7)) - 1;

            slotOccupied[index] = !slotOccupied[index];

            if (slotOccupied[index])
            {
                btn.BackColor = Color.Gold;
                btn.Text = (index + 1).ToString("00") + "\nĐầy";
            }
            else
            {
                btn.BackColor = SystemColors.Control;
                btn.Text = (index + 1).ToString("00");
            }

            lblTarget.Text = "Vị trí kho: Ô " + (index + 1).ToString("00");
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

        // Gửi lệnh qua cổng COM
        private void SendCommand(string command)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("Bạn chưa kết nối COM!");
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
            lblObjectType.Text = "Loại phôi: Phôi A";
            lblConfidence.Text = "Độ tin cậy: 96%";
            lblTarget.Text = "Vị trí kho: Ô 05";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SendCommand("STOP");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblObjectType.Text = "Loại phôi: ---";
            lblConfidence.Text = "Độ tin cậy: ---";
            lblTarget.Text = "Vị trí kho: ---";
            lblConnection.Text = "Trạng thái: OFFLINE";
        }

        // Kết nối COM
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    MessageBox.Show("Đã kết nối COM rồi!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(cbPort.Text))
                {
                    MessageBox.Show("Bạn hãy chọn cổng COM trước!");
                    return;
                }

                serialPort.PortName = cbPort.Text;
                serialPort.BaudRate = 115200;
                serialPort.Open();

                lblConnection.Text = "Trạng thái: ONLINE";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại: " + ex.Message);
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