using System;
using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;
using WebSocketSharp;

namespace NP.SDK.Android.Transport
{
    public class AndroidWebSocketTransport :
        IRuntimeTransport
    {
        private WebSocket _client;
        private bool _running;

        private readonly string _url;

        public event Action<string> DataReceived;

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        public AndroidWebSocketTransport(
            string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(
                    "WebSocket URL cannot be empty.",
                    "url");
            }

            _url = url;
        }

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _client =
                new WebSocket(_url);

            _client.OnOpen +=
                Client_OnOpen;

            _client.OnMessage +=
                Client_OnMessage;

            _client.OnClose +=
                Client_OnClose;

            _client.OnError +=
                Client_OnError;

            _client.Connect();
        }

        private void Client_OnOpen(
            object sender,
            EventArgs e)
        {
            _running = true;

            RaiseData(
                "Android WebSocket Connected");
        }

        private void Client_OnMessage(
            object sender,
            MessageEventArgs e)
        {
            RaiseData(e.Data);
        }

        private void Client_OnClose(
            object sender,
            CloseEventArgs e)
        {
            _running = false;

            RaiseData(
                "Android WebSocket Closed");
        }

        private void Client_OnError(
            object sender,
            ErrorEventArgs e)
        {
            RaiseData(
                "Android WebSocket Error : "
                + e.Message);
        }

        public void Send(
            string data)
        {
            if (_client == null)
            {
                return;
            }

            if (!_client.IsAlive)
            {
                return;
            }

            _client.Send(data);
        }

        public void Send(
            RuntimeMessage message)
        {
            if (message == null)
            {
                return;
            }

            string json =
                Newtonsoft.Json.JsonConvert.SerializeObject(
                    message);

            Send(json);
        }

        public void Stop()
        {
            if (_client != null)
            {
                _client.Close();

                _client = null;
            }

            _running = false;
        }

        private void RaiseData(
            string data)
        {
            Action<string> handler =
                DataReceived;

            if (handler != null)
            {
                handler(data);
            }
        }
    }
}