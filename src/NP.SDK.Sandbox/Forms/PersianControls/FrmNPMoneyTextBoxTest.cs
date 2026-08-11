using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NP.SDK.Sandbox.Forms.PersianControls
{
    public partial class FrmNPMoneyTextBoxTest : Form
    {
        public FrmNPMoneyTextBoxTest()
        {
            InitializeComponent();
        }

        private void FrmNPMoneyTextBoxTest_Load(
            object sender,
            EventArgs e)
        {
            propertyGrid1.SelectedObject =
                npMoneyTextBox1;

            LoadManual();
        }

        private void LoadManual()
        {
            try
            {
                string file =
                    Path.Combine(
                        Application.StartupPath,
                        @"..\..\..\..\docs\controls\NPMoneyTextBox.md");

                file = Path.GetFullPath(file);

                if (File.Exists(file))
                {
                    rtbManual.Text =
                        File.ReadAllText(file);
                }
                else
                {
                    rtbManual.Text =
                        "Manual not found.";
                }
            }
            catch (Exception ex)
            {
                rtbManual.Text =
                    ex.Message;
            }
        }

        private void propertyGrid1_PropertyValueChanged(
            object s,
            PropertyValueChangedEventArgs e)
        {
            npMoneyTextBox1.Refresh();
        }
    }
}