using NP.SDK.Contracts.Messages;
using NP.SDK.Server.Clients;
using NP.SDK.Server.Transport;
using NP.SDK.Server.Sessions;
using System;
using NP.SDK.Contracts;


namespace NP.SDK.Sandbox.Clients
{
    public class SandboxRuntimeClient
    {
        private RuntimeClient _client;

        //private WebSocketTransport _transport;
        //private IRuntimeTransport _transport;
        private WebSocketClientTransport _transport;

        private RuntimeSessionManager _sessionManager;

        public event Action<string> LogReceived;

        private void Log(string text)
        {
            if (LogReceived != null)
            {
                LogReceived(text);
            }

            Console.WriteLine(text);
        }

        public void Start()
        {
            Log("Starting Sandbox RuntimeClient");


            _transport =
                new WebSocketClientTransport(
                    "ws://localhost:5051/runtime");


            _transport.Start();


            _client =
                new RuntimeClient(
                    "Sandbox Client",
                    _transport);


            _client.Connect();


            _sessionManager =
                new RuntimeSessionManager();

            _sessionManager.Create(
                _client);

            //_server.SessionManager.Create(_client);


            RuntimeMessage message =
                new RuntimeMessage();


            message.Command =
                "test";


            message.Data =
                "Hello from RuntimeClient";


            _client.Send(message);


            Log("RuntimeClient Connected");


            Log("Session Created");


            Log("Sending test message");
        }

        public void Stop()
        {
            Log("Stopping RuntimeClient");


            if (_client != null)
            {
                _client.Disconnect();
            }


            if (_transport != null)
            {
                _transport.Stop();
            }


            Log("RuntimeClient Stopped");
        }
    }
}