using System;
using System.Collections.Generic;
using NP.SDK.Contracts;
using NP.SDK.Core.Runtime;
using NP.SDK.Server.Context;
using NP.SDK.Server.Dispatching;
using NP.SDK.Server.Logging;
using NP.SDK.Server.Sessions;
using NP.SDK.Server.Transport;
using NP.SDK.Contracts.Messages;
using System.Web.Script.Serialization;
using NP.SDK.Server.Clients;

namespace NP.SDK.Server.Hosting
{
    /// <summary>
    /// Main runtime server.
    /// Responsible for composing runtime services
    /// and managing transports.
    /// </summary>
    public class RuntimeServer :
        IRuntimeServer
    {
        private readonly List<IRuntimeTransport> _transports;
        private WebSocketTransport _webSocketTransport;
        
        private bool _running;

        public RuntimeServer()
        {
            _transports =
                new List<IRuntimeTransport>();

            Context =
                new RuntimeContextService();

            Sessions =
                new RuntimeSessionManager();

            Logger =
                new RuntimeLogger();

            Dispatcher =
                new MessageDispatcher();

            HttpTransport http =
                new HttpTransport();

            http.MessageReceived +=
                Dispatcher.Dispatch;

            AddTransport(http);

            //AddTransport(
            //    new WebSocketTransport());
         
            _webSocketTransport =
    new WebSocketTransport();
            _webSocketTransport.ClientConnected +=
                WebSocketTransport_ClientConnected;
            
            AddTransport(
                _webSocketTransport);
            
            CommandDispatcher =
    new CommandDispatcher();

            CommandDispatcher.Register(
                new PingCommandHandler());

            CommandDispatcher.Register(
                new TestCommandHandler());

            WireEvents();
        }

        //--------------------------------------------------
        // Properties
        //--------------------------------------------------
        public RuntimeContextService Context
        {
            get;
            private set;
        }

        public RuntimeSessionManager Sessions
        {
            get;
            private set;
        }

        public RuntimeLogger Logger
        {
            get;
            private set;
        }

        public MessageDispatcher Dispatcher
        {
            get;
            private set;
        }

        public IEnumerable<IRuntimeTransport> Transports
        {
            get
            {
                return _transports;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        public NP.SDK.Server.Dispatching.CommandDispatcher CommandDispatcher
        {
            get;
            private set;
        }

        //--------------------------------------------------
        // Events
        //--------------------------------------------------
        private void WireEvents()
        {
            Dispatcher.MessageReceived -=
                Dispatcher_MessageReceived;

            Dispatcher.MessageReceived +=
                Dispatcher_MessageReceived;

            foreach (IRuntimeTransport transport
                in _transports)
            {
                transport.DataReceived +=
                    Transport_DataReceived;
            }
        }

        private void WebSocketTransport_ClientConnected(
            WebSocketConnection connection)
        {
            Logger.Write(
    ">>> ClientConnected Event Fired");
            RuntimeClient client =
    new RuntimeClient(
        connection.EndPoint,
        _webSocketTransport);

            //client.Connect();

            RuntimeSession session =
                Sessions.Create(client);

            Logger.Write(
                "Runtime Session Created : " +
                session.Id);

            Logger.Write(
                "Session Count : " +
                Sessions.Count);
        }

        private void Dispatcher_MessageReceived(
            RuntimeMessage message)
        {
            Logger.Write(
                "[" + message.Source + "] "
                + message.Command
                + " : "
                + message.Data);

            if (!CommandDispatcher.Dispatch(message))
            {
                Logger.Write(
                    "Unknown Command : "
                    + message.Command);
            }
        }
        
        //--------------------------------------------------
        // Runtime message
        //--------------------------------------------------
        private void OnMessageReceived(
            RuntimeMessage message)
        {
            if (message == null)
                return;


            Logger.Write(
                "Message : "
                + message.Data);
        }

        //--------------------------------------------------
        // Transport management
        //--------------------------------------------------
        public void AddTransport(
            IRuntimeTransport transport)
        {
            if (transport == null)
                return;


            _transports.Add(
                transport);
        }

        //--------------------------------------------------
        // Runtime control
        //--------------------------------------------------
        public void Start()
        {
            if (_running)
                return;


            Logger.Write(
                "Runtime Starting");


            foreach (IRuntimeTransport transport
                in _transports)
            {
                Logger.Write("Starting: " + transport.GetType().Name);
                transport.Start();
            }


            _running = true;


            Logger.Write(
                "Runtime Started");
        }

        public void Stop()
        {
            if (!_running)
                return;


            foreach (IRuntimeTransport transport
                in _transports)
            {
                transport.Stop();
            }


            _running = false;


            Logger.Write(
                "Runtime Stopped");
        }

        private void Transport_DataReceived(string data)
        {
            if (data == "WebSocket Client Connected")
            {
                Logger.Write(data);

                return;
            }

            if (data == "WebSocket Client Closed")
            {
                Logger.Write(data);

                return;
            }

            RuntimeMessage message = null;


            if (!String.IsNullOrWhiteSpace(data))
            {
                string text = data.Trim();

                if (text.StartsWith("{"))
                {
                    try
                    {
                        JavaScriptSerializer serializer =
                            new JavaScriptSerializer();

                        message =
                            serializer.Deserialize<RuntimeMessage>(
                                text);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(
                            "Deserialize Error : "
                            + ex.Message);
                    }
                }
            }

            if (message == null)
            {
                message = new RuntimeMessage();

                message.Command = "transport";

                message.Data = data;
            }

            Dispatcher.Dispatch(message);
        }

    }
}