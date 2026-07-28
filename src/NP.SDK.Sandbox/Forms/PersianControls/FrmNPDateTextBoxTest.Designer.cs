namespace NP.SDK.Sandbox.Forms.PersianControls
{
    partial class FrmNPDateTextBoxTest
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
            this.npDateTextBox1 = new NP.SDK.UI.PersianControls.Controls.NPDateTextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.rtbManual = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPersianDate = new System.Windows.Forms.Label();
            this.lblMiladiDate = new System.Windows.Forms.Label();
            this.lblIsCorrect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // npDateTextBox1
            // 
            this.npDateTextBox1.ConvertKeyboard = false;
            this.npDateTextBox1.Font = new System.Drawing.Font("NPTahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npDateTextBox1.Location = new System.Drawing.Point(12, 12);
            this.npDateTextBox1.MiladiDate = new System.DateTime(((long)(0)));
            this.npDateTextBox1.Name = "npDateTextBox1";
            this.npDateTextBox1.PersianDate = "";
            this.npDateTextBox1.Size = new System.Drawing.Size(113, 26);
            this.npDateTextBox1.TabIndex = 0;
            this.npDateTextBox1.TextChanged += new System.EventHandler(this.npDateTextBox1_TextChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(12, 44);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.npDateTextBox1;
            this.propertyGrid1.Size = new System.Drawing.Size(291, 418);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // rtbManual
            // 
            this.rtbManual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbManual.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbManual.Location = new System.Drawing.Point(309, 44);
            this.rtbManual.Name = "rtbManual";
            this.rtbManual.ReadOnly = true;
            this.rtbManual.Size = new System.Drawing.Size(473, 418);
            this.rtbManual.TabIndex = 2;
            this.rtbManual.Text = "";
            this.rtbManual.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Manual :";
            // 
            // lblPersianDate
            // 
            this.lblPersianDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPersianDate.BackColor = System.Drawing.Color.Ivory;
            this.lblPersianDate.Location = new System.Drawing.Point(162, 407);
            this.lblPersianDate.Name = "lblPersianDate";
            this.lblPersianDate.Size = new System.Drawing.Size(129, 23);
            this.lblPersianDate.TabIndex = 18;
            this.lblPersianDate.Text = " ";
            // 
            // lblMiladiDate
            // 
            this.lblMiladiDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMiladiDate.BackColor = System.Drawing.Color.Ivory;
            this.lblMiladiDate.Location = new System.Drawing.Point(20, 434);
            this.lblMiladiDate.Name = "lblMiladiDate";
            this.lblMiladiDate.Size = new System.Drawing.Size(129, 23);
            this.lblMiladiDate.TabIndex = 19;
            this.lblMiladiDate.Text = " ";
            // 
            // lblIsCorrect
            // 
            this.lblIsCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIsCorrect.BackColor = System.Drawing.Color.Ivory;
            this.lblIsCorrect.Location = new System.Drawing.Point(162, 434);
            this.lblIsCorrect.Name = "lblIsCorrect";
            this.lblIsCorrect.Size = new System.Drawing.Size(129, 23);
            this.lblIsCorrect.TabIndex = 20;
            this.lblIsCorrect.Text = " ";
            // 
            // FrmNPDateTextBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 474);
            this.Controls.Add(this.lblIsCorrect);
            this.Controls.Add(this.lblMiladiDate);
            this.Controls.Add(this.lblPersianDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rtbManual);
            this.Controls.Add(this.npDateTextBox1);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "FrmNPDateTextBoxTest";
            this.Text = "FrmNPDateTextBoxTest";
            this.Load += new System.EventHandler(this.FrmNPDateTextBoxTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.PersianControls.Controls.NPDateTextBox npDateTextBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.RichTextBox rtbManual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPersianDate;
        private System.Windows.Forms.Label lblMiladiDate;
        private System.Windows.Forms.Label lblIsCorrect;
    }
}