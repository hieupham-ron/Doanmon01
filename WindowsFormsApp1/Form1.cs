using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        private System.IO.Ports.SerialPort serialPort =
    new System.IO.Ports.SerialPort();
        public frmMain()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSlot01_Click(object sender, EventArgs e)
        {
            btnSlot01.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 01";
        }

        private void btnSlot02_Click(object sender, EventArgs e)
        {
            btnSlot02.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 02";
        }

        private void btnSlot03_Click(object sender, EventArgs e)
        {
            btnSlot03.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 03";
        }

        private void btnSlot04_Click(object sender, EventArgs e)
        {
            btnSlot04.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 04";
        }

        private void btnSlot05_Click(object sender, EventArgs e)
        {
            btnSlot05.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 05";
        }

        private void btnSlot06_Click(object sender, EventArgs e)
        {
            btnSlot06.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 06";
        }

        private void btnSlot07_Click(object sender, EventArgs e)
        {
            btnSlot07.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 07";
        }

        private void btnSlot08_Click(object sender, EventArgs e)
        {
            btnSlot08.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 08";
        }

        private void btnSlot09_Click(object sender, EventArgs e)
        {
            btnSlot09.BackColor = Color.LightGreen;
            lblTarget.Text = "Vị trí kho: Ô 09";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            lblConnection.Text = "Trạng thái: Đang về HOME";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lblConnection.Text = "Trạng thái: Đang chạy";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lblConnection.Text = "Trạng thái: Đã dừng";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblConnection.Text = "Trạng thái: OFFLINE";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbPort.Items.Clear();

            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            cbPort.Items.AddRange(ports);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
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
    }
}
