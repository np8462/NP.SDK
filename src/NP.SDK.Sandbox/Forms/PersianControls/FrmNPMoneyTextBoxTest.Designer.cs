namespace NP.SDK.Sandbox.Forms.PersianControls
{
    partial class FrmNPMoneyTextBoxTest
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
            this.label7 = new System.Windows.Forms.Label();
            this.rtbManual = new System.Windows.Forms.RichTextBox();
            this.npMoneyTextBox1 = new NP.SDK.UI.PersianControls.Controls.NPMoneyTextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(309, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Manual :";
            // 
            // rtbManual
            // 
            this.rtbManual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbManual.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbManual.Location = new System.Drawing.Point(298, 35);
            this.rtbManual.Name = "rtbManual";
            this.rtbManual.ReadOnly = true;
            this.rtbManual.Size = new System.Drawing.Size(462, 472);
            this.rtbManual.TabIndex = 19;
            this.rtbManual.Text = "";
            this.rtbManual.WordWrap = false;
            // 
            // npMoneyTextBox1
            // 
            this.npMoneyTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.npMoneyTextBox1.ConvertKeyboard = false;
            this.npMoneyTextBox1.Currency = NP.SDK.UI.PersianControls.Controls.NPMoneyTextBox.NPCurrency.Toman;
            this.npMoneyTextBox1.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.System;
            this.npMoneyTextBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npMoneyTextBox1.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.System;
            this.npMoneyTextBox1.Location = new System.Drawing.Point(13, 12);
            this.npMoneyTextBox1.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.npMoneyTextBox1.Name = "npMoneyTextBox1";
            this.npMoneyTextBox1.NextControl = null;
            this.npMoneyTextBox1.PreviousControl = null;
            this.npMoneyTextBox1.Size = new System.Drawing.Size(279, 25);
            this.npMoneyTextBox1.TabIndex = 21;
            this.npMoneyTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(12, 35);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.npMoneyTextBox1;
            this.propertyGrid1.Size = new System.Drawing.Size(280, 472);
            this.propertyGrid1.TabIndex = 18;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // FrmNPMoneyTextBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 519);
            this.Controls.Add(this.npMoneyTextBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rtbManual);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "FrmNPMoneyTextBoxTest";
            this.Text = "FrmNPMoneyTextBoxTest";
            this.Load += new System.EventHandler(this.FrmNPMoneyTextBoxTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtbManual;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private UI.PersianControls.Controls.NPMoneyTextBox npMoneyTextBox1;
    }
}