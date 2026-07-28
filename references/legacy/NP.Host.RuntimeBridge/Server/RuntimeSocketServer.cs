using NP.Services.Bridge;
using NP.Services.Commands;
using NP.Services.Common;
using NP.Services.Routing;
using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NP.Host.RuntimeBridge.Server
{
    public class RuntimeSocketServer :
        IRuntimeServer
    {
        //--------------------------------------------------
        // Fields
        //--------------------------------------------------

        private readonly MessageRouter _router;

        private readonly BridgeSessionService _bridgeSession;

        private WebSocketSharp.Server.WebSocketServer _server;

        private bool _running;

        //--------------------------------------------------
        // ctor
        //--------------------------------------------------

        public RuntimeSocketServer(
            MessageRouter router,
            BridgeSessionService bridge)
        {
            _router = router;

            _bridgeSession = bridge;
        }

        //--------------------------------------------------

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        //--------------------------------------------------

        public bool IsServer
        {
            get
            {
                return true;
            }
        }

        //--------------------------------------------------

        public event RuntimeMessageReceivedHandler
            MessageReceived;

        //--------------------------------------------------

        public void Start()
        {
            if (_running)
                return;

            _server =
    new WebSocketSharp.Server.WebSocketServer(5050);

            _server.AddWebSocketService<BridgeBehavior>(
                "/bridge",
                () => new BridgeBehavior(this));

            _server.Start();

            _running = true;

            if (_router != null)
            {
                _router.RouteLog(
                    "Runtime WebSocket Server Started");
            }
        }

        //--------------------------------------------------

        public void Stop()
        {
            if (!_running)
                return;

            _running = false;

            if (_server != null)
            {
                _server.Stop();

                _server = null;
            }

            if (_router != null)
            {
                _router.RouteLog(
                    "Runtime WebSocket Server Stopped");
            }
        }

        //--------------------------------------------------

        public void Send(
            string message)
        {
            if (!_running)
                return;

            if (_server == null)
                return;

            _server.WebSocketServices
                .Broadcast(message);
        }
        //--------------------------------------------------
        // Bridge Behavior
        //--------------------------------------------------

        private class BridgeBehavior :
            WebSocketBehavior
        {
            private readonly RuntimeSocketServer _runtime;

            public BridgeBehavior(
                RuntimeSocketServer runtime)
            {
                _runtime = runtime;
            }

            //--------------------------------------------------

            protected override void OnOpen()
            {
                if (_runtime._router != null)
                {
                    _runtime._router.RouteLog(
                        "WebSocket Connected");
                }

                base.OnOpen();
            }

            //--------------------------------------------------

            protected override void OnClose(
                CloseEventArgs e)
            {
                if (_runtime._router != null)
                {
                    _runtime._router.RouteLog(
                        "WebSocket Closed");
                }

                base.OnClose(e);
            }

            //--------------------------------------------------

            protected override void OnError(
                WebSocketSharp.ErrorEventArgs e)
            {
                if (_runtime._router != null)
                {
                    _runtime._router.RouteLog(
                        e.Message);
                }

                base.OnError(e);
            }

            //--------------------------------------------------
            protected override void OnMessage(
    MessageEventArgs e)
            {
                MessagePacket packet =
                    JsonHelper.Deserialize<MessagePacket>(
                        e.Data);

                if (packet.payload.ToolName == "bridge")
                {
                    if (packet.payload.Action == "receive")
                    {
                        AiContext context =
                            _runtime._bridgeSession.GetContext();

                        Send(
                            JsonHelper.Serialize(context));

                        return;
                    }
                }

                _runtime._router.Process(packet);

                Send("OK");
            }
        }
    }
}