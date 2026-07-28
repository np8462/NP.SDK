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