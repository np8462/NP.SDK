using System;

namespace NP.SDK.Server.Hosting
{
    /// <summary>
    /// Runtime host configuration.
    /// </summary>
    public class RuntimeHostOptions
    {
        public RuntimeHostOptions()
        {
            Mode = RuntimeMode.Server;

            ListenAddress = "0.0.0.0";
            ListenPort = 5050;

            ServerAddress = "127.0.0.1";
            ServerPort = 5050;

            AutoStart = false;

            EnableLogging = true;
        }

        /// <summary>
        /// Runtime mode.
        /// </summary>
        public RuntimeMode Mode
        {
            get;
            set;
        }

        /// <summary>
        /// Automatically starts runtime.
        /// </summary>
        public bool AutoStart
        {
            get;
            set;
        }

        /// <summary>
        /// Enables logging.
        /// </summary>
        public bool EnableLogging
        {
            get;
            set;
        }

        // آدرسی که سرور روی آن Listen می‌کند
        public string ListenAddress { get; set; }

        // پورتی که سرور روی آن Listen می‌کند
        public int ListenPort { get; set; }

        // آدرس سروری که Client به آن متصل می‌شود
        public string ServerAddress { get; set; }

        // پورت سرور مقصد
        public int ServerPort { get; set; }

    }
}