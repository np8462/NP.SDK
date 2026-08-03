using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;
using NP.SDK.Core.Runtime;
using System;

//namespace NP.SDK.Core.Client
namespace NP.SDK.Server.Clients
{
    /// <summary>
    /// Represents a runtime client.
    /// </summary>
    public class RuntimeClient :
        IRuntimeClient
    {
        private readonly IRuntimeTransport _transport;

        public RuntimeClient(
            string name,
            IRuntimeTransport transport)
        {
            Id = Guid.NewGuid();

            Name = name;

            _transport = transport;

            Connected = false;

            LastActivity = DateTime.Now;
        }

        public Guid Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool Connected
        {
            get;
            private set;
        }

        public RuntimeSession Session
        {
            get;
            internal set;
        }

        public void Connect()
        {
            Connected = true;

            LastActivity = DateTime.Now;
        }

        public void Disconnect()
        {
            Connected = false;
        }

        public void Send(
    RuntimeMessage message)
        {
            if (_transport == null)
            {
                return;
            }

            LastActivity = DateTime.Now;

            _transport.Send(message);
        }

        public IRuntimeTransport Transport
        {
            get
            {
                return _transport;
            }
        }

        public DateTime LastActivity
        {
            get;
            internal set;
        }
    }
}