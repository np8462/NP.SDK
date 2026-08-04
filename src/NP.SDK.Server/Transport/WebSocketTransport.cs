using System;
using NP.SDK.Contracts;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Web.Script.Serialization;
using NP.SDK.Contracts.Messages;
using System.Collections.Generic;
using NP.SDK.Core.Runtime;
using NP.SDK.Server.Clients;


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
        public event Action<WebSocketConnection> ClientConnected;
        public event Action<WebSocketConnection> ClientDisconnected;

        private readonly Dictionary<string, WebSocketConnection> _connections;

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
            _connections =        new Dictionary<string, WebSocketConnection>();
        }


        public void Start()
        {
            if (_running)
                return;


            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(
                    "WebSocketTransport Start Error: "
                    + ex.Message);
            }
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

        private void RaiseClientDisconnected(
    WebSocketConnection connection)
        {
            Action<WebSocketConnection> handler =
                ClientDisconnected;

            if (handler != null)
            {
                handler(connection);
            }
        }

        internal void AddConnection(
            string id,
            WebSocketConnection connection)
        {
            _connections[id] =
                connection;

            RaiseClientConnected(connection);
        }

        internal void RemoveConnection(
    string id)
        {
            WebSocketConnection connection;

            if (_connections.TryGetValue(
                id,
                out connection))
            {
                connection.Connected = false;

                RaiseClientDisconnected(connection);

                _connections.Remove(id);
            }
        }

        private void RaiseClientConnected(
    WebSocketConnection connection)
        {
            Action<WebSocketConnection> handler =
                ClientConnected;

    //        Console.WriteLine(
    //"RaiseClientConnected : " +
    //(handler == null ? "NULL" : "OK"));
            
            if (handler != null)
            {
                handler(connection);
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
                Console.WriteLine(
    "SERVER RECEIVED : "
    + e.Data);

                base.OnMessage(e);
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


                WebSocketConnection connection =
                    new WebSocketConnection();


                connection.Connected = true;

                connection.LastActivity =
                    DateTime.Now;


                connection.EndPoint =
                    Context.UserEndPoint.ToString();


                _owner.AddConnection(
                    ID,
                    connection);
            }

            protected override void OnClose(
                CloseEventArgs e)
            {
                _owner.RemoveConnection(ID);
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