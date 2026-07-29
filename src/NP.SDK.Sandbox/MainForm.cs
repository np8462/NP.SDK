using NP.SDK.Core.IO.FileTools;
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

        private void btnMergeFiles_Click(object sender, EventArgs e)
        {
            TextFileJoinOptions options = new TextFileJoinOptions();

            while (true)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Select Source File";
                    dialog.Filter =
                        "Source Files (*.cs;*.txt)|*.cs;*.txt|All Files (*.*)|*.*";

                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() != DialogResult.OK)
                        break;

                    options.InputFiles.Add(dialog.FileName);
                }

                DialogResult result =
                    MessageBox.Show(
                        "Do you want to add another file?",
                        "Continue",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    break;
            }

            if (options.InputFiles.Count == 0)
                return;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Save Merged File";

                dialog.Filter =
                    "C# Source (*.cs)|*.cs|Text File (*.txt)|*.txt|All Files (*.*)|*.*";

                dialog.FileName = "Merged.cs";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                options.OutputFile = dialog.FileName;
            }

            try
            {
                TextFileJoiner.Join(options);

                MessageBox.Show(
                    "Merge completed successfully.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}