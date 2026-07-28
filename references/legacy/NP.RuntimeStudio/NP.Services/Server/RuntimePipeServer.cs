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