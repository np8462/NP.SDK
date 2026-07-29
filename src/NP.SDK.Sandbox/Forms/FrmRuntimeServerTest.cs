using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NP.SDK.Server.Hosting;

namespace NP.SDK.Sandbox.Tests
{
    public partial class FrmRuntimeServerTest : Form
    {
        private RuntimeServer _server;
        
        public FrmRuntimeServerTest()
        {
            InitializeComponent();
        }

        private void btnStart_Click(
            object sender,
            EventArgs e)
        {
            _server =
                new RuntimeServer();


            _server.Logger.LogWritten +=
                OnLogWritten;


            _server.Start();
        }

        private void OnLogWritten(
    string message)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action<string>(
                        OnLogWritten),
                    message);

                return;
            }


            txtLog.AppendText(
                message
                + Environment.NewLine);
        }

        private void btnStop_Click(
    object sender,
    EventArgs e)
        {
            if (_server != null)
            {
                _server.Stop();
            }
        }

        private void btnSendTest_Click(
            object sender,
            EventArgs e)
        {
            _server.Dispatcher.Dispatch(
                "test",
                "Hello NP.SDK Runtime");
        }
    }
}
