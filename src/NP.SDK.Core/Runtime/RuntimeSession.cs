using NP.SDK.Contracts;
using System;

namespace NP.SDK.Core.Runtime
{
    /// <summary>
    /// Represents a runtime session.
    /// </summary>
    public class RuntimeSession
    {
        public RuntimeSession()
        {
            Id = Guid.NewGuid();

            CreatedAt =
                DateTime.Now;

            LastActivity =
                CreatedAt;

            State =
                RuntimeSessionState.Created;
        }

        //--------------------------------------------------
        // Identity
        //--------------------------------------------------

        public Guid Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        //--------------------------------------------------
        // Client
        //--------------------------------------------------

        public IRuntimeClient Client
        {
            get;
            set;
        }

        public string Transport
        {
            get;
            set;
        }

        //--------------------------------------------------
        // State
        //--------------------------------------------------

        public RuntimeSessionState State
        {
            get;
            private set;
        }

        public bool Connected
        {
            get;
            private set;
        }

        //--------------------------------------------------
        // Time
        //--------------------------------------------------

        public DateTime CreatedAt
        {
            get;
            private set;
        }

        public DateTime ConnectedAt
        {
            get;
            private set;
        }

        public DateTime? DisconnectedAt
        {
            get;
            private set;
        }

        public DateTime LastActivity
        {
            get;
            private set;
        }

        //--------------------------------------------------
        // Context
        //--------------------------------------------------

        public RuntimeContext Context
        {
            get;
            set;
        }

        //--------------------------------------------------
        // Duration
        //--------------------------------------------------

        public TimeSpan Duration
        {
            get
            {
                if (ConnectedAt == DateTime.MinValue)
                {
                    return TimeSpan.Zero;
                }

                DateTime end =
                    DisconnectedAt ??
                    DateTime.Now;

                return end -
                    ConnectedAt;
            }
        }

        //--------------------------------------------------
        // State Management
        //--------------------------------------------------

        public void Connect()
        {
            Connected = true;

            State =
                RuntimeSessionState.Connected;

            ConnectedAt =
                DateTime.Now;

            LastActivity =
                ConnectedAt;

            DisconnectedAt = null;
        }

        public void Disconnect()
        {
            Connected = false;

            State =
                RuntimeSessionState.Disconnected;

            DisconnectedAt =
                DateTime.Now;

            LastActivity =
                DisconnectedAt.Value;
        }

        public void Close()
        {
            Connected = false;

            State =
                RuntimeSessionState.Closed;

            LastActivity =
                DateTime.Now;
        }

        public void UpdateActivity()
        {
            LastActivity =
                DateTime.Now;
        }
    }
}