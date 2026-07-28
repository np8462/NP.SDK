using NP.Services.Bridge;
using NP.Services.Routing;
using WebSocketSharp;

namespace NP.Host.RuntimeBridge.Server
{
    public sealed class RuntimeServerService
    {
        private static readonly RuntimeServerService _instance =
            new RuntimeServerService();

        public static RuntimeServerService Instance
        {
            get
            {
                return _instance;
            }
        }

        private IRuntimeServer _server;

        private RuntimeServerService()
        {
        }

        public IRuntimeServer Server
        {
            get
            {
                return _server;
            }
        }

        public bool IsRunning
        {
            get
            {
                if (_server == null)
                    return false;

                return _server.IsRunning;
            }
        }

        public void Start(
    MessageRouter router,
    BridgeSessionService bridge)
        {
            Router = router;

            BridgeSession = bridge;

            if (_server != null)
            {
                if (_server.IsRunning)
                    return;
            }

            _server =
                new RuntimeSocketServer(
                    router,
                    bridge);

            _server.Start();
        }

        public void SetContext(
    AiContext context)
        {
            BridgeSession.SetContext(context);
        }

        public void Stop()
        {
            if (_server == null)
                return;

            _server.Stop();
        }

        public BridgeSessionService BridgeSession
        {
            get;
            private set;
        }

        public MessageRouter Router
        {
            get;
            private set;
        }
    }
}