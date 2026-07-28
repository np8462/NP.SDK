using NP.SDK.UI.PersianControls.Validation;
using System;
using System.IO;
using System.Windows.Forms;

namespace NP.SDK.Sandbox.Forms.PersianControls
{
    public partial class FrmControlExplorer : Form
    {
        public FrmControlExplorer()
        {
            InitializeComponent();
        }

        private void FrmControlExplorer_Load(object sender, EventArgs e)
        {
            cmbInputMode.DataSource =
                Enum.GetValues(typeof(InputMode));

            cmbKeyboardMode.DataSource =
                Enum.GetValues(typeof(KeyboardMode));

            cmbDigitMode.DataSource =
                Enum.GetValues(typeof(DigitMode));

            LoadManual();
        }

        private void LoadManual()
        {
            try
            {
                string file =
                    Path.Combine(
                        Application.StartupPath,
                        @"..\..\..\..\docs\controls\NPPersianTextBox.md");

                if (File.Exists(file))
                    txtManual.Text = File.ReadAllText(file);
                else
                    txtManual.Text = "Manual not found.";
            }
            catch (Exception ex)
            {
                txtManual.Text = ex.Message;
            }
        }

        private void cmbInputMode_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            npPersianTextBox1.InputMode =
                (InputMode)cmbInputMode.SelectedItem;
        }

        private void cmbKeyboardMode_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            npPersianTextBox1.KeyboardMode =
                (KeyboardMode)cmbKeyboardMode.SelectedItem;
        }

        private void cmbDigitMode_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            npPersianTextBox1.DigitMode =
                (DigitMode)cmbDigitMode.SelectedItem;
        }

        private void numDecimalPlaces_ValueChanged(
    object sender,
    EventArgs e)
        {
            npPersianTextBox1.DecimalPlaces =
                (int)numDecimalPlaces.Value;
        }

        private void txtMaxValue_TextChanged(
    object sender,
    EventArgs e)
        {
            decimal value;

            if (decimal.TryParse(
                txtMaxValue.Text,
                out value))
            {
                npPersianTextBox1.MaxValue = value;
            }
        }

        private void chkConvertKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            npPersianTextBox1.ConvertKeyboard =
                chkConvertKeyboard.Checked;
        }

        private void chkNextControl_CheckedChanged(object sender, EventArgs e)
        {
            npPersianTextBox1.MoveToNextControlOnEnter = chkNextControl.Checked;
        }

        private void chkPreviousControl_CheckedChanged(object sender, EventArgs e)
        {
            npPersianTextBox1.MoveToPreviousControlOnBackspace = chkPreviousControl.Checked;
        }
    }
}