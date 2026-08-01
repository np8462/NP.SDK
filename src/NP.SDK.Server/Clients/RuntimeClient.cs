using System;
using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Server.Clients
{
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

            Connected = true;
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


        public void Disconnect()
        {
            Connected = false;
        }


        public void Send(
            RuntimeMessage message)
        {
            if (!Connected)
                return;


            _transport.Send(message);
        }
    }
}