namespace NP.SDK.Sandbox.Forms
{
    partial class FrmSecurityTest
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
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpRoles = new System.Windows.Forms.GroupBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.grpPermissions = new System.Windows.Forms.GroupBox();
            this.lstPermissions = new System.Windows.Forms.ListBox();
            this.grpAuthorization = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbResource = new System.Windows.Forms.ComboBox();
            this.btnCheckAuthorization = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.lblIdentityResult = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAuthorizationResult = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTransportResult = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinalResult = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSecurityLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTestIdentity = new System.Windows.Forms.Button();
            this.btnTestAuthorization = new System.Windows.Forms.Button();
            this.btnTestTransport = new System.Windows.Forms.Button();
            this.btnRunFullTest = new System.Windows.Forms.Button();
            this.grpUser.SuspendLayout();
            this.grpRoles.SuspendLayout();
            this.grpPermissions.SuspendLayout();
            this.grpAuthorization.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lblStatus);
            this.grpUser.Controls.Add(this.lblStatusTitle);
            this.grpUser.Controls.Add(this.cmbUser);
            this.grpUser.Controls.Add(this.lblUser);
            this.grpUser.Location = new System.Drawing.Point(12, 0);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(463, 62);
            this.grpUser.TabIndex = 0;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "User / Identity";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 26);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User:";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(50, 23);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(182, 21);
            this.cmbUser.TabIndex = 1;
            this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Location = new System.Drawing.Point(238, 26);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(40, 13);
            this.lblStatusTitle.TabIndex = 2;
            this.lblStatusTitle.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(284, 26);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(166, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Active";
            // 
            // grpRoles
            // 
            this.grpRoles.Controls.Add(this.lstRoles);
            this.grpRoles.Location = new System.Drawing.Point(12, 68);
            this.grpRoles.Name = "grpRoles";
            this.grpRoles.Size = new System.Drawing.Size(463, 121);
            this.grpRoles.TabIndex = 1;
            this.grpRoles.TabStop = false;
            this.grpRoles.Text = "Roles";
            // 
            // lstRoles
            // 
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(3, 16);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(457, 102);
            this.lstRoles.TabIndex = 0;
            // 
            // grpPermissions
            // 
            this.grpPermissions.Controls.Add(this.lstPermissions);
            this.grpPermissions.Location = new System.Drawing.Point(12, 195);
            this.grpPermissions.Name = "grpPermissions";
            this.grpPermissions.Size = new System.Drawing.Size(463, 121);
            this.grpPermissions.TabIndex = 2;
            this.grpPermissions.TabStop = false;
            this.grpPermissions.Text = "Permissions";
            // 
            // lstPermissions
            // 
            this.lstPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPermissions.FormattingEnabled = true;
            this.lstPermissions.Location = new System.Drawing.Point(3, 16);
            this.lstPermissions.Name = "lstPermissions";
            this.lstPermissions.Size = new System.Drawing.Size(457, 102);
            this.lstPermissions.TabIndex = 0;
            // 
            // grpAuthorization
            // 
            this.grpAuthorization.Controls.Add(this.btnCheckAuthorization);
            this.grpAuthorization.Controls.Add(this.cmbResource);
            this.grpAuthorization.Controls.Add(this.label2);
            this.grpAuthorization.Controls.Add(this.cmbOperation);
            this.grpAuthorization.Controls.Add(this.label3);
            this.grpAuthorization.Location = new System.Drawing.Point(12, 322);
            this.grpAuthorization.Name = "grpAuthorization";
            this.grpAuthorization.Size = new System.Drawing.Size(463, 85);
            this.grpAuthorization.TabIndex = 4;
            this.grpAuthorization.TabStop = false;
            this.grpAuthorization.Text = "Authorization Test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resource:";
            // 
            // cmbOperation
            // 
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(296, 25);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(154, 21);
            this.cmbOperation.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Operation:";
            // 
            // cmbResource
            // 
            this.cmbResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResource.FormattingEnabled = true;
            this.cmbResource.Location = new System.Drawing.Point(78, 25);
            this.cmbResource.Name = "cmbResource";
            this.cmbResource.Size = new System.Drawing.Size(132, 21);
            this.cmbResource.TabIndex = 3;
            this.cmbResource.SelectedIndexChanged += new System.EventHandler(this.cmbResource_SelectedIndexChanged);
            // 
            // btnCheckAuthorization
            // 
            this.btnCheckAuthorization.Location = new System.Drawing.Point(296, 52);
            this.btnCheckAuthorization.Name = "btnCheckAuthorization";
            this.btnCheckAuthorization.Size = new System.Drawing.Size(154, 23);
            this.btnCheckAuthorization.TabIndex = 4;
            this.btnCheckAuthorization.Text = "Check Authorization";
            this.btnCheckAuthorization.UseVisualStyleBackColor = true;
            this.btnCheckAuthorization.Click += new System.EventHandler(this.btnCheckAuthorization_Click);
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.btnRunFullTest);
            this.grpResult.Controls.Add(this.btnTestTransport);
            this.grpResult.Controls.Add(this.btnTestAuthorization);
            this.grpResult.Controls.Add(this.btnTestIdentity);
            this.grpResult.Controls.Add(this.lblFinalResult);
            this.grpResult.Controls.Add(this.label10);
            this.grpResult.Controls.Add(this.lblTransportResult);
            this.grpResult.Controls.Add(this.label8);
            this.grpResult.Controls.Add(this.lblAuthorizationResult);
            this.grpResult.Controls.Add(this.label6);
            this.grpResult.Controls.Add(this.lblIdentityResult);
            this.grpResult.Controls.Add(this.label4);
            this.grpResult.Location = new System.Drawing.Point(12, 413);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(463, 138);
            this.grpResult.TabIndex = 5;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Security Result";
            // 
            // lblIdentityResult
            // 
            this.lblIdentityResult.Location = new System.Drawing.Point(88, 24);
            this.lblIdentityResult.Name = "lblIdentityResult";
            this.lblIdentityResult.Size = new System.Drawing.Size(201, 18);
            this.lblIdentityResult.TabIndex = 3;
            this.lblIdentityResult.Text = "Active";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Identity:";
            // 
            // lblAuthorizationResult
            // 
            this.lblAuthorizationResult.Location = new System.Drawing.Point(88, 53);
            this.lblAuthorizationResult.Name = "lblAuthorizationResult";
            this.lblAuthorizationResult.Size = new System.Drawing.Size(201, 18);
            this.lblAuthorizationResult.TabIndex = 5;
            this.lblAuthorizationResult.Text = "Active";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Authorization:";
            // 
            // lblTransportResult
            // 
            this.lblTransportResult.Location = new System.Drawing.Point(88, 82);
            this.lblTransportResult.Name = "lblTransportResult";
            this.lblTransportResult.Size = new System.Drawing.Size(201, 18);
            this.lblTransportResult.TabIndex = 7;
            this.lblTransportResult.Text = "Active";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Transport:";
            // 
            // lblFinalResult
            // 
            this.lblFinalResult.Location = new System.Drawing.Point(88, 111);
            this.lblFinalResult.Name = "lblFinalResult";
            this.lblFinalResult.Size = new System.Drawing.Size(201, 18);
            this.lblFinalResult.TabIndex = 9;
            this.lblFinalResult.Text = "Active";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Final Result:";
            // 
            // txtSecurityLog
            // 
            this.txtSecurityLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecurityLog.Location = new System.Drawing.Point(481, 26);
            this.txtSecurityLog.Name = "txtSecurityLog";
            this.txtSecurityLog.Size = new System.Drawing.Size(329, 540);
            this.txtSecurityLog.TabIndex = 6;
            this.txtSecurityLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(481, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log:";
            // 
            // btnTestIdentity
            // 
            this.btnTestIdentity.Location = new System.Drawing.Point(295, 19);
            this.btnTestIdentity.Name = "btnTestIdentity";
            this.btnTestIdentity.Size = new System.Drawing.Size(154, 23);
            this.btnTestIdentity.TabIndex = 5;
            this.btnTestIdentity.Text = "Test Identity";
            this.btnTestIdentity.UseVisualStyleBackColor = true;
            this.btnTestIdentity.Click += new System.EventHandler(this.btnTestIdentity_Click);
            // 
            // btnTestAuthorization
            // 
            this.btnTestAuthorization.Location = new System.Drawing.Point(296, 48);
            this.btnTestAuthorization.Name = "btnTestAuthorization";
            this.btnTestAuthorization.Size = new System.Drawing.Size(154, 23);
            this.btnTestAuthorization.TabIndex = 10;
            this.btnTestAuthorization.Text = "Test Authorization";
            this.btnTestAuthorization.UseVisualStyleBackColor = true;
            this.btnTestAuthorization.Click += new System.EventHandler(this.btnTestAuthorization_Click);
            // 
            // btnTestTransport
            // 
            this.btnTestTransport.Location = new System.Drawing.Point(296, 77);
            this.btnTestTransport.Name = "btnTestTransport";
            this.btnTestTransport.Size = new System.Drawing.Size(154, 23);
            this.btnTestTransport.TabIndex = 11;
            this.btnTestTransport.Text = "Test Transport";
            this.btnTestTransport.UseVisualStyleBackColor = true;
            this.btnTestTransport.Click += new System.EventHandler(this.btnTestTransport_Click);
            // 
            // btnRunFullTest
            // 
            this.btnRunFullTest.Location = new System.Drawing.Point(296, 106);
            this.btnRunFullTest.Name = "btnRunFullTest";
            this.btnRunFullTest.Size = new System.Drawing.Size(154, 23);
            this.btnRunFullTest.TabIndex = 12;
            this.btnRunFullTest.Text = "Run Full Security Test";
            this.btnRunFullTest.UseVisualStyleBackColor = true;
            this.btnRunFullTest.Click += new System.EventHandler(this.btnRunFullTest_Click);
            // 
            // FrmSecurityTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 578);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSecurityLog);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpAuthorization);
            this.Controls.Add(this.grpPermissions);
            this.Controls.Add(this.grpRoles);
            this.Controls.Add(this.grpUser);
            this.Name = "FrmSecurityTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NP.SDK Security Test";
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.grpRoles.ResumeLayout(false);
            this.grpPermissions.ResumeLayout(false);
            this.grpAuthorization.ResumeLayout(false);
            this.grpAuthorization.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.GroupBox grpRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.GroupBox grpPermissions;
        private System.Windows.Forms.ListBox lstPermissions;
        private System.Windows.Forms.GroupBox grpAuthorization;
        private System.Windows.Forms.Button btnCheckAuthorization;
        private System.Windows.Forms.ComboBox cmbResource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.Label lblFinalResult;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTransportResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAuthorizationResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIdentityResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRunFullTest;
        private System.Windows.Forms.Button btnTestTransport;
        private System.Windows.Forms.Button btnTestAuthorization;
        private System.Windows.Forms.Button btnTestIdentity;
        private System.Windows.Forms.RichTextBox txtSecurityLog;
        private System.Windows.Forms.Label label1;
    }
}