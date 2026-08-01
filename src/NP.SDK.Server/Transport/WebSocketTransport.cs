using System;
using NP.SDK.Contracts;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Web.Script.Serialization;
using NP.SDK.Contracts.Messages;
using NP.SDK.Contracts.Messages;


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


            //RaiseData(
            //    "WebSocketTransport Started");
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


            //RaiseData(
            //    "WebSocketTransport Stopped");
        }



        public void Send(
            string data)
        {
            if (!_running)
                return;


            if (_server == null)
                return;


            _server.WebSocketServices
                .Broadcast(data);
        }

        public void Send(
    RuntimeMessage message)
        {
            if (message == null)
                return;


            string json =
                new System.Web.Script.Serialization.JavaScriptSerializer()
                .Serialize(message);


            Send(json);
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
                _owner.RaiseData(
                    e.Data);

    //            JavaScriptSerializer serializer =
    //new JavaScriptSerializer();

    //            RuntimeMessage message =
    //                serializer.Deserialize<RuntimeMessage>(
    //                    e.Data);

    //            Dispatcher.Dispatch(message);
            }



            protected override void OnOpen()
            {
                _owner.RaiseData(
                    "WebSocket Client Connected");
            }



            protected override void OnClose(
                CloseEventArgs e)
            {
                _owner.RaiseData(
                    "WebSocket Client Closed");
            }



            protected override void OnError(
                WebSocketSharp.ErrorEventArgs e)
            {
                _owner.RaiseData(
                    "WebSocket Error : "
                    + e.Message);
            }
        }
    }
}