using System;

namespace NP.SDK.Server.Hosting
{
    /// <summary>
    /// Runtime host.
    /// Responsible for starting and stopping the runtime.
    /// </summary>
    public class RuntimeBridgeHost
    {
        public RuntimeBridgeHost(RuntimeHostOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            Options = options;
        }

        /// <summary>
        /// Runtime configuration.
        /// </summary>
        public RuntimeHostOptions Options
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether runtime is running.
        /// </summary>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes runtime resources.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Starts runtime.
        /// </summary>
        public virtual void Start()
        {
            if (IsRunning)
                return;

            Initialize();

            switch (Options.Mode)
            {
                case RuntimeMode.Server:

                    StartServer();

                    break;

                case RuntimeMode.Client:

                    StartClient();

                    break;

                default:

                    throw new InvalidOperationException("Unknown runtime mode.");
            }

            IsRunning = true;
        }

        /// <summary>
        /// Stops runtime.
        /// </summary>
        public virtual void Stop()
        {
            if (!IsRunning)
                return;

            StopInternal();

            IsRunning = false;
        }

        /// <summary>
        /// Restarts runtime.
        /// </summary>
        public virtual void Restart()
        {
            Stop();

            Start();
        }

        protected virtual void StartServer()
        {

        }

        protected virtual void StartClient()
        {

        }

        protected virtual void StopInternal()
        {

        }
    }
}