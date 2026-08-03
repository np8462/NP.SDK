using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;
//using NP.SDK.Core.Serialization;
using WebSocketSharp;
using System;

namespace NP.SDK.Server.Transport
{
    /// <summary>
    /// WebSocket client transport.
    /// Connects RuntimeClient to RuntimeServer.
    /// </summary>
    public class WebSocketClientTransport :
        IRuntimeTransport
    {
        private WebSocket _client;

        private bool _running;


        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }


        public event Action<string> DataReceived;


        private readonly string _url;


        public WebSocketClientTransport(
            string url)
        {
            _url = url;
        }


        public void Start()
        {
            if (_running)
                return;


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


            _running = true;
        }


        private void Client_OnOpen(
            object sender,
            EventArgs e)
        {
            Console.WriteLine(
                "WebSocket Client Connected");
        }


        private void Client_OnMessage(
            object sender,
            MessageEventArgs e)
        {
            if (DataReceived != null)
            {
                DataReceived(e.Data);
            }
        }


        private void Client_OnClose(
            object sender,
            CloseEventArgs e)
        {
            _running = false;

            Console.WriteLine(
                "WebSocket Client Closed");
        }


        private void Client_OnError(
            object sender,
            ErrorEventArgs e)
        {
            Console.WriteLine(
                e.Message);
        }


        public void Stop()
        {
            if (_client != null)
            {
                _client.Close();
            }


            _running = false;
        }


        public void Send(
            string data)
        {
            if (_client == null)
                return;


            if (!_client.IsAlive)
                return;


            _client.Send(data);
        }


        public void Send(RuntimeMessage message)
        {
            string json =
                Newtonsoft.Json.JsonConvert.SerializeObject(message);

            Send(json);
        }

    }
}