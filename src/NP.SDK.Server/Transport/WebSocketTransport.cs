using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using NP.SDK.Contracts;

namespace NP.SDK.Server.Transport
{
    /// <summary>
    /// WebSocket based runtime transport.
    /// </summary>
    public class WebSocketTransport :
        IRuntimeTransport
    {
        private WebSocketServer _server;

        private bool _running;


        public event Action<string> DataReceived;


        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }


        public int Port
        {
            get;
            private set;
        }


        public WebSocketTransport(
            int port = 5051)
        {
            Port = port;
        }


        public void Start()
        {
            if (_running)
                return;


            _server =
                new WebSocketServer(
                    Port);


            _server.AddWebSocketService<RuntimeBehavior>(
                "/runtime",
                () =>
                {
                    return new RuntimeBehavior(this);
                });


            _server.Start();


            _running = true;
        }


        public void Stop()
        {
            if (!_running)
                return;


            if (_server != null)
            {
                _server.Stop();
                _server = null;
            }


            _running = false;
        }


        public void Send(string data)
        {
            if (!_running)
                return;


            _server.WebSocketServices
                .Broadcast(data);
        }


        private class RuntimeBehavior :
            WebSocketBehavior
        {
            private readonly WebSocketTransport _owner;


            public RuntimeBehavior(
                WebSocketTransport owner)
            {
                _owner = owner;
            }


            protected override void OnMessage(
                MessageEventArgs e)
            {
                if (_owner.DataReceived != null)
                {
                    _owner.DataReceived(e.Data);
                }
            }


            protected override void OnOpen()
            {
                if (_owner.DataReceived != null)
                {
                    _owner.DataReceived(
                        "WebSocket Client Connected");
                }
            }


            protected override void OnClose(
                CloseEventArgs e)
            {
                if (_owner.DataReceived != null)
                {
                    _owner.DataReceived(
                        "WebSocket Client Closed");
                }
            }
        }
    }
}