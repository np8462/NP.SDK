using NP.SDK.Server.Transport;
using System;

namespace NP.SDK.Sandbox.Server
{
    public class SandboxRuntimeServer
    {
        private WebSocketTransport _transport;


        public event Action<string> LogReceived;


        private void Log(string text)
        {
            if(LogReceived != null)
                LogReceived(text);

            Console.WriteLine(text);
        }


        public void Start()
        {
            Log("Starting Runtime Server");


            _transport =
                new WebSocketTransport();


            _transport.Start();


            Log("WebSocket Server Started");
        }


        public void Stop()
        {
            if(_transport != null)
            {
                _transport.Stop();
            }


            Log("Runtime Server Stopped");
        }

    }
}