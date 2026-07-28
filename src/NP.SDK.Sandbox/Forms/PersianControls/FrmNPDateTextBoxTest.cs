using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NP.SDK.Sandbox.Forms.PersianControls
{
    public partial class FrmNPDateTextBoxTest : Form
    {
        public FrmNPDateTextBoxTest()
        {
            InitializeComponent();
        }

        private void npDateTextBox1_TextChanged(object sender, EventArgs e)
        {
            lblPersianDate.Text = npDateTextBox1.PersianDate;
            lblMiladiDate.Text = npDateTextBox1.IsCorrectDate
                ? npDateTextBox1.MiladiDate.ToString()
                : "";

            lblIsCorrect.Text = npDateTextBox1.IsCorrectDate.ToString();
        }

        private void FrmNPDateTextBoxTest_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject =
                npDateTextBox1;

            LoadManual();
        }

        private void LoadManual()
        {
            try
            {
                string file =
                    Path.Combine(
                    Application.StartupPath,
                    @"..\..\..\..\docs\controls\NPDateTextBox.md");

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

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            npDateTextBox1.Refresh();
        }
    }
}
