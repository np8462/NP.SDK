using NP.SDK.Server.Hosting;
using System;

namespace NP.SDK.Sandbox.Server
{
    public class SandboxRuntimeServer
    {
        private RuntimeServer _server;


        public event Action<string> LogReceived;


        private void Log(string text)
        {
            if (LogReceived != null)
                LogReceived(text);

            Console.WriteLine(text);
        }


        public void Start()
        {
            Log("Starting Runtime Server");


            _server =
                new RuntimeServer();


            _server.Logger.LogWritten +=
                Logger_LogWritten;


            _server.Start();


            Log("Runtime Server Started");
        }


        private void Logger_LogWritten(
            string message)
        {
            Log(message);
        }


        public void Stop()
        {
            if (_server != null)
            {
                _server.Stop();
            }


            Log("Runtime Server Stopped");
        }
    }
}