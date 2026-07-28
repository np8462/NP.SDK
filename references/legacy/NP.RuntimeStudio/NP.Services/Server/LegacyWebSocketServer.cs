using NP.UI.Forms;
using NP.Services.Runtime;
using System;
using System.Windows.Forms;

namespace NP.Services.Server
{
    public class LegacyWebSocketServer
    {
        private readonly HostForm _form;

        public LegacyWebSocketServer(HostForm form)
        {
            _form = form;
        }

        public bool IsRunning { get; private set; }

        public int Port { get; private set; }

        public void Start(int port)
        {
            if (IsRunning)
                return;

            Port = port;
            IsRunning = true;

            _form.Log(string.Format("Server started on port {0}", port.ToString()));
        }

        public void Stop()
        {
            if (!IsRunning)
                return;

            IsRunning = false;

            _form.Log("Server stopped");
        }
    }
}