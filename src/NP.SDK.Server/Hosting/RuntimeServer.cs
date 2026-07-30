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

            AddTransport(
                new WebSocketTransport());

    //        WebSocketTransport ws =
    //new WebSocketTransport();

    //        ws.MessageReceived +=
    //            Dispatcher.Dispatch;

    //        AddTransport(ws);

            CommandDispatcher =
    new NP.SDK.Server.Dispatching.CommandDispatcher();

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
            Dispatcher.MessageReceived +=
                    Dispatcher_MessageReceived;

            Dispatcher.MessageReceived +=
    Dispatcher_MessageReceived;

            foreach (IRuntimeTransport transport
                in _transports)
            {
                transport.DataReceived +=
                    OnTransportDataReceived;
            }
        }

        private void Dispatcher_MessageReceived(
            RuntimeMessage message)
        {
            Logger.Write(
                "[" +
                message.Source +
                "] "
                +
                message.Command
                +
                " : "
                +
                message.Data);

            CommandDispatcher.Execute(message);
        }
    //    private void Dispatcher_MessageReceived(
    //RuntimeMessage message)
    //    {
    //        Logger.Write(
    //            "Command : "
    //            + message.Command);

    //        CommandDispatcher.Execute(message);
    //    }

        //--------------------------------------------------
        // Transport message
        //--------------------------------------------------
        private void OnTransportDataReceived(
            string data)
        {
            RuntimeMessage message =
                new RuntimeMessage();


            message.Command =
                "transport";


            message.Data =
                data;


            Dispatcher.Dispatch(message);
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
    }
}