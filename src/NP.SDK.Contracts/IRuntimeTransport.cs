using System;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Contracts
{
    public interface IRuntimeTransport
    {
        bool IsRunning
        {
            get;
        }

        //RuntimeSession Session
        //{
        //    get;
        //    set;
        //}

        void Start();


        void Stop();


        void Send(
            string data);


        void Send(
            RuntimeMessage message);


        event Action<string> DataReceived;
    }
}