namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.grpObject = new System.Windows.Forms.GroupBox();
            this.lblConfidence = new System.Windows.Forms.Label();
            this.lblConnection = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnSlot09 = new System.Windows.Forms.Button();
            this.btnSlot08 = new System.Windows.Forms.Button();
            this.btnSlot07 = new System.Windows.Forms.Button();
            this.btnSlot06 = new System.Windows.Forms.Button();
            this.btnSlot05 = new System.Windows.Forms.Button();
            this.btnSlot04 = new System.Windows.Forms.Button();
            this.btnSlot03 = new System.Windows.Forms.Button();
            this.btnSlot02 = new System.Windows.Forms.Button();
            this.grpWarehouse = new System.Windows.Forms.GroupBox();
            this.btnSlot01 = new System.Windows.Forms.Button();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.pnlCamera = new System.Windows.Forms.Panel();
            this.grpObject.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.grpWarehouse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.pnlCamera.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(6, 69);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(75, 16);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Vị trí kho: ---";
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(6, 25);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(80, 16);
            this.lblObjectType.TabIndex = 0;
            this.lblObjectType.Text = "Loại phôi: ---";
            // 
            // grpObject
            // 
            this.grpObject.Controls.Add(this.lblTarget);
            this.grpObject.Controls.Add(this.lblConfidence);
            this.grpObject.Controls.Add(this.lblObjectType);
            this.grpObject.Location = new System.Drawing.Point(713, 275);
            this.grpObject.Name = "grpObject";
            this.grpObject.Size = new System.Drawing.Size(418, 130);
            this.grpObject.TabIndex = 9;
            this.grpObject.TabStop = false;
            this.grpObject.Text = "Thông tin phôi";
            // 
            // lblConfidence
            // 
            this.lblConfidence.AutoSize = true;
            this.lblConfidence.Location = new System.Drawing.Point(6, 47);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new System.Drawing.Size(83, 16);
            this.lblConfidence.TabIndex = 1;
            this.lblConfidence.Text = "Độ tin cậy: ---";
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.Location = new System.Drawing.Point(133, 51);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(128, 16);
            this.lblConnection.TabIndex = 2;
            this.lblConnection.Text = "Trạng thái: OFFLINE";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 51);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(121, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "KẾT NỐI";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(6, 21);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(121, 24);
            this.cbPort.TabIndex = 0;
            this.cbPort.Text = "Chọn COM";
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblConnection);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.cbPort);
            this.grpConnection.Location = new System.Drawing.Point(5, 385);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(418, 90);
            this.grpConnection.TabIndex = 8;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Kết nối STM32";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(95, 59);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "rs";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(14, 59);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(95, 28);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "ST";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(14, 31);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 23);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnReset);
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.btnStart);
            this.grpControl.Controls.Add(this.btnHome);
            this.grpControl.Location = new System.Drawing.Point(5, 275);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(418, 104);
            this.grpControl.TabIndex = 7;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Điều khiển";
            // 
            // btnSlot09
            // 
            this.btnSlot09.Location = new System.Drawing.Point(169, 157);
            this.btnSlot09.Name = "btnSlot09";
            this.btnSlot09.Size = new System.Drawing.Size(70, 60);
            this.btnSlot09.TabIndex = 8;
            this.btnSlot09.Text = "09";
            this.btnSlot09.UseVisualStyleBackColor = true;
            this.btnSlot09.Click += new System.EventHandler(this.btnSlot09_Click);
            // 
            // btnSlot08
            // 
            this.btnSlot08.Location = new System.Drawing.Point(93, 157);
            this.btnSlot08.Name = "btnSlot08";
            this.btnSlot08.Size = new System.Drawing.Size(70, 60);
            this.btnSlot08.TabIndex = 7;
            this.btnSlot08.Text = "08";
            this.btnSlot08.UseVisualStyleBackColor = true;
            this.btnSlot08.Click += new System.EventHandler(this.btnSlot08_Click);
            // 
            // btnSlot07
            // 
            this.btnSlot07.Location = new System.Drawing.Point(17, 157);
            this.btnSlot07.Name = "btnSlot07";
            this.btnSlot07.Size = new System.Drawing.Size(70, 60);
            this.btnSlot07.TabIndex = 6;
            this.btnSlot07.Text = "07";
            this.btnSlot07.UseVisualStyleBackColor = true;
            this.btnSlot07.Click += new System.EventHandler(this.btnSlot07_Click);
            // 
            // btnSlot06
            // 
            this.btnSlot06.Location = new System.Drawing.Point(169, 91);
            this.btnSlot06.Name = "btnSlot06";
            this.btnSlot06.Size = new System.Drawing.Size(70, 60);
            this.btnSlot06.TabIndex = 5;
            this.btnSlot06.Text = "06";
            this.btnSlot06.UseVisualStyleBackColor = true;
            this.btnSlot06.Click += new System.EventHandler(this.btnSlot06_Click);
            // 
            // btnSlot05
            // 
            this.btnSlot05.Location = new System.Drawing.Point(93, 91);
            this.btnSlot05.Name = "btnSlot05";
            this.btnSlot05.Size = new System.Drawing.Size(70, 60);
            this.btnSlot05.TabIndex = 4;
            this.btnSlot05.Text = "05";
            this.btnSlot05.UseVisualStyleBackColor = true;
            this.btnSlot05.Click += new System.EventHandler(this.btnSlot05_Click);
            // 
            // btnSlot04
            // 
            this.btnSlot04.Location = new System.Drawing.Point(17, 91);
            this.btnSlot04.Name = "btnSlot04";
            this.btnSlot04.Size = new System.Drawing.Size(70, 60);
            this.btnSlot04.TabIndex = 3;
            this.btnSlot04.Text = "04";
            this.btnSlot04.UseVisualStyleBackColor = true;
            this.btnSlot04.Click += new System.EventHandler(this.btnSlot04_Click);
            // 
            // btnSlot03
            // 
            this.btnSlot03.Location = new System.Drawing.Point(169, 25);
            this.btnSlot03.Name = "btnSlot03";
            this.btnSlot03.Size = new System.Drawing.Size(70, 60);
            this.btnSlot03.TabIndex = 2;
            this.btnSlot03.Text = "03";
            this.btnSlot03.UseVisualStyleBackColor = true;
            this.btnSlot03.Click += new System.EventHandler(this.btnSlot03_Click);
            // 
            // btnSlot02
            // 
            this.btnSlot02.Location = new System.Drawing.Point(93, 25);
            this.btnSlot02.Name = "btnSlot02";
            this.btnSlot02.Size = new System.Drawing.Size(70, 60);
            this.btnSlot02.TabIndex = 1;
            this.btnSlot02.Text = "02";
            this.btnSlot02.UseVisualStyleBackColor = true;
            this.btnSlot02.Click += new System.EventHandler(this.btnSlot02_Click);
            // 
            // grpWarehouse
            // 
            this.grpWarehouse.Controls.Add(this.btnSlot09);
            this.grpWarehouse.Controls.Add(this.btnSlot08);
            this.grpWarehouse.Controls.Add(this.btnSlot07);
            this.grpWarehouse.Controls.Add(this.btnSlot06);
            this.grpWarehouse.Controls.Add(this.btnSlot05);
            this.grpWarehouse.Controls.Add(this.btnSlot04);
            this.grpWarehouse.Controls.Add(this.btnSlot03);
            this.grpWarehouse.Controls.Add(this.btnSlot02);
            this.grpWarehouse.Controls.Add(this.btnSlot01);
            this.grpWarehouse.Location = new System.Drawing.Point(873, 12);
            this.grpWarehouse.Name = "grpWarehouse";
            this.grpWarehouse.Size = new System.Drawing.Size(258, 248);
            this.grpWarehouse.TabIndex = 6;
            this.grpWarehouse.TabStop = false;
            this.grpWarehouse.Text = "KHO 3 × 3";
            // 
            // btnSlot01
            // 
            this.btnSlot01.Location = new System.Drawing.Point(17, 25);
            this.btnSlot01.Name = "btnSlot01";
            this.btnSlot01.Size = new System.Drawing.Size(70, 60);
            this.btnSlot01.TabIndex = 0;
            this.btnSlot01.Text = "01";
            this.btnSlot01.UseVisualStyleBackColor = true;
            this.btnSlot01.Click += new System.EventHandler(this.btnSlot01_Click);
            // 
            // picCamera
            // 
            this.picCamera.Location = new System.Drawing.Point(3, 3);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(412, 217);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.TabIndex = 0;
            this.picCamera.TabStop = false;
            // 
            // pnlCamera
            // 
            this.pnlCamera.Controls.Add(this.picCamera);
            this.pnlCamera.Location = new System.Drawing.Point(5, 46);
            this.pnlCamera.Name = "pnlCamera";
            this.pnlCamera.Size = new System.Drawing.Size(418, 223);
            this.pnlCamera.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 549);
            this.Controls.Add(this.grpObject);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.grpWarehouse);
            this.Controls.Add(this.pnlCamera);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.grpObject.ResumeLayout(false);
            this.grpObject.PerformLayout();
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.grpWarehouse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.pnlCamera.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.GroupBox grpObject;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnSlot09;
        private System.Windows.Forms.Button btnSlot08;
        private System.Windows.Forms.Button btnSlot07;
        private System.Windows.Forms.Button btnSlot06;
        private System.Windows.Forms.Button btnSlot05;
        private System.Windows.Forms.Button btnSlot04;
        private System.Windows.Forms.Button btnSlot03;
        private System.Windows.Forms.Button btnSlot02;
        private System.Windows.Forms.GroupBox grpWarehouse;
        private System.Windows.Forms.Button btnSlot01;
        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.Panel pnlCamera;
    }
}