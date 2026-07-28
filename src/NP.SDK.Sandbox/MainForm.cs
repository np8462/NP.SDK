using NP.SDK.Sandbox.Forms.PersianControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NP.SDK.Sandbox
{
    public partial class MainForm : Form
    {
        FrmControlExplorer frmNPPersianTextBox;
        FrmNPDateTextBoxTest frmNPDateTextBoxTest;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnNPPersianTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmNPPersianTextBox == null || frmNPPersianTextBox.IsDisposed)
                    frmNPPersianTextBox = new FrmControlExplorer();
                frmNPPersianTextBox.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNPDateTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmNPDateTextBoxTest == null || frmNPDateTextBoxTest.IsDisposed)
                    frmNPDateTextBoxTest = new FrmNPDateTextBoxTest();
                frmNPDateTextBoxTest.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTestLogging_Click(object sender, EventArgs e)
        {

            Tests.LoggingTest.Run();

            MessageBox.Show("Logging test completed.");
        }

        private void btnTestIdentity_Click(object sender, EventArgs e)
        {
            Tests.IdentityTest.Run();
            MessageBox.Show("Test identify completed.");
        }
    }
}
