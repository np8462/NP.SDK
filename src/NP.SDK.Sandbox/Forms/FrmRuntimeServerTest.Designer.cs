namespace NP.SDK.Sandbox.Forms
{
    partial class FrmRuntimeServerTest
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnSendTest = new System.Windows.Forms.Button();
            this.btnHttpTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(343, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 41);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(406, 208);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // btnSendTest
            // 
            this.btnSendTest.Location = new System.Drawing.Point(93, 12);
            this.btnSendTest.Name = "btnSendTest";
            this.btnSendTest.Size = new System.Drawing.Size(75, 23);
            this.btnSendTest.TabIndex = 3;
            this.btnSendTest.Text = "Send Test";
            this.btnSendTest.UseVisualStyleBackColor = true;
            this.btnSendTest.Click += new System.EventHandler(this.btnSendTest_Click);
            // 
            // btnHttpTest
            // 
            this.btnHttpTest.Location = new System.Drawing.Point(174, 12);
            this.btnHttpTest.Name = "btnHttpTest";
            this.btnHttpTest.Size = new System.Drawing.Size(75, 23);
            this.btnHttpTest.TabIndex = 4;
            this.btnHttpTest.Text = "Http Test";
            this.btnHttpTest.UseVisualStyleBackColor = true;
            this.btnHttpTest.Click += new System.EventHandler(this.btnHttpTest_Click);
            // 
            // FrmRuntimeServerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 261);
            this.Controls.Add(this.btnHttpTest);
            this.Controls.Add(this.btnSendTest);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "FrmRuntimeServerTest";
            this.Text = "FrmRuntimeServerTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnSendTest;
        private System.Windows.Forms.Button btnHttpTest;
    }
}