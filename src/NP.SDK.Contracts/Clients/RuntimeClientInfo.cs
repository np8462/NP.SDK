using System;

namespace NP.SDK.Contracts.Clients
{
    /// <summary>
    /// Runtime client information.
    /// </summary>
    public class RuntimeClientInfo
    {
        public RuntimeClientInfo()
        {
            Id =
                Guid.NewGuid();

            ConnectedAt =
                DateTime.Now;
        }

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

        public string Transport
        {
            get;
            set;
        }

        public DateTime ConnectedAt
        {
            get;
            private set;
        }
    }
}