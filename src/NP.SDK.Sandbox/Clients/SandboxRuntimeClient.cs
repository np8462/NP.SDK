using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;
using NP.SDK.Server.Clients;
using NP.SDK.Server.Sessions;
using NP.SDK.Server.Transport;
using System;

namespace NP.SDK.Sandbox.Clients
{
    /// <summary>
    /// Provides a test/runtime client for the Sandbox application.
    /// </summary>
    public class SandboxRuntimeClient
    {
        private const string DefaultServerAddress =
            "ws://localhost:5051/runtime";

        private RuntimeClient _client;

        private WebSocketClientTransport _transport;

        private RuntimeSessionManager _sessionManager;


        //--------------------------------------------------
        // Events
        //--------------------------------------------------

        public event Action<string> LogReceived;


        //--------------------------------------------------
        // Logging
        //--------------------------------------------------

        private void Log(
            string text)
        {
            Action<string> handler =
                LogReceived;

            if (handler != null)
            {
                handler(text);
            }

            Console.WriteLine(text);
        }


        //--------------------------------------------------
        // Start
        //--------------------------------------------------

        public void Start()
        {
            Start(
                DefaultServerAddress,
                Environment.MachineName);
        }


        public void Start(
            string serverAddress)
        {
            Start(
                serverAddress,
                Environment.MachineName);
        }


        public void Start(
            string serverAddress,
            string clientName)
        {
            if (String.IsNullOrWhiteSpace(serverAddress))
            {
                throw new ArgumentException(
                    "Server address cannot be empty.",
                    "serverAddress");
            }


            if (String.IsNullOrWhiteSpace(clientName))
            {
                clientName =
                    Environment.MachineName;
            }


            if (_transport != null &&
                _transport.IsRunning)
            {
                Log(
                    "RuntimeClient is already running.");

                return;
            }


            Log(
                "Starting Sandbox RuntimeClient");


            Log(
                "Client Name : "
                + clientName);


            Log(
                "Server Address : "
                + serverAddress);


            _transport =
                new WebSocketClientTransport(
                    serverAddress);


            _transport.Start();


            _client =
                new RuntimeClient(
                    clientName,
                    _transport);


            _client.Connect();


            _sessionManager =
                new RuntimeSessionManager();


            _sessionManager.Create(
                _client);


            Log(
                "RuntimeClient Connected");


            Log(
                "Session Created");
        }


        //--------------------------------------------------
        // Send Message
        //--------------------------------------------------

        /// <summary>
        /// Sends a message using the default Sandbox test command.
        /// </summary>
        public void SendMessage(
            string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                Log(
                    "Message is empty.");

                return;
            }


            RuntimeMessage runtimeMessage =
                new RuntimeMessage();


            runtimeMessage.Command =
                "test";


            runtimeMessage.Data =
                message;


            runtimeMessage.Source =
                "Sandbox";


            SendMessage(
                runtimeMessage);
        }


        /// <summary>
        /// Sends a runtime message.
        /// </summary>
        public void SendMessage(
            RuntimeMessage message)
        {
            if (_client == null)
            {
                Log(
                    "RuntimeClient is not connected.");

                return;
            }


            if (!_client.Connected)
            {
                Log(
                    "RuntimeClient is disconnected.");

                return;
            }


            if (message == null)
            {
                Log(
                    "Message is null.");

                return;
            }


            _client.Send(
                message);


            Log(
                "Message sent.");
        }


        //--------------------------------------------------
        // Stop / Disconnect
        //--------------------------------------------------

        public void Stop()
        {
            if (_client == null &&
                _transport == null)
            {
                return;
            }


            Log(
                "Stopping RuntimeClient");


            if (_client != null)
            {
                _client.Disconnect();
            }


            if (_transport != null)
            {
                _transport.Stop();
            }


            _client = null;

            _transport = null;

            _sessionManager = null;


            Log(
                "RuntimeClient Stopped");
        }
    }
}