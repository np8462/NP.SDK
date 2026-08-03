using NP.SDK.Sandbox.Clients;
using NP.SDK.Sandbox.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NP.SDK.Sandbox.Forms
{
    public partial class FrmServerClientTest : Form
    {
        private SandboxRuntimeServer _server;

        private SandboxRuntimeClient _client;

        public FrmServerClientTest()
        {
            InitializeComponent();
        }

        private void btnStart_Click(
        object sender,
        EventArgs e)
        {
            if (rbServer.Checked)
            {
                _server =
                    new SandboxRuntimeServer();

                _server.LogReceived += AddLog;

                _server.Start();
            }


            if (rbClient.Checked)
            {
                _client =
                    new SandboxRuntimeClient();

                _client.LogReceived += AddLog;

                _client.Start();
            }
        }

        private void AddLog(string text)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action<string>(AddLog),
                    text);

                return;
            }


            txtLog.AppendText(
                DateTime.Now.ToString("HH:mm:ss")
                + " "
                + text
                + Environment.NewLine);
        }

        private void btnStop_Click(
            object sender,
            EventArgs e)
        {
            if (_server != null)
            {
                _server.Stop();

                _server = null;
            }


            if (_client != null)
            {
                _client.Stop();

                _client = null;
            }


            AddLog("Stopped");
        }

    }
}
