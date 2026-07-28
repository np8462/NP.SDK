using NP.Services.Bridge;
using NP.Services.Commands;
using NP.Services.Routing;
using NP.Services.Server;

namespace NP.Host.RuntimeBridge
{
    internal static class RuntimeBridgeBootstrap
    {
        private static bool _started;

        public static void Start()
        {
            if (_started)
                return;

            MessageRouter router =
                new MessageRouter(
                    null,
                    new CommandBus());

            BridgeSessionService bridge =
                new BridgeSessionService();

            bridge.SetContext(
                new AiContext()
                {
                    ProjectName = "TEST",
                    FileName = "Test.cs",
                    SelectedCode = "Console.WriteLine(\"Hello Runtime\");"
                });

            RuntimeServerService.Instance.Start(
                router,
                bridge);

            // تست اولیه
            //if (RuntimeServerService.Instance.Server != null)
            //{
            //    RuntimeServerService.Instance.Server.Send(
            //        "{\"type\":\"runtime\",\"message\":\"Bridge Started\"}");
            //}

            _started = true;
        }

        public static void Stop()
        {
            RuntimeServerService.Instance.Stop();

            _started = false;
        }
    }
}