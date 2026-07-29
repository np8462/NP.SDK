//============================================================
// File : Program.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Program.cs
//============================================================

using System;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(
                new RuntimeApplicationContext());
        }
    }
}


//============================================================
// File : RuntimeBridgeBootstrap.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\RuntimeBridgeBootstrap.cs
//============================================================

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


//============================================================
// File : RuntimeApplicationContext.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\RuntimeApplicationContext.cs
//============================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge
{
    public sealed class RuntimeApplicationContext
        : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public RuntimeApplicationContext()
        {
            _notifyIcon =
                new NotifyIcon();

            CreateNotifyIcon();

            StartRuntimeBridge();
        }

        //--------------------------------------------------

        private void CreateNotifyIcon()
        {
            _notifyIcon.Icon =
                SystemIcons.Application;
            //_notifyIcon.Icon =
            //    Properties.Resources.App;

            _notifyIcon.Text =
                "NP Runtime Bridge";

            _notifyIcon.Visible =
                true;

            _notifyIcon.ContextMenu =
                new ContextMenu(
                    new MenuItem[]
                    {
                        new MenuItem(
                            "Status",
                            OnStatus),

                        new MenuItem("-"),

                        new MenuItem(
                            "Exit",
                            OnExit)
                    });
        }

        //--------------------------------------------------

        private void StartRuntimeBridge()
        {
            try
            {
                RuntimeBridgeBootstrap.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Runtime Bridge");
            }
        }

        //--------------------------------------------------

        private void OnStatus(
            object sender,
            EventArgs e)
        {
            //WebSocket _socket = new WebSocket(
            //        "ws://127.0.0.1:5050/bridge");

            MessageBox.Show(
                "Runtime Bridge is running.",
                "NP Runtime Bridge",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        //--------------------------------------------------

        private void OnExit(
            object sender,
            EventArgs e)
        {
            try
            {
                RuntimeBridgeBootstrap.Stop();

                _notifyIcon.Visible =
                    false;

                _notifyIcon.Dispose();
            }
            catch
            {
            }

            ExitThread();
        }
    }
}


//============================================================
// File : IRuntimeBridgeClient.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\IRuntimeBridgeClient.cs
//============================================================

using NP.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NP.Host.RuntimeBridge
{
    public interface IRuntimeBridgeClient
    {
        bool IsConnected
        {
            get;
        }

        void EnsureRunning();

        void SetContext(
            AiContext context);

        AiContext GetContext();

        void Send(
            MessagePacket packet);
    }
}


//============================================================
// File : HttpServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\HttpServer.cs
//============================================================

using System;
using System.IO;
using System.Net;
using System.Threading;
using NP.Core.Models;
using NP.Services.Bridge;
using NP.Services.Commands;
using NP.Services.Common;
using NP.Services.Routing;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge.Server
{
    public enum ServerOwner
    {
        Host,
        Extension
    }

    public class HttpServer : IRuntimeServer
    {
        private readonly MessageRouter _router;
        private readonly BridgeSessionService _bridgeSession;
        private HttpListener _listener;
        private readonly object _sync = new object();
        private bool _started;
        private ServerOwner _owner = ServerOwner.Host;
        public event RuntimeMessageReceivedHandler MessageReceived;
        //public bool IsRunning
        //{
        //    get
        //    {
        //        return _started;
        //    }
        //}

        public HttpServer(
            MessageRouter router,
            BridgeSessionService bridgeSession)
        {
            _router = router;
            _bridgeSession = bridgeSession;
        }

        public void Start()
        {
            Start(ServerOwner.Host);
        }

        public void Start(ServerOwner owner)
        {
            try
            {
                lock (_sync)
                {
                    if (_started)
                    {
                        _router.RouteLog(
                            "HTTP Server already running. Owner = " + owner);

                        return;
                    }

                    _owner = owner;
                    _listener = new HttpListener();
                    if (IsRunning)
                        return;
                    _listener.Prefixes.Add("http://localhost:5050/");
                    _listener.Start();
                    _started = true;
                    _router.RouteLog(
                        "Listener IsListening = " +
                        _listener.IsListening);

                    Thread thread = new Thread(ListenLoop);
                    thread.IsBackground = true;
                    thread.Start();
                    _router.RouteLog(
                        "HTTP Server Started. Owner = " + owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsRunning
        {
            get
            {
                _started = (_listener != null && _listener.IsListening);
                return _started;
            }
        }
        //public void Start()
        //{
        //    if (_listener != null)
        //        return;

        //    _listener = new HttpListener();

        //    _listener.Prefixes.Add(
        //        "http://localhost:5050/");

        //    _listener.Start();


        //    Thread thread =
        //        new Thread(ListenLoop);

        //    thread.IsBackground = true;

        //    thread.Start();

        //    _router.RouteLog(
        //        "HTTP Server Started");
        //}
        public bool IsServer
        {
            get
            {
                return true;
            }
        }

        private void WriteResponse(HttpListenerContext context,    string json)
        {
            using (StreamWriter writer =
                new StreamWriter(
                    context.Response.OutputStream))
            {
                writer.Write(json);
            }

            context.Response.Close();
        }

        private void ListenLoop()
        {
            while (_listener.IsListening)
            {
                try
                {
                    HttpListenerContext context =
                        _listener.GetContext();
                    
                    string path = context.Request.Url.AbsolutePath;

                    if (context.Request.HttpMethod == "GET")
                    {
                        if (path == "/bridge/context")
                        {
                            AiContext data =
                                _bridgeSession.GetContext();

                            string contextJson =
                                JsonHelper.Serialize(data);

                            WriteResponse(context, contextJson);
                            continue;
                        }
                    }


                    string body;

                    using (StreamReader reader =
                        new StreamReader(
                            context.Request.InputStream))
                    {
                        body = reader.ReadToEnd();
                    }

                    _router.RouteLog(
                        "Client Connected");

                    _router.RouteLog(
                        "Received : " + body);

                    //----------------------------------
                    // اگر Context کامل از VS آمده باشد
                    //----------------------------------

                    if (body.Contains("\"ProjectName\"") &&
                        body.Contains("\"SelectedCode\""))
                    {
                        AiContext aiContext =
                            JsonHelper.Deserialize<AiContext>(body);

                        _bridgeSession.SetContext(aiContext);

                        _router.RouteLog(
                            "Bridge Context Updated");
                    }
                    else
                    {
                        MessagePacket packet =
                            JsonHelper.Deserialize<MessagePacket>(body);



                        if (packet.payload != null &&packet.payload.ToolName == "bridge" &&    packet.payload.Action == "receive")
                        {
                            AiContext data =
                                _bridgeSession.GetContext();

                            string contextJson =
                                JsonHelper.Serialize(data);

                            WriteResponse(context, contextJson);
                            continue;  
                        }
                        _router.Process(packet);
                    }

                    ToolResponse response =
                        new ToolResponse();

                    response.Success = true;

                    response.Result =
                        "Bridge OK";

                    string responseJson =
                        JsonHelper.Serialize(response);

                    WriteResponse(context, responseJson);
                }
                catch (Exception ex)
                {
                    _router.RouteLog(
                        ex.ToString());
                }
            }
        }

        public void Stop()
        {
            if (_listener == null)
                return;

            _listener.Stop();

            _listener.Close();

            _listener = null;

            _started = false;
        }

        public void Send(string json)
        {
            //
            // بعداً برای Push به Clientها
            //
        }
    }
}


//============================================================
// File : IRuntimeServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\IRuntimeServer.cs
//============================================================

namespace NP.Host.RuntimeBridge.Server
{
    public delegate void RuntimeMessageReceivedHandler(
        string message);

    public interface IRuntimeServer
    {
        bool IsRunning { get; }

        bool IsServer { get; }

        void Start();

        void Stop();

        void Send(string message);

        event RuntimeMessageReceivedHandler
            MessageReceived;
    }

    public enum RuntimeServerType
    {
        Http,
        WebSocket
    }
}


//============================================================
// File : LegacyWebSocketServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\LegacyWebSocketServer.cs
//============================================================

using NP.UI.Forms;
using NP.Services.Runtime;
using System;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge.Server
{
    public class LegacyWebSocketServer
    {
        private readonly HostForm _form;

        public LegacyWebSocketServer(HostForm form)
        {
            _form = form;
        }

        public bool IsRunning { get; private set; }

        public int Port { get; private set; }

        public void Start(int port)
        {
            if (IsRunning)
                return;

            Port = port;
            IsRunning = true;

            _form.Log(string.Format("Server started on port {0}", port.ToString()));
        }

        public void Stop()
        {
            if (!IsRunning)
                return;

            IsRunning = false;

            _form.Log("Server stopped");
        }
    }
}


//============================================================
// File : PipeMessage.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\PipeMessage.cs
//============================================================

namespace NP.Host.RuntimeBridge.Server
{
    public class PipeMessage
    {
        public string Sender
        {
            get;
            set;
        }

        public string Command
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}


//============================================================
// File : PipeMessage.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\PipeMessage.cs
//============================================================

namespace NP.Host.RuntimeBridge.Server
{
    public class PipeMessage
    {
        public string Sender
        {
            get;
            set;
        }

        public string Command
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}


//============================================================
// File : RuntimePipeClient.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\RuntimePipeClient.cs
//============================================================

using System.IO;
using System.IO.Pipes;
using System.Text;

namespace NP.Host.RuntimeBridge.Server
{
    public class RuntimePipeClient
    {
        private const string PipeName =
            "NP.Runtime";

        public bool Send(string json)
        {
            try
            {
                using (NamedPipeClientStream pipe =
                    new NamedPipeClientStream(
                        ".",
                        PipeName,
                        PipeDirection.Out))
                {
                    pipe.Connect(300);

                    using (StreamWriter writer =
                        new StreamWriter(pipe))
                    {
                        writer.AutoFlush = true;

                        writer.WriteLine(json);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


//============================================================
// File : RuntimePipeServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\RuntimePipeServer.cs
//============================================================

using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace NP.Host.RuntimeBridge.Server
{
    public class RuntimePipeServer :
        IRuntimeServer
    {
        private const string PipeName =
            "NP.Runtime";

        private Thread _thread;

        private bool _running;

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        public bool IsServer
        {
            get;
            private set;
        }

        public event RuntimeMessageReceivedHandler
            MessageReceived;

        public void Start()
        {
            if (_running)
                return;

            //----------------------------------
            // آیا سرور دیگری وجود دارد؟
            //----------------------------------

            RuntimePipeClient client =
                new RuntimePipeClient();

            if (client.Send("__PING__"))
            {
                IsServer = false;
                _running = true;
                return;
            }

            //----------------------------------
            // هیچ سروری نیست
            //----------------------------------

            IsServer = true;

            _running = true;

            _thread =
                new Thread(ServerLoop);

            _thread.IsBackground = true;

            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        public void Send(string json)
        {
            RuntimePipeClient client =
                new RuntimePipeClient();

            client.Send(json);
        }

        //----------------------------------------------------

        //private void ServerLoop()
        //{
        //    while (_running)
        //    {
        //        try
        //        {
        //            using (NamedPipeServerStream pipe =
        //                new NamedPipeServerStream(
        //                    PipeName,
        //                    PipeDirection.In))
        //            {
        //                pipe.WaitForConnection();

        //                using (StreamReader reader =
        //                    new StreamReader(pipe))
        //                {
        //                    string json =
        //                        reader.ReadLine();

        //                    if (!String.IsNullOrEmpty(json))
        //                    {
        //                        if (MessageReceived != null)
        //                        {
        //                            MessageReceived(json);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        private void ServerLoop()
        {
            while (_running)
            {
                NamedPipeServerStream pipe =
                    new NamedPipeServerStream(
                        PipeName,
                        PipeDirection.InOut,
                        NamedPipeServerStream.MaxAllowedServerInstances,
                        PipeTransmissionMode.Message,
                        PipeOptions.Asynchronous);

                pipe.BeginWaitForConnection(
                    PipeConnected,
                    pipe);
            }

            Thread.Sleep(50);
        }

        private void PipeConnected(
    IAsyncResult ar)
        {
            NamedPipeServerStream pipe =
                (NamedPipeServerStream)ar.AsyncState;

            try
            {
                pipe.EndWaitForConnection(ar);

                ThreadPool.QueueUserWorkItem(
                    ReadPipe,
                    pipe);
            }
            catch
            {
                pipe.Dispose();
            }
        }

        private void ReadPipe(object state)
        {
            NamedPipeServerStream pipe =
                (NamedPipeServerStream)state;

            try
            {
                using (StreamReader reader =
                    new StreamReader(pipe))
                {
                    string json =
                        reader.ReadLine();

                    if (!String.IsNullOrEmpty(json))
                    {
                        if (MessageReceived != null)
                        {
                            MessageReceived(json);
                        }
                    }
                }
            }
            catch
            {
            }

            try
            {
                pipe.Dispose();
            }
            catch
            {
            }
        }


    }
}


//============================================================
// File : RuntimeServerService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\RuntimeServerService.cs
//============================================================

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


//============================================================
// File : RuntimeSocketBehavior.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\RuntimeSocketBehavior.cs
//============================================================

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebSocketSharp;
//using WebSocketSharp.Server;

//namespace NP.Host.RuntimeBridge.Server
//{

//private class BridgeBehavior :
//    WebSocketBehavior
//{
//    private readonly RuntimeSocketServer _runtime;

//    public BridgeBehavior(
//        RuntimeSocketServer runtime)
//    {
//        _runtime = runtime;
//    }

//    //--------------------------------------------------

//    protected override void OnOpen()
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Connected");
//        }

//        base.OnOpen();
//    }

//    //--------------------------------------------------

//    protected override void OnClose(
//        CloseEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Closed");
//        }

//        base.OnClose(e);
//    }

//    //--------------------------------------------------

//    protected override void OnError(
//        WebSocketSharp.ErrorEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                e.Message);
//        }

//        base.OnError(e);
//    }

//    //--------------------------------------------------

//    protected override void OnMessage(
//        MessageEventArgs e)
//    {
//        try
//        {
//            if (_runtime.MessageReceived != null)
//            {
//                _runtime.MessageReceived(
//                    e.Data);
//            }

//            if (_runtime._router != null)
//            {
//                _runtime._router.RouteLog(
//                    "WS <= " + e.Data);
//            }

//            //------------------------------------------------
//            // فعلاً فقط Echo
//            //------------------------------------------------

//            Send("OK");
//        }
//        catch (Exception ex)
//        {
//            Send(ex.ToString());
//        }
//    }
//}



//============================================================
// File : RuntimeSocketServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.Host.RuntimeBridge\Server\RuntimeSocketServer.cs
//============================================================

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


//============================================================
// File : BridgeClientService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Bridge\BridgeClientService.cs
//============================================================

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NP.Core.Models;
using System;
using System.Windows.Forms;

namespace NP.Services.Bridge
{
    public class BridgeClientService
    {
        private readonly HttpClient _client;

        public BridgeClientService()
        {
            _client = new HttpClient();
            _client.BaseAddress =
                new Uri("http://localhost:5050/");
        }

        public bool SendContext(AiContext context)
        {
            try
            {
                string json =
                    JsonConvert.SerializeObject(context);

                StringContent content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                HttpResponseMessage response =
                    _client.PostAsync(
                        "bridge/context",
                        content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return false;
            }
        }
    }
}


//============================================================
// File : BridgeService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Bridge\BridgeService.cs
//============================================================

using System;
using System.IO;
using Newtonsoft.Json;
using NP.Core.Models;

namespace NP.Services.Bridge
{
    public class BridgeService
    {
        private string GetBridgeFile()
        {
            return Path.Combine(
                Path.GetTempPath(),
                "NP_BridgeRequest.json");
        }

        public BridgeRequest Load()
        {
            try
            {
                string file = GetBridgeFile();

                if (!File.Exists(file))
                    return null;

                string json =
                    File.ReadAllText(file);

                return JsonConvert
                    .DeserializeObject<BridgeRequest>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}


//============================================================
// File : BridgeSessionService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Bridge\BridgeSessionService.cs
//============================================================

using NP.Core.Models;

namespace NP.Services.Bridge
{
    public class BridgeSessionService
    {
        private AiContext _context;

        public void SetContext(AiContext context)
        {
            _context = context;
        }

        public AiContext GetContext()
        {
            return _context;
        }

        public void Clear()
        {
            _context = null;
        }

        public bool HasContext
        {
            get
            {
                return _context != null;
            }
        }
    }
}

//using NP.Core.Models;

//namespace NP.Services.Bridge
//{
//    public class BridgeSessionService
//    {
//        public BridgeRequest CurrentRequest
//        {
//            get;
//            private set;
//        }

//        public void SetRequest(BridgeRequest request)
//        {
//            CurrentRequest = request;
//        }

//        public void Clear()
//        {
//            CurrentRequest = null;
//        }
//    }
//}


//============================================================
// File : ChromeBridgeService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Bridge\ChromeBridgeService.cs
//============================================================

using NP.Services.Commands;
using NP.UI.Forms;
using NP.Services.Runtime;
using System.Windows.Forms;

namespace NP.Services.Bridge
{
    public class ChromeBridgeService
    {
        private HostForm _form;

        public ChromeBridgeService(HostForm form, CommandBus commandBus)
        {
            _form = form;

            commandBus.CommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(CommandPacket cmd)
        {
            if (cmd.Command != "chrome_message")
                return;

            _form.Log("ChromeBridge : " + cmd.Data);
        }
    }
}


//============================================================
// File : VSBridgeService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Bridge\VSBridgeService.cs
//============================================================

using System;
using NP.Services.Commands;
using NP.Services.Common;

namespace NP.Services.Bridge
{
    public class VSBridgeService
    {
        public event Action<string> OnInsertText;

        public void Handle(CommandPacket cmd)
        {
            if (cmd.Target != "vs2012")
                return;

            if (cmd.Command != "vs_command")
                return;

            VsCommand payload =
                JsonHelper.Deserialize<VsCommand>(cmd.Data);

            if (payload.Command == "insert_text")
            {
                if (OnInsertText != null)
                {
                    OnInsertText(payload.Data);
                }
            }
        }
    }
}

//using NP.Host.Core;
//using NP.Services.Commands;
//using System;

//    public class VSBridgeService
//    {
//        public event Action<string> OnInsertText;

//        public void Handle(CommandPacket cmd)
//        {
//            if (cmd.Target != "vs2012")
//                return;

//            if (cmd.Command == "vs_command")
//            {
//                var payload =
//                    JsonHelper.Deserialize<VsCommand>(cmd.Data);

//                if (payload.Command == "insert_text")
//                {
//                    OnInsertText?.Invoke(payload.Data);
//                }
//            }
//        }
//    }



/*
using NP.Host.Core;
using NP.Services.Commands;

namespace NP.Host.Services
{
    public class VSBridgeService
    {
        private MainForm _form;

        public VSBridgeService(MainForm form, CommandBus commandBus)
        {
            _form = form;

            commandBus.CommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(CommandPacket cmd)
        {
            if (cmd.Target != "vs2012")
                return;

            RuntimeBuilder.Log("VSBridge : " + cmd.Command);
        }
    }
}
*/


//============================================================
// File : ChromeBridgeDispatcher.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\ChromeBridge\ChromeBridgeDispatcher.cs
//============================================================

using NP.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NP.Services.ChromeBridge
{
    public sealed class ChromeBridgeDispatcher
    {
        private static readonly ChromeBridgeDispatcher _current =
            new ChromeBridgeDispatcher();

        public static ChromeBridgeDispatcher Current
        {
            get { return _current; }
        }

        private ChromeBridgeDispatcher()
        {

        }

        //--------------------------------

        public void Dispatch(MessagePacket packet)
        {
            switch (packet.Action)
            {
                case BridgeAction.InsertCode:

                    InsertCode(packet);

                    break;

                case BridgeAction.SendFile:

                    SendFile(packet);

                    break;
            }
        }

        //--------------------------------

        private void InsertCode(MessagePacket packet)
        {
            MessageBox.Show(packet.payload.Content);
        }

        //--------------------------------

        private void SendFile(MessagePacket packet)
        {
            MessageBox.Show(
                packet.payload.FileName);

            MessageBox.Show(
                packet.payload.Content);
        }

    }
}


//============================================================
// File : CommandBus.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\CommandBus.cs
//============================================================

using System;

namespace NP.Services.Commands
{
    public class CommandBus
    {
        public event Action<CommandPacket> CommandReceived;

        public void Send(CommandPacket command)
        {
            if (CommandReceived != null)
                CommandReceived(command);
        }
    }
}


//============================================================
// File : CommandManager.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\CommandManager.cs
//============================================================

using System;
using System.Collections.Generic;
using NP.Core.Models;

namespace NP.Services.Commands
{
    public class CommandManager
    {
        private List<CommandModel> _commands;

        public event Action<CommandModel> CommandAdded;

        public CommandManager()
        {
            _commands =
                new List<CommandModel>();
        }

        public List<CommandModel> Commands
        {
            get
            {
                return _commands;
            }
        }

        public void Add(
            string source,
            string command,
            string details)
        {
            CommandModel item =
                new CommandModel();

            item.Time = DateTime.Now;
            item.Source = source;
            item.Command = command;
            item.Details = details;

            _commands.Add(item);

            if (CommandAdded != null)
            {
                CommandAdded(item);
            }
        }

        public void Clear()
        {
            _commands.Clear();
        }
    }
}


//============================================================
// File : CommandPacket.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\CommandPacket.cs
//============================================================

namespace NP.Services.Commands
{
    public class CommandPacket
    {
        public string Id { get; set; }

        public string SessionId { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public string Command { get; set; }

        public string Data { get; set; }
    }
}


//============================================================
// File : CommandRegistry.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\CommandRegistry.cs
//============================================================

using System;
using System.Collections.Generic;

namespace NP.Services.Commands
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands =
            new Dictionary<string, ICommand>();

        public void Register(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            _commands[command.Name] = command;
        }

        public object Execute(string name, object parameter)
        {
            ICommand command;

            if (!_commands.TryGetValue(name, out command))
                return null;

            return command.Execute(parameter);
        }
    }
}


//============================================================
// File : ICommand.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\ICommand.cs
//============================================================

namespace NP.Services.Commands
{
    public interface ICommand
    {
        string Name { get; }

        object Execute(object parameter);
    }
}


//============================================================
// File : MessagePacket.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\MessagePacket.cs
//============================================================

namespace NP.Services.Commands
{
    public enum BridgeAction
    {
        None,
        SetContext,
        Receive,
        SendFile,
        AskAI,
        InsertCode,
        ExecuteCommand,
        Ping
    }

    public class MessagePacket
    {
        public string id { get; set; }

        public string sessionId { get; set; }

        public string source { get; set; }

        public string target { get; set; }

        public string type { get; set; }

        public BridgeAction Action { get; set; }

        public ToolRequest payload { get; set; }

        //public AiContext Context { get; set; }
    }
}


//============================================================
// File : RelayCommand.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\RelayCommand.cs
//============================================================

using System;

namespace NP.Services.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, object> _execute;

        public string Name { get; private set; }

        public RelayCommand(string name,
                            Func<object, object> execute)
        {
            Name = name;
            _execute = execute;
        }

        public object Execute(object parameter)
        {
            return _execute(parameter);
        }
    }
}


//============================================================
// File : ToolRequest.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\ToolRequest.cs
//============================================================

using NP.Services.Bridge;
using System;

namespace NP.Services.Commands
{
    public class ToolRequest
    {
        public string ToolName { get; set; }

        public string Action { get; set; }

        public string Data { get; set; }

        public string Content { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }

        public string PageTitle { get; set; }

        public DateTime? Time { get; set; }

        public AiContext Context { get; set; }
    }
}


//============================================================
// File : ToolResponse.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\ToolResponse.cs
//============================================================

/*
namespace NP.Host.Models
{
    public class ToolResponse
    {
        public bool Success { get; set; }

        public string Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
*/

namespace NP.Services.Commands
{
    public class ToolResponse
    {
        public bool Success { get; set; }

        public string Result { get; set; }

        public string Error { get; set; }
    }
}


//============================================================
// File : VsCommand.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Commands\VsCommand.cs
//============================================================

namespace NP.Services.Commands
{
    public class VsCommand
    {
        public string Command { get; set; }
        public string Data { get; set; }
    }
}


//============================================================
// File : MessageRouter.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Routing\MessageRouter.cs
//============================================================

using NP.Services.Commands;
using NP.Services.Common;
using System.Windows.Forms;

namespace NP.Services.Routing
{
    public class MessageRouter
    {
        private Form _form;
        private CommandBus _commandBus;

        //public void Process(MessagePacket packet)
        //{
        //    CommandPacket cmd =
        //        CommandFactory.Create(packet);

        //    _commandBus.Send(cmd);
        //}
        public void Process(MessagePacket packet)
        {
            CommandPacket cmd = new CommandPacket
            {
                Source = "chrome",
                Target = "host",
                Command = packet.type,
                Data = JsonHelper.Serialize(packet.payload)
            };

            _commandBus.Send(cmd);
        }
        public MessageRouter(Form form, CommandBus commandBus)
        {
            _form = form;
            _commandBus = commandBus;
        }
        public void RouteLog(string message)
        {
            CommandPacket cmd = new CommandPacket();

            cmd.Command = "log";
            cmd.Data = message;

            _commandBus.Send(cmd);
        }

        //public void Process(MessagePacket packet)
        //{
        //    switch (packet.type)
        //    {
        //        case "test_message":
        //        case "tool_request":
        //        case "ai_prompt":
        //        case "open_document":
        //        case "compile_project":

        //            CommandPacket cmd = new CommandPacket();

        //            cmd.Source = "chrome";
        //            cmd.Target = "host";
        //            cmd.Command = "chrome_message";
        //            cmd.Data = packet.payload;

        //            _commandBus.Send(cmd);

        //            break;

        //        //case "tool_request":

        //        //    break;

        //        //case "open_document":

        //        //    break;

        //        //case "compile_project":

        //        //    break;

        //        //case "ai_prompt":

        //        //    break;

        //        default:

        //            RuntimeBuilder.Log("Unknown type : " + packet.type);

        //            break;
        //    }
        //}
    }
}


//============================================================
// File : IRuntimeBridge.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\RuntimeBridge\IRuntimeBridge.cs
//============================================================

using NP.Services.Bridge;
using NP.Services.Commands;

namespace NP.Services.RuntimeBridge
{
    public interface IRuntimeBridge
    {
        bool IsConnected
        {
            get;
        }

        void EnsureRunning();

        void Connect();

        void Disconnect();

        void SetContext(
            AiContext context);

        AiContext GetContext();

        string Send(
            MessagePacket packet);
    }
}


//============================================================
// File : RuntimeBridgeClient.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\RuntimeBridge\RuntimeBridgeClient.cs
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NP.Services.Bridge;
using NP.Services.Common;
using NP.Services.Commands;    

namespace NP.Services.RuntimeBridge
{
    public sealed class RuntimeBridgeClient
        : IRuntimeBridge
    {
        readonly RuntimeBridgeSocket _socket;

        public RuntimeBridgeClient()
        {
            _socket =
                new RuntimeBridgeSocket();
        }

        //------------------------------------

        public bool IsConnected
        {
            get
            {
                return _socket.IsConnected;
            }
        }

        //------------------------------------

        public void EnsureRunning()
        {
            RuntimeBridgeLauncher.EnsureRunning();

            Connect();
        }

        //------------------------------------

        public void Connect()
        {
            _socket.Connect();
        }

        //------------------------------------

        public void Disconnect()
        {
            _socket.Disconnect();
        }

        //------------------------------------

        public void SetContext(
            AiContext context)
        {
            MessagePacket packet =
                new MessagePacket();

            packet.type =
                "bridge";

            packet.payload =
                new ToolRequest();

            packet.payload.ToolName =
                "bridge";

            packet.payload.Action =
                "setContext";

            packet.payload.Context =
                context;

            _socket.Send(
                JsonHelper.Serialize(packet));
        }

        //------------------------------------

        public AiContext GetContext()
        {
            MessagePacket packet =
                new MessagePacket();

            packet.type =
                "bridge";

            packet.payload =
                new ToolRequest();

            packet.payload.ToolName =
                "bridge";

            packet.payload.Action =
                "receive";

            string json =
                _socket.Send(
                    JsonHelper.Serialize(packet));

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonHelper.Deserialize<AiContext>(
                json);
        }

        //------------------------------------

        public string Send(
            MessagePacket packet)
        {
            return _socket.Send(
                JsonHelper.Serialize(packet));
        }
    }
}


//============================================================
// File : RuntimeBridgeLauncher.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\RuntimeBridge\RuntimeBridgeLauncher.cs
//============================================================

using System;
using System.Diagnostics;
using System.IO;
using System.Configuration; 

namespace NP.Services.RuntimeBridge
{
    public static class RuntimeBridgeLauncher
    {
        public static bool IsRunning()
        {
            Process[] list =
                Process.GetProcessesByName(
                    "NP.Host.RuntimeBridge");

            return list.Length > 0;
        }

        //------------------------------------------------

        public static void EnsureRunning()
        {
            if (IsRunning())
                return;

            Start();
        }

        //------------------------------------------------
        //public static void Start()
        //{
        //    if (ProcessExists())
        //        return;

        //    if (string.IsNullOrEmpty(RuntimeBridgePath))
        //        throw new InvalidOperationException(
        //            "RuntimeBridgePath is not initialized.");

        //    Process.Start(RuntimeBridgePath);
        //}
        public static void Start()
        {
            //string exe =
            //    Path.Combine(
            //        AppDomain.CurrentDomain.BaseDirectory,
            //        "NP.Host.RuntimeBridge.exe");

            //string exe = ConfigurationManager.AppSettings["RuntimeBridgePath"];
            string exe = RuntimeBridgeLauncher.RuntimeBridgePath;
            Process.Start(exe);
        }

        public static string RuntimeBridgePath
        {
            get;
            set;
        }
    }
}


//============================================================
// File : RuntimeBridgeProvider.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\RuntimeBridge\RuntimeBridgeProvider.cs
//============================================================

using NP.Services.RuntimeBridge;

public static class RuntimeBridgeProvider
{
    private static IRuntimeBridge _bridge;

    public static IRuntimeBridge Current
    {
        get
        {
            if (_bridge == null)
            {
                _bridge =
                    new RuntimeBridgeClient();

                _bridge.EnsureRunning();
            }

            return _bridge;
        }
    }
}


//============================================================
// File : RuntimeBridgeSocket.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\RuntimeBridge\RuntimeBridgeSocket.cs
//============================================================

using WebSocketSharp;

namespace NP.Services.RuntimeBridge
{
    internal sealed class RuntimeBridgeSocket
    {
        private WebSocket _socket;

        public bool IsConnected
        {
            get
            {
                if (_socket == null)
                    return false;

                return _socket.ReadyState ==
                    WebSocketState.Open;
            }
        }

        //----------------------------------------

        public void Connect()
        {
            if (IsConnected)
                return;

            _socket =
                new WebSocket(
                    "ws://127.0.0.1:5050/bridge");

            _socket.Connect();
        }

        //----------------------------------------

        public void Disconnect()
        {
            if (_socket == null)
                return;

            _socket.Close();

            _socket = null;
        }

        //----------------------------------------

        public string Send(string json)
        {
            if (!IsConnected)
                return null;

            string result = null;

            _socket.OnMessage +=
                (s, e) =>
                {
                    result = e.Data;
                };

            _socket.Send(json);

            int timeout = 0;

            while (result == null &&
                   timeout < 500)
            {
                System.Threading.Thread.Sleep(10);

                timeout += 10;
            }

            return result;
        }
    }
}


//============================================================
// File : HttpServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\HttpServer.cs
//============================================================

using System;
using System.IO;
using System.Net;
using System.Threading;
using NP.Core.Models;
using NP.Services.Bridge;
using NP.Services.Commands;
using NP.Services.Common;
using NP.Services.Routing;
using System.Windows.Forms;

namespace NP.Services.Server
{
    public enum ServerOwner
    {
        Host,
        Extension
    }

    public class HttpServer : IRuntimeServer
    {
        private readonly MessageRouter _router;
        private readonly BridgeSessionService _bridgeSession;
        private HttpListener _listener;
        private readonly object _sync = new object();
        private bool _started;
        private ServerOwner _owner = ServerOwner.Host;
        public event RuntimeMessageReceivedHandler MessageReceived;
        //public bool IsRunning
        //{
        //    get
        //    {
        //        return _started;
        //    }
        //}

        public HttpServer(
            MessageRouter router,
            BridgeSessionService bridgeSession)
        {
            _router = router;
            _bridgeSession = bridgeSession;
        }

        public void Start()
        {
            Start(ServerOwner.Host);
        }

        public void Start(ServerOwner owner)
        {
            try
            {
                lock (_sync)
                {
                    if (_started)
                    {
                        _router.RouteLog(
                            "HTTP Server already running. Owner = " + owner);

                        return;
                    }

                    _owner = owner;
                    _listener = new HttpListener();
                    if (IsRunning)
                        return;
                    _listener.Prefixes.Add("http://localhost:5050/");
                    _listener.Start();
                    _started = true;
                    _router.RouteLog(
                        "Listener IsListening = " +
                        _listener.IsListening);

                    Thread thread = new Thread(ListenLoop);
                    thread.IsBackground = true;
                    thread.Start();
                    _router.RouteLog(
                        "HTTP Server Started. Owner = " + owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsRunning
        {
            get
            {
                _started = (_listener != null && _listener.IsListening);
                return _started;
            }
        }
        //public void Start()
        //{
        //    if (_listener != null)
        //        return;

        //    _listener = new HttpListener();

        //    _listener.Prefixes.Add(
        //        "http://localhost:5050/");

        //    _listener.Start();


        //    Thread thread =
        //        new Thread(ListenLoop);

        //    thread.IsBackground = true;

        //    thread.Start();

        //    _router.RouteLog(
        //        "HTTP Server Started");
        //}
        public bool IsServer
        {
            get
            {
                return true;
            }
        }

        private void WriteResponse(HttpListenerContext context,    string json)
        {
            using (StreamWriter writer =
                new StreamWriter(
                    context.Response.OutputStream))
            {
                writer.Write(json);
            }

            context.Response.Close();
        }

        private void ListenLoop()
        {
            while (_listener.IsListening)
            {
                try
                {
                    HttpListenerContext context =
                        _listener.GetContext();
                    
                    string path = context.Request.Url.AbsolutePath;

                    if (context.Request.HttpMethod == "GET")
                    {
                        if (path == "/bridge/context")
                        {
                            AiContext data =
                                _bridgeSession.GetContext();

                            string contextJson =
                                JsonHelper.Serialize(data);

                            WriteResponse(context, contextJson);
                            continue;
                        }
                    }


                    string body;

                    using (StreamReader reader =
                        new StreamReader(
                            context.Request.InputStream))
                    {
                        body = reader.ReadToEnd();
                    }

                    _router.RouteLog(
                        "Client Connected");

                    _router.RouteLog(
                        "Received : " + body);

                    //----------------------------------
                    // اگر Context کامل از VS آمده باشد
                    //----------------------------------

                    if (body.Contains("\"ProjectName\"") &&
                        body.Contains("\"SelectedCode\""))
                    {
                        AiContext aiContext =
                            JsonHelper.Deserialize<AiContext>(body);

                        _bridgeSession.SetContext(aiContext);

                        _router.RouteLog(
                            "Bridge Context Updated");
                    }
                    else
                    {
                        MessagePacket packet =
                            JsonHelper.Deserialize<MessagePacket>(body);



                        if (packet.payload != null &&packet.payload.ToolName == "bridge" &&    packet.payload.Action == "receive")
                        {
                            AiContext data =
                                _bridgeSession.GetContext();

                            string contextJson =
                                JsonHelper.Serialize(data);

                            WriteResponse(context, contextJson);
                            continue;  
                        }
                        _router.Process(packet);
                    }

                    ToolResponse response =
                        new ToolResponse();

                    response.Success = true;

                    response.Result =
                        "Bridge OK";

                    string responseJson =
                        JsonHelper.Serialize(response);

                    WriteResponse(context, responseJson);
                }
                catch (Exception ex)
                {
                    _router.RouteLog(
                        ex.ToString());
                }
            }
        }

        public void Stop()
        {
            if (_listener == null)
                return;

            _listener.Stop();

            _listener.Close();

            _listener = null;

            _started = false;
        }

        public void Send(string json)
        {
            //
            // بعداً برای Push به Clientها
            //
        }
    }
}


//============================================================
// File : IRuntimeServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\IRuntimeServer.cs
//============================================================

namespace NP.Services.Server
{
    public delegate void RuntimeMessageReceivedHandler(
        string message);

    public interface IRuntimeServer
    {
        bool IsRunning { get; }

        bool IsServer { get; }

        void Start();

        void Stop();

        void Send(string message);

        event RuntimeMessageReceivedHandler
            MessageReceived;
    }

    public enum RuntimeServerType
    {
        Http,
        WebSocket
    }
}


//============================================================
// File : LegacyWebSocketServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\LegacyWebSocketServer.cs
//============================================================

using NP.UI.Forms;
using NP.Services.Runtime;
using System;
using System.Windows.Forms;

namespace NP.Services.Server
{
    public class LegacyWebSocketServer
    {
        private readonly HostForm _form;

        public LegacyWebSocketServer(HostForm form)
        {
            _form = form;
        }

        public bool IsRunning { get; private set; }

        public int Port { get; private set; }

        public void Start(int port)
        {
            if (IsRunning)
                return;

            Port = port;
            IsRunning = true;

            _form.Log(string.Format("Server started on port {0}", port.ToString()));
        }

        public void Stop()
        {
            if (!IsRunning)
                return;

            IsRunning = false;

            _form.Log("Server stopped");
        }
    }
}


//============================================================
// File : PipeMessage.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\PipeMessage.cs
//============================================================

namespace NP.Services.Server
{
    public class PipeMessage
    {
        public string Sender
        {
            get;
            set;
        }

        public string Command
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}


//============================================================
// File : RuntimePipeClient.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\RuntimePipeClient.cs
//============================================================

using System.IO;
using System.IO.Pipes;
using System.Text;

namespace NP.Services.Server
{
    public class RuntimePipeClient
    {
        private const string PipeName =
            "NP.Runtime";

        public bool Send(string json)
        {
            try
            {
                using (NamedPipeClientStream pipe =
                    new NamedPipeClientStream(
                        ".",
                        PipeName,
                        PipeDirection.Out))
                {
                    pipe.Connect(300);

                    using (StreamWriter writer =
                        new StreamWriter(pipe))
                    {
                        writer.AutoFlush = true;

                        writer.WriteLine(json);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


//============================================================
// File : RuntimePipeServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\RuntimePipeServer.cs
//============================================================

using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace NP.Services.Server
{
    public class RuntimePipeServer :
        IRuntimeServer
    {
        private const string PipeName =
            "NP.Runtime";

        private Thread _thread;

        private bool _running;

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        public bool IsServer
        {
            get;
            private set;
        }

        public event RuntimeMessageReceivedHandler
            MessageReceived;

        public void Start()
        {
            if (_running)
                return;

            //----------------------------------
            // آیا سرور دیگری وجود دارد؟
            //----------------------------------

            RuntimePipeClient client =
                new RuntimePipeClient();

            if (client.Send("__PING__"))
            {
                IsServer = false;
                _running = true;
                return;
            }

            //----------------------------------
            // هیچ سروری نیست
            //----------------------------------

            IsServer = true;

            _running = true;

            _thread =
                new Thread(ServerLoop);

            _thread.IsBackground = true;

            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        public void Send(string json)
        {
            RuntimePipeClient client =
                new RuntimePipeClient();

            client.Send(json);
        }

        //----------------------------------------------------

        //private void ServerLoop()
        //{
        //    while (_running)
        //    {
        //        try
        //        {
        //            using (NamedPipeServerStream pipe =
        //                new NamedPipeServerStream(
        //                    PipeName,
        //                    PipeDirection.In))
        //            {
        //                pipe.WaitForConnection();

        //                using (StreamReader reader =
        //                    new StreamReader(pipe))
        //                {
        //                    string json =
        //                        reader.ReadLine();

        //                    if (!String.IsNullOrEmpty(json))
        //                    {
        //                        if (MessageReceived != null)
        //                        {
        //                            MessageReceived(json);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        private void ServerLoop()
        {
            while (_running)
            {
                NamedPipeServerStream pipe =
                    new NamedPipeServerStream(
                        PipeName,
                        PipeDirection.InOut,
                        NamedPipeServerStream.MaxAllowedServerInstances,
                        PipeTransmissionMode.Message,
                        PipeOptions.Asynchronous);

                pipe.BeginWaitForConnection(
                    PipeConnected,
                    pipe);
            }

            Thread.Sleep(50);
        }

        private void PipeConnected(
    IAsyncResult ar)
        {
            NamedPipeServerStream pipe =
                (NamedPipeServerStream)ar.AsyncState;

            try
            {
                pipe.EndWaitForConnection(ar);

                ThreadPool.QueueUserWorkItem(
                    ReadPipe,
                    pipe);
            }
            catch
            {
                pipe.Dispose();
            }
        }

        private void ReadPipe(object state)
        {
            NamedPipeServerStream pipe =
                (NamedPipeServerStream)state;

            try
            {
                using (StreamReader reader =
                    new StreamReader(pipe))
                {
                    string json =
                        reader.ReadLine();

                    if (!String.IsNullOrEmpty(json))
                    {
                        if (MessageReceived != null)
                        {
                            MessageReceived(json);
                        }
                    }
                }
            }
            catch
            {
            }

            try
            {
                pipe.Dispose();
            }
            catch
            {
            }
        }


    }
}


//============================================================
// File : RuntimeServerService.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\RuntimeServerService.cs
//============================================================

using NP.Services.Bridge;
using NP.Services.Routing;
using WebSocketSharp;

namespace NP.Services.Server
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


//============================================================
// File : RuntimeSocketBehavior.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\RuntimeSocketBehavior.cs
//============================================================

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebSocketSharp;
//using WebSocketSharp.Server;

//namespace NP.Services.Server
//{

//private class BridgeBehavior :
//    WebSocketBehavior
//{
//    private readonly RuntimeSocketServer _runtime;

//    public BridgeBehavior(
//        RuntimeSocketServer runtime)
//    {
//        _runtime = runtime;
//    }

//    //--------------------------------------------------

//    protected override void OnOpen()
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Connected");
//        }

//        base.OnOpen();
//    }

//    //--------------------------------------------------

//    protected override void OnClose(
//        CloseEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                "WebSocket Closed");
//        }

//        base.OnClose(e);
//    }

//    //--------------------------------------------------

//    protected override void OnError(
//        WebSocketSharp.ErrorEventArgs e)
//    {
//        if (_runtime._router != null)
//        {
//            _runtime._router.RouteLog(
//                e.Message);
//        }

//        base.OnError(e);
//    }

//    //--------------------------------------------------

//    protected override void OnMessage(
//        MessageEventArgs e)
//    {
//        try
//        {
//            if (_runtime.MessageReceived != null)
//            {
//                _runtime.MessageReceived(
//                    e.Data);
//            }

//            if (_runtime._router != null)
//            {
//                _runtime._router.RouteLog(
//                    "WS <= " + e.Data);
//            }

//            //------------------------------------------------
//            // فعلاً فقط Echo
//            //------------------------------------------------

//            Send("OK");
//        }
//        catch (Exception ex)
//        {
//            Send(ex.ToString());
//        }
//    }
//}



//============================================================
// File : RuntimeSocketServer.cs
// Path : E:\NotSaved\Projects\NP.SDK\references\legacy\NP.RuntimeStudio\NP.Services\Server\RuntimeSocketServer.cs
//============================================================

using NP.Services.Bridge;
using NP.Services.ChromeBridge;
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

namespace NP.Services.Server
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

                if (packet == null)
                {
                    Send("INVALID_PACKET");
                    return;
                }

                if (packet.payload != null &&
                    packet.payload.ToolName == "bridge")
                {
                    switch (packet.payload.Action)
                    {
                        case "setContext":

                            _runtime._bridgeSession.SetContext(
                                packet.payload.Context);

                            Send("OK");
                            return;

                        case "receive":

                            AiContext context =
                                _runtime._bridgeSession.GetContext();

                            Send(
                                JsonHelper.Serialize(context));

                            return;
                    }
                }

                //if (_runtime._router != null)
                //{
                //    _runtime._router.Process(packet);
                //}

                //Send("OK");

                ChromeBridgeDispatcher
                .Current
                .Dispatch(packet);

                if (_runtime._router != null)
                {
                    _runtime._router.Process(packet);
                }

                Send("OK");
            }
        }
    }
}


