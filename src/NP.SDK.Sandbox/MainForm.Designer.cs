namespace NP.SDK.Sandbox
{
    partial class MainForm
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
            this.btnNPPersianTextBox = new System.Windows.Forms.Button();
            this.btnNPDateTextBox = new System.Windows.Forms.Button();
            this.btnTestLogging = new System.Windows.Forms.Button();
            this.btnTestIdentity = new System.Windows.Forms.Button();
            this.btnMergeFiles = new System.Windows.Forms.Button();
            this.btnStartRuntime = new System.Windows.Forms.Button();
            this.btnTestClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNPPersianTextBox
            // 
            this.btnNPPersianTextBox.Location = new System.Drawing.Point(12, 12);
            this.btnNPPersianTextBox.Name = "btnNPPersianTextBox";
            this.btnNPPersianTextBox.Size = new System.Drawing.Size(116, 23);
            this.btnNPPersianTextBox.TabIndex = 0;
            this.btnNPPersianTextBox.Text = "NPPersianTextBox";
            this.btnNPPersianTextBox.UseVisualStyleBackColor = true;
            this.btnNPPersianTextBox.Click += new System.EventHandler(this.btnNPPersianTextBox_Click);
            // 
            // btnNPDateTextBox
            // 
            this.btnNPDateTextBox.Location = new System.Drawing.Point(12, 41);
            this.btnNPDateTextBox.Name = "btnNPDateTextBox";
            this.btnNPDateTextBox.Size = new System.Drawing.Size(116, 23);
            this.btnNPDateTextBox.TabIndex = 1;
            this.btnNPDateTextBox.Text = "NPDateTextBox";
            this.btnNPDateTextBox.UseVisualStyleBackColor = true;
            this.btnNPDateTextBox.Click += new System.EventHandler(this.btnNPDateTextBox_Click);
            // 
            // btnTestLogging
            // 
            this.btnTestLogging.Location = new System.Drawing.Point(12, 70);
            this.btnTestLogging.Name = "btnTestLogging";
            this.btnTestLogging.Size = new System.Drawing.Size(116, 23);
            this.btnTestLogging.TabIndex = 2;
            this.btnTestLogging.Text = "Test Logging";
            this.btnTestLogging.UseVisualStyleBackColor = true;
            this.btnTestLogging.Click += new System.EventHandler(this.btnTestLogging_Click);
            // 
            // btnTestIdentity
            // 
            this.btnTestIdentity.Location = new System.Drawing.Point(12, 99);
            this.btnTestIdentity.Name = "btnTestIdentity";
            this.btnTestIdentity.Size = new System.Drawing.Size(116, 23);
            this.btnTestIdentity.TabIndex = 3;
            this.btnTestIdentity.Text = "Test Identity";
            this.btnTestIdentity.UseVisualStyleBackColor = true;
            this.btnTestIdentity.Click += new System.EventHandler(this.btnTestIdentity_Click);
            // 
            // btnMergeFiles
            // 
            this.btnMergeFiles.Location = new System.Drawing.Point(156, 12);
            this.btnMergeFiles.Name = "btnMergeFiles";
            this.btnMergeFiles.Size = new System.Drawing.Size(116, 23);
            this.btnMergeFiles.TabIndex = 4;
            this.btnMergeFiles.Text = "Merge Files";
            this.btnMergeFiles.UseVisualStyleBackColor = true;
            this.btnMergeFiles.Click += new System.EventHandler(this.btnMergeFiles_Click);
            // 
            // btnStartRuntime
            // 
            this.btnStartRuntime.Location = new System.Drawing.Point(156, 41);
            this.btnStartRuntime.Name = "btnStartRuntime";
            this.btnStartRuntime.Size = new System.Drawing.Size(116, 23);
            this.btnStartRuntime.TabIndex = 5;
            this.btnStartRuntime.Text = "Start Runtime";
            this.btnStartRuntime.UseVisualStyleBackColor = true;
            this.btnStartRuntime.Click += new System.EventHandler(this.btnStartRuntime_Click);
            // 
            // btnTestClient
            // 
            this.btnTestClient.Location = new System.Drawing.Point(156, 70);
            this.btnTestClient.Name = "btnTestClient";
            this.btnTestClient.Size = new System.Drawing.Size(116, 23);
            this.btnTestClient.TabIndex = 6;
            this.btnTestClient.Text = "Test Client";
            this.btnTestClient.UseVisualStyleBackColor = true;
            this.btnTestClient.Click += new System.EventHandler(this.btnTestClient_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnTestClient);
            this.Controls.Add(this.btnStartRuntime);
            this.Controls.Add(this.btnMergeFiles);
            this.Controls.Add(this.btnTestIdentity);
            this.Controls.Add(this.btnTestLogging);
            this.Controls.Add(this.btnNPDateTextBox);
            this.Controls.Add(this.btnNPPersianTextBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNPPersianTextBox;
        private System.Windows.Forms.Button btnNPDateTextBox;
        private System.Windows.Forms.Button btnTestLogging;
        private System.Windows.Forms.Button btnTestIdentity;
        private System.Windows.Forms.Button btnMergeFiles;
        private System.Windows.Forms.Button btnStartRuntime;
        private System.Windows.Forms.Button btnTestClient;
    }
}