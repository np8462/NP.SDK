//using NP.SDK.Contracts.Messages;
//using NP.SDK.Sandbox.Clients;
//using NP.SDK.Sandbox.Server;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace NP.SDK.Sandbox.Forms
//{
//    public partial class FrmServerClientTest : Form
//    {
//        private const string DefaultServerAddress =
//            "ws://localhost:5051/runtime";
//        private SandboxRuntimeServer _server;
//        private SandboxRuntimeClient _client;

//        public FrmServerClientTest()
//        {
//            InitializeComponent();
//        }

//        private void btnStartServer_Click(
//            object sender,
//            EventArgs e)
//        {
//            try
//            {
//                {
//                    _server =
//                        new SandboxRuntimeServer();

//                    _server.LogReceived +=
//                        AddLog;

//                    _server.Start();

//                    return;
//                }
//            }
//            catch (Exception ex)
//            {
//                AddLog(
//                    "Start Error : "
//                    + ex.Message);
//            }
//        }

//        private void AddLog(string text)
//        {
//            if (InvokeRequired)
//            {
//                Invoke(
//                    new Action<string>(AddLog),
//                    text);

//                return;
//            }


//            txtLog.AppendText(
//                DateTime.Now.ToString("HH:mm:ss")
//                + " "
//                + text
//                + Environment.NewLine);
//        }

//        private void btnStopServer_Click(
//            object sender,
//            EventArgs e)
//        {
//            try
//            {
//                if (_server != null)
//                {
//                    _server.Stop();

//                    _server = null;
//                }
//                AddLog("Server stopped");
//            }
//            catch (Exception ex)
//            {
//                AddLog(
//                    "Start Error : "
//                    + ex.Message);
//            }
//        }

//        private void FrmServerClientTest_Load(
//            object sender,
//            EventArgs e)
//        {
//            txtServerAddress.Text =
//                DefaultServerAddress;

//            txtClientName.Text =
//                Environment.MachineName;

//            txtPort.Text =
//                "5051";
//        }

//        private void btnConnectClient_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string address = txtServerAddress.Text.Trim();

//                if (String.IsNullOrWhiteSpace(address))
//                {
//                    AddLog(
//                        "Server Address is empty.");

//                    return;
//                }

//                _client =
//                    new SandboxRuntimeClient();

//                _client.LogReceived +=
//                    AddLog;

//                _client.Start(address);
//            }
//            catch (Exception ex)
//            {
//                AddLog(
//                    "Start Error : "
//                    + ex.Message);
//            }
//        }

//        private void btnDisconnectClient_Click(object sender, EventArgs e)
//        {
//            try
//            {

//                if (_client != null)
//                {
//                    _client.Stop();

//                    _client = null;
//                }
//                AddLog("Client disconnected");
//            }
//            catch (Exception ex)
//            {
//                AddLog(
//                    "Start Error : "
//                    + ex.Message);
//            }

//        }

//        private void btnSendTest_Click(
//            object sender,
//            EventArgs e)
//        {
//            try
//            {
//                if (_client == null)
//                {
//                    AddLog(
//                        "Client is not connected.");

//                    return;
//                }

//                RuntimeMessage message =
//                    new RuntimeMessage();

//                message.Command =
//                    "test";

//                message.Data =
//                    "Hello from RuntimeClient";

//                message.Source =
//                    "Sandbox";

//                _client.SendMessage(
//                    message);
//            }
//            catch (Exception ex)
//            {
//                AddLog(
//                    "Send Message Error : "
//                    + ex.Message);
//            }
//        }
//    }
//}


using NP.SDK.Contracts.Messages;
using NP.SDK.Sandbox.Clients;
using NP.SDK.Sandbox.Server;
using System;
using System.Windows.Forms;

namespace NP.SDK.Sandbox.Forms
{
    public partial class FrmServerClientTest : Form
    {
        private const string DefaultServerAddress =
            "ws://localhost:5051/runtime";


        private SandboxRuntimeServer _server;

        private SandboxRuntimeClient _client;


        public FrmServerClientTest()
        {
            InitializeComponent();
        }


        //--------------------------------------------------
        // Form
        //--------------------------------------------------

        private void FrmServerClientTest_Load(
            object sender,
            EventArgs e)
        {
            txtServerAddress.Text =
                DefaultServerAddress;

            txtClientName.Text =
                Environment.MachineName;

            txtPort.Text =
                "5051";
        }


        //--------------------------------------------------
        // Logging
        //--------------------------------------------------

        private void AddLog(
            string text)
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


        //--------------------------------------------------
        // Server
        //--------------------------------------------------

        private void btnStartServer_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_server != null)
                {
                    AddLog(
                        "Server is already running.");

                    return;
                }


                _server =
                    new SandboxRuntimeServer();


                _server.LogReceived +=
                    AddLog;


                _server.Start();
            }
            catch (Exception ex)
            {
                AddLog(
                    "Start Server Error : "
                    + ex.Message);
            }
        }


        private void btnStopServer_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_server == null)
                {
                    AddLog(
                        "Server is not running.");

                    return;
                }


                _server.Stop();

                _server = null;


                AddLog(
                    "Server stopped");
            }
            catch (Exception ex)
            {
                AddLog(
                    "Stop Server Error : "
                    + ex.Message);
            }
        }


        //--------------------------------------------------
        // Client
        //--------------------------------------------------

        private void btnConnectClient_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_client != null)
                {
                    AddLog(
                        "Client is already connected.");

                    return;
                }


                string address =
                    txtServerAddress.Text.Trim();


                if (String.IsNullOrWhiteSpace(address))
                {
                    AddLog(
                        "Server Address is empty.");

                    return;
                }


                string clientName =
                    txtClientName.Text.Trim();


                if (String.IsNullOrWhiteSpace(clientName))
                {
                    clientName =
                        Environment.MachineName;
                }


                _client =
                    new SandboxRuntimeClient();


                _client.LogReceived +=
                    AddLog;


                _client.Start(
                    address,
                    clientName);
            }
            catch (Exception ex)
            {
                AddLog(
                    "Connect Client Error : "
                    + ex.Message);
            }
        }


        private void btnDisconnectClient_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_client == null)
                {
                    AddLog(
                        "Client is not connected.");

                    return;
                }


                _client.Stop();

                _client = null;


                AddLog(
                    "Client disconnected");
            }
            catch (Exception ex)
            {
                AddLog(
                    "Disconnect Client Error : "
                    + ex.Message);
            }
        }


        //--------------------------------------------------
        // Test Message
        //--------------------------------------------------

        private void btnSendTest_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_client == null)
                {
                    AddLog(
                        "Client is not connected.");

                    return;
                }


                RuntimeMessage message =
                    new RuntimeMessage();


                message.Command =
                    "test";


                message.Data =
                    "Hello from RuntimeClient";


                message.Source =
                    "Sandbox";


                _client.SendMessage(
                    message);
            }
            catch (Exception ex)
            {
                AddLog(
                    "Send Message Error : "
                    + ex.Message);
            }
        }
    }
}