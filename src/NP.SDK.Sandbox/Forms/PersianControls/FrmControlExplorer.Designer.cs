namespace NP.SDK.Sandbox.Forms.PersianControls
{
    partial class FrmControlExplorer
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
            this.cmbInputMode = new System.Windows.Forms.ComboBox();
            this.cmbKeyboardMode = new System.Windows.Forms.ComboBox();
            this.cmbDigitMode = new System.Windows.Forms.ComboBox();
            this.numDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.chkConvertKeyboard = new System.Windows.Forms.CheckBox();
            this.chkNextControl = new System.Windows.Forms.CheckBox();
            this.chkPreviousControl = new System.Windows.Forms.CheckBox();
            this.txtManual = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.npPersianTextBox1 = new NP.SDK.UI.PersianControls.Controls.NPPersianTextBox();
            this.txtMaxValue = new NP.SDK.UI.PersianControls.Controls.NPPersianTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDecimalPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbInputMode
            // 
            this.cmbInputMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputMode.FormattingEnabled = true;
            this.cmbInputMode.Location = new System.Drawing.Point(103, 38);
            this.cmbInputMode.Name = "cmbInputMode";
            this.cmbInputMode.Size = new System.Drawing.Size(121, 21);
            this.cmbInputMode.TabIndex = 1;
            this.cmbInputMode.SelectedIndexChanged += new System.EventHandler(this.cmbInputMode_SelectedIndexChanged);
            // 
            // cmbKeyboardMode
            // 
            this.cmbKeyboardMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyboardMode.FormattingEnabled = true;
            this.cmbKeyboardMode.Location = new System.Drawing.Point(103, 65);
            this.cmbKeyboardMode.Name = "cmbKeyboardMode";
            this.cmbKeyboardMode.Size = new System.Drawing.Size(121, 21);
            this.cmbKeyboardMode.TabIndex = 2;
            this.cmbKeyboardMode.SelectedIndexChanged += new System.EventHandler(this.cmbKeyboardMode_SelectedIndexChanged);
            // 
            // cmbDigitMode
            // 
            this.cmbDigitMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDigitMode.FormattingEnabled = true;
            this.cmbDigitMode.Location = new System.Drawing.Point(103, 92);
            this.cmbDigitMode.Name = "cmbDigitMode";
            this.cmbDigitMode.Size = new System.Drawing.Size(121, 21);
            this.cmbDigitMode.TabIndex = 3;
            this.cmbDigitMode.SelectedIndexChanged += new System.EventHandler(this.cmbDigitMode_SelectedIndexChanged);
            // 
            // numDecimalPlaces
            // 
            this.numDecimalPlaces.Location = new System.Drawing.Point(104, 119);
            this.numDecimalPlaces.Name = "numDecimalPlaces";
            this.numDecimalPlaces.Size = new System.Drawing.Size(99, 20);
            this.numDecimalPlaces.TabIndex = 4;
            this.numDecimalPlaces.ValueChanged += new System.EventHandler(this.numDecimalPlaces_ValueChanged);
            // 
            // chkConvertKeyboard
            // 
            this.chkConvertKeyboard.AutoSize = true;
            this.chkConvertKeyboard.Location = new System.Drawing.Point(12, 171);
            this.chkConvertKeyboard.Name = "chkConvertKeyboard";
            this.chkConvertKeyboard.Size = new System.Drawing.Size(114, 17);
            this.chkConvertKeyboard.TabIndex = 6;
            this.chkConvertKeyboard.Text = "Convert Keyboard ";
            this.chkConvertKeyboard.UseVisualStyleBackColor = true;
            this.chkConvertKeyboard.CheckedChanged += new System.EventHandler(this.chkConvertKeyboard_CheckedChanged);
            // 
            // chkNextControl
            // 
            this.chkNextControl.AutoSize = true;
            this.chkNextControl.Location = new System.Drawing.Point(12, 194);
            this.chkNextControl.Name = "chkNextControl";
            this.chkNextControl.Size = new System.Drawing.Size(123, 17);
            this.chkNextControl.TabIndex = 7;
            this.chkNextControl.Text = "Move Next On Enter";
            this.chkNextControl.UseVisualStyleBackColor = true;
            this.chkNextControl.CheckedChanged += new System.EventHandler(this.chkNextControl_CheckedChanged);
            // 
            // chkPreviousControl
            // 
            this.chkPreviousControl.AutoSize = true;
            this.chkPreviousControl.Location = new System.Drawing.Point(12, 217);
            this.chkPreviousControl.Name = "chkPreviousControl";
            this.chkPreviousControl.Size = new System.Drawing.Size(171, 17);
            this.chkPreviousControl.TabIndex = 8;
            this.chkPreviousControl.Text = "Move Previous On Backspace";
            this.chkPreviousControl.UseVisualStyleBackColor = true;
            this.chkPreviousControl.CheckedChanged += new System.EventHandler(this.chkPreviousControl_CheckedChanged);
            // 
            // txtManual
            // 
            this.txtManual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtManual.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManual.Location = new System.Drawing.Point(250, 31);
            this.txtManual.Name = "txtManual";
            this.txtManual.Size = new System.Drawing.Size(412, 207);
            this.txtManual.TabIndex = 9;
            this.txtManual.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Test Control :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Input Mode :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Keyboard Mode :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Digit Mode :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Decimal Places :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Max Value :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(247, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Manual :";
            // 
            // npPersianTextBox1
            // 
            this.npPersianTextBox1.ConvertKeyboard = true;
            this.npPersianTextBox1.DecimalPlaces = 2;
            this.npPersianTextBox1.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.System;
            this.npPersianTextBox1.InputMode = NP.SDK.UI.PersianControls.Validation.InputMode.Any;
            this.npPersianTextBox1.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.System;
            this.npPersianTextBox1.Location = new System.Drawing.Point(103, 12);
            this.npPersianTextBox1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.npPersianTextBox1.MoveToNextControlOnEnter = true;
            this.npPersianTextBox1.MoveToPreviousControlOnBackspace = true;
            this.npPersianTextBox1.Name = "npPersianTextBox1";
            this.npPersianTextBox1.NextControl = null;
            this.npPersianTextBox1.PreviousControl = null;
            this.npPersianTextBox1.Size = new System.Drawing.Size(121, 20);
            this.npPersianTextBox1.TabIndex = 0;
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.ConvertKeyboard = true;
            this.txtMaxValue.DecimalPlaces = 2;
            this.txtMaxValue.DigitMode = NP.SDK.UI.PersianControls.Validation.DigitMode.System;
            this.txtMaxValue.InputMode = NP.SDK.UI.PersianControls.Validation.InputMode.Decimal;
            this.txtMaxValue.KeyboardMode = NP.SDK.UI.PersianControls.Validation.KeyboardMode.System;
            this.txtMaxValue.Location = new System.Drawing.Point(103, 145);
            this.txtMaxValue.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtMaxValue.MoveToNextControlOnEnter = true;
            this.txtMaxValue.MoveToPreviousControlOnBackspace = true;
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.NextControl = null;
            this.txtMaxValue.PreviousControl = null;
            this.txtMaxValue.Size = new System.Drawing.Size(100, 20);
            this.txtMaxValue.TabIndex = 17;
            // 
            // FrmControlExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 250);
            this.Controls.Add(this.txtMaxValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtManual);
            this.Controls.Add(this.chkPreviousControl);
            this.Controls.Add(this.chkNextControl);
            this.Controls.Add(this.chkConvertKeyboard);
            this.Controls.Add(this.numDecimalPlaces);
            this.Controls.Add(this.cmbDigitMode);
            this.Controls.Add(this.cmbKeyboardMode);
            this.Controls.Add(this.cmbInputMode);
            this.Controls.Add(this.npPersianTextBox1);
            this.Name = "FrmControlExplorer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmControlExplorer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDecimalPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.PersianControls.Controls.NPPersianTextBox npPersianTextBox1;
        private System.Windows.Forms.ComboBox cmbInputMode;
        private System.Windows.Forms.ComboBox cmbKeyboardMode;
        private System.Windows.Forms.ComboBox cmbDigitMode;
        private System.Windows.Forms.NumericUpDown numDecimalPlaces;
        private System.Windows.Forms.CheckBox chkConvertKeyboard;
        private System.Windows.Forms.CheckBox chkNextControl;
        private System.Windows.Forms.CheckBox chkPreviousControl;
        private System.Windows.Forms.RichTextBox txtManual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private UI.PersianControls.Controls.NPPersianTextBox txtMaxValue;

    }
}

