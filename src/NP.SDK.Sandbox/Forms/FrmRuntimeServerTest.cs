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
using System.Net;
using System.IO;

namespace NP.SDK.Sandbox.Forms
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
                BeginInvoke(
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

        private void btnHttpTest_Click(
            object sender,
            EventArgs e)
        {
            HttpWebRequest request =
                (HttpWebRequest)
                WebRequest.Create(
                    "http://127.0.0.1:5050/");
                    //"http://localhost:5050/");

            request.Method = "POST";

            request.ContentType =
                "application/json";

            string json =
                "{\"Command\":\"ping\",\"Data\":\"Hello Runtime\"}";

            using (StreamWriter writer =
                new StreamWriter(
                    request.GetRequestStream()))
            {
                writer.Write(json);
            }

            HttpWebResponse response =
                (HttpWebResponse)
                request.GetResponse();

            using (StreamReader reader =
                new StreamReader(
                    response.GetResponseStream()))
            {
                MessageBox.Show(
                    reader.ReadToEnd());
            }
        }
    }
}
