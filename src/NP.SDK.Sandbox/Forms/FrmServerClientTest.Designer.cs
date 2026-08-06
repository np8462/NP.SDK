namespace NP.SDK.Sandbox.Forms
{
    partial class FrmServerClientTest
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendTest = new System.Windows.Forms.Button();
            this.btnDisconnectClient = new System.Windows.Forms.Button();
            this.btnConnectClient = new System.Windows.Forms.Button();
            this.txtServerAddress = new NP.SDK.UI.PersianControls.Controls.NPPersianTextBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientName = new NP.SDK.UI.PersianControls.Controls.NPPersianTextBox();
            this.txtPort = new NP.SDK.UI.PersianControls.Controls.NPPersianTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSendTest);
            this.groupBox1.Controls.Add(this.btnDisconnectClient);
            this.groupBox1.Controls.Add(this.btnConnectClient);
            this.groupBox1.Controls.Add(this.txtServerAddress);
            this.groupBox1.Controls.Add(this.btnStopServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnStartServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Runtime Mode";
            // 
            // btnSendTest
            // 
            this.btnSendTest.Location = new System.Drawing.Point(99, 104);
            this.btnSendTest.Name = "btnSendTest";
            this.btnSendTest.Size = new System.Drawing.Size(92, 23);
            this.btnSendTest.TabIndex = 13;
            this.btnSendTest.Text = "Send Test";
            this.btnSendTest.UseVisualStyleBackColor = true;
            this.btnSendTest.Click += new System.EventHandler(this.btnSendTest_Click);
            // 
            // btnDisconnectClient
            // 
            this.btnDisconnectClient.Location = new System.Drawing.Point(197, 133);
            this.btnDisconnectClient.Name = "btnDisconnectClient";
            this.btnDisconnectClient.Size = new System.Drawing.Size(116, 23);
            this.btnDisconnectClient.TabIndex = 12;
            this.btnDisconnectClient.Text = "Disconnect Client";
            this.btnDisconnectClient.UseVisualStyleBackColor = true;
            this.btnDisconnectClient.Click += new System.EventHandler(this.btnDisconnectClient_Click);
            // 
            // btnConnectClient
            // 
            this.btnConnectClient.Location = new System.Drawing.Point(197, 104);
            this.btnConnectClient.Name = "btnConnectClient";
            this.btnConnectClient.Size = new System.Drawing.Size(116, 23);
            this.btnConnectClient.TabIndex = 11;
            this.btnConnectClient.Text = "Connect Client";
            this.btnConnectClient.UseVisualStyleBackColor = true;
            this.btnConnectClient.Click += new System.EventHandler(this.btnConnectClient_Click);
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.ConvertKeyboard = false;
            this.txtServerAddress.DecimalPlaces = 0;
            this.txtServerAddress.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.English;
            this.txtServerAddress.InputMode = NP.SDK.UI.PersianControls.Validation.InputMode.Any;
            this.txtServerAddress.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.System;
            this.txtServerAddress.Location = new System.Drawing.Point(93, 50);
            this.txtServerAddress.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtServerAddress.MoveToNextControlOnEnter = false;
            this.txtServerAddress.MoveToPreviousControlOnBackspace = false;
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.NextControl = null;
            this.txtServerAddress.PreviousControl = null;
            this.txtServerAddress.Size = new System.Drawing.Size(222, 20);
            this.txtServerAddress.TabIndex = 10;
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(18, 133);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(75, 23);
            this.btnStopServer.TabIndex = 8;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Server Name:";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(18, 104);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 7;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // txtClientName
            // 
            this.txtClientName.ConvertKeyboard = false;
            this.txtClientName.DecimalPlaces = 0;
            this.txtClientName.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.English;
            this.txtClientName.InputMode = NP.SDK.UI.PersianControls.Validation.InputMode.Any;
            this.txtClientName.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.English;
            this.txtClientName.Location = new System.Drawing.Point(93, 24);
            this.txtClientName.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtClientName.MoveToNextControlOnEnter = false;
            this.txtClientName.MoveToPreviousControlOnBackspace = false;
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.NextControl = null;
            this.txtClientName.PreviousControl = null;
            this.txtClientName.Size = new System.Drawing.Size(100, 20);
            this.txtClientName.TabIndex = 6;
            // 
            // txtPort
            // 
            this.txtPort.ConvertKeyboard = false;
            this.txtPort.DecimalPlaces = 0;
            this.txtPort.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.English;
            this.txtPort.InputMode = NP.SDK.UI.PersianControls.Validation.InputMode.Integer;
            this.txtPort.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.English;
            this.txtPort.Location = new System.Drawing.Point(93, 76);
            this.txtPort.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPort.MoveToNextControlOnEnter = false;
            this.txtPort.MoveToPreviousControlOnBackspace = false;
            this.txtPort.Name = "txtPort";
            this.txtPort.NextControl = null;
            this.txtPort.PreviousControl = null;
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Client Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Location = new System.Drawing.Point(0, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 174);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(338, 155);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // FrmServerClientTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 365);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmServerClientTest";
            this.Text = "FrmServerClientTest";
            this.Load += new System.EventHandler(this.FrmServerClientTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Button btnStartServer;
        private UI.PersianControls.Controls.NPPersianTextBox txtClientName;
        private UI.PersianControls.Controls.NPPersianTextBox txtPort;
        private UI.PersianControls.Controls.NPPersianTextBox txtServerAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendTest;
        private System.Windows.Forms.Button btnDisconnectClient;
        private System.Windows.Forms.Button btnConnectClient;
    }
}