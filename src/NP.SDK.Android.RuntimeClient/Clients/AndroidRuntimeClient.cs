using System;
using NP.SDK.Contracts.Messages;
using NP.SDK.Android.RuntimeClient.Transport;

namespace NP.SDK.Android.RuntimeClient.Clients
{
    public class AndroidRuntimeClient
    {
        private AndroidWebSocketTransport _transport;

        public event Action<string> LogReceived;

        private void Log(
            string text)
        {
            Action<string> handler =
                LogReceived;

            if (handler != null)
            {
                handler(text);
            }
            global::Android.Util.Log.Debug(
                                        "NP.SDK.Android.RuntimeClient",
                                        text);
        }

        public void Start(
            string serverAddress)
        {
            if (String.IsNullOrWhiteSpace(serverAddress))
            {
                throw new ArgumentException(
                    "Server address cannot be empty.",
                    "serverAddress");
            }

            if (_transport != null &&
                _transport.IsRunning)
            {
                Log(
                    "Android RuntimeClient is already running.");

                return;
            }

            Log(
                "Starting Android RuntimeClient");

            Log(
                "Server Address : "
                + serverAddress);

            _transport =
                new AndroidWebSocketTransport(
                    serverAddress);

            _transport.DataReceived +=
                Transport_DataReceived;

            _transport.Start();
        }

        private void Transport_DataReceived(
            string data)
        {
            Log(
                "Received : "
                + data);
        }

        public void SendMessage(
            string message)
        {
            if (_transport == null)
            {
                Log(
                    "Android RuntimeClient is not connected.");

                return;
            }

            if (!_transport.IsRunning)
            {
                Log(
                    "Android RuntimeClient is disconnected.");

                return;
            }

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
                "Android";

            _transport.Send(
                runtimeMessage);

            Log(
                "Message sent.");
        }

        public void Stop()
        {
            if (_transport == null)
            {
                return;
            }

            Log(
                "Stopping Android RuntimeClient");

            _transport.Stop();

            _transport.DataReceived -=
                Transport_DataReceived;

            _transport = null;

            Log(
                "Android RuntimeClient Stopped");
        }
    }
}