using NP.SDK.UI.PersianControls.Validation;
namespace NP.SDK.UI.PersianControls.Controls
{
    partial class NPDateTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDay = new NPPersianTextBox();
            this.txtMonth = new NPPersianTextBox();
            this.txtYear = new NPPersianTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "/";
            // 
            // txtDay
            // 
            this.txtDay.DecimalPlaces = ((byte)(0));
            this.txtDay.Location = new System.Drawing.Point(89, 2);
            this.txtDay.MaxLength = 2;
            this.txtDay.MaxValue = ((long)(31));
            this.txtDay.Name = "txtDay";
            this.txtDay.NextControl = this.txtMonth;
            this.txtDay.PreviousControl = null;
            this.txtDay.Size = new System.Drawing.Size(23, 22);
            this.txtDay.TabIndex = 0;
            this.txtDay.InputMode =InputMode.Integer;
            this.txtDay.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDay_KeyUp);
            // 
            // txtMonth
            // 
            this.txtMonth.DecimalPlaces = ((byte)(0));
            this.txtMonth.Location = new System.Drawing.Point(54, 2);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.MaxValue = ((long)(12));
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.NextControl = this.txtYear;
            this.txtMonth.PreviousControl= txtDay;
            this.txtMonth.Size = new System.Drawing.Size(23, 22);
            this.txtMonth.TabIndex = 1;
            this.txtMonth.InputMode = InputMode.Integer;
            //this.txtMonth.TextChanged += new System.EventHandler(this.txtMonth_TextChanged);
            this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonth_KeyDown);
            this.txtMonth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMonth_KeyUp);
            // 
            // txtYear
            // 
            this.txtYear.DecimalPlaces = ((byte)(0));
            this.txtYear.Location = new System.Drawing.Point(3, 2);
            this.txtYear.MaxLength = 4;
            this.txtYear.MaxValue = ((long)(9378));
            this.txtYear.Name = "txtYear";
            this.txtYear.NextControl = null;
            this.txtYear.PreviousControl= txtMonth;
            this.txtYear.Size = new System.Drawing.Size(39, 22);
            this.txtYear.TabIndex = 2;
            this.txtYear.InputMode = InputMode.Integer;
            //this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            this.txtYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtYear_KeyDown);
            // 
            // NPDateTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtYear);
            this.Font = new System.Drawing.Font("NPTahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NPDateTextBox";
            this.Size = new System.Drawing.Size(113, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NPPersianTextBox txtYear;
        private System.Windows.Forms.Label label1;
        private NPPersianTextBox txtMonth;
        private NPPersianTextBox txtDay;
        private System.Windows.Forms.Label label2;
    }
}
