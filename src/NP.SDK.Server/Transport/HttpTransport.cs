using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using NP.SDK.Core.Runtime;
using NP.SDK.Server.Dispatching;
using NP.SDK.Contracts.Messages;
using NP.SDK.Contracts;
using System.Web.Script.Serialization;

namespace NP.SDK.Server.Transport
{
    /// <summary>
    /// HTTP runtime transport.
    /// </summary>
    public class HttpTransport : IRuntimeTransport
    {
        public event Action<string> DataReceived;
        public event Action<RuntimeMessage> MessageReceived;

        private HttpListener _listener;

        private Thread _thread;

        private bool _running;

        public HttpTransport()
        {
        }

        //public HttpTransport(
        //    MessageDispatcher dispatcher)
        //{
        //    Dispatcher = dispatcher;
        //}

        //public MessageDispatcher Dispatcher
        //{
        //    get;
        //    private set;
        //}

        public string Prefix
        {
            get;
            set;
        }

        public bool IsRunning
        {
            get
            {
                return _running;
            }
        }

        public void Start()
        {
            if (_running)
                return;

            if (String.IsNullOrWhiteSpace(Prefix))
            {
                Prefix =
                    "http://127.0.0.1:5050/";
            }

            _listener =
                new HttpListener();

            _listener.Prefixes.Add(Prefix);

            _listener.Start();

            _running = true;

            _thread =
                new Thread(ListenLoop);

            _thread.IsBackground = true;

            _thread.Start();
        }

        public void Stop()
        {
            _running = false;

            if (_listener != null)
            {
                _listener.Stop();

                _listener.Close();

                _listener = null;
            }
        }

        private void ListenLoop()
        {
            while (_running)
            {
                try
                {
                    HttpListenerContext context =
                        _listener.GetContext();

                    ProcessRequest(context);
                }
                catch (HttpListenerException)
                {
                    if (!_running)
                        break;

                    throw;
                }
                catch (ObjectDisposedException)
                {
                    if (!_running)
                        break;

                    throw;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);

                    Console.WriteLine(
                        ex.Message);
                }
            }
        }

        private void ProcessRequest(
            HttpListenerContext context)
        {
            string body;

            using (StreamReader reader =
                new StreamReader(
                    context.Request.InputStream))
            {
                body =
                    reader.ReadToEnd();
            }

            //RuntimeMessage message =
            //    new RuntimeMessage();

            //message.Command = "http";
            //message.Source = "HttpTransport";
            //message.Data = body;

            RuntimeMessage message = null;

            try
            {
                JavaScriptSerializer serializer =
                    new JavaScriptSerializer();

                message =
                    serializer.Deserialize<RuntimeMessage>(
                        body);
            }
            catch
            {
            }

            if (message == null)
            {
                message = new RuntimeMessage();

                message.Command = "http";
                
                if (String.IsNullOrWhiteSpace(message.Source))
                {
                    message.Source = "HttpTransport";
                }

                message.Data = body;
            }

            OnMessageReceived(message);

            WriteResponse(
                context,
                "{\"success\":true}");
        }

        protected virtual void OnMessageReceived(
    RuntimeMessage message)
        {
            Action<RuntimeMessage> handler =
                MessageReceived;

            if (handler != null)
            {
                handler(message);
            }
        }

        private void WriteResponse(
            HttpListenerContext context,
            string json)
        {
            byte[] buffer =
                Encoding.UTF8.GetBytes(json);

            context.Response.ContentType =
                "application/json";

            context.Response.ContentLength64 =
                buffer.Length;

            context.Response.OutputStream.Write(
                buffer,
                0,
                buffer.Length);

            context.Response.OutputStream.Close();
        }
        
        public void Send(string data)
        {
            // فعلاً برای پاسخ‌دهی بعدی استفاده می‌شود
            // در نسخه بعدی می‌توانیم Clientها را نگه داریم
            // و Push انجام دهیم
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
    }
}