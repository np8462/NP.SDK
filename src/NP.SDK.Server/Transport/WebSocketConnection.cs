using System;

namespace NP.SDK.Server.Transport
{
    /// <summary>
    /// Represents a WebSocket connection.
    /// </summary>
    public class WebSocketConnection
    {
        public WebSocketConnection()
        {
            Id = Guid.NewGuid();

            ConnectedAt = DateTime.Now;
        }

        public Guid Id
        {
            get;
            private set;
        }

        public string EndPoint
        {
            get;
            set;
        }

        public DateTime ConnectedAt
        {
            get;
            private set;
        }

        public DateTime LastActivity
        {
            get;
            set;
        }

        public bool Connected
        {
            get;
            set;
        }
    }
}