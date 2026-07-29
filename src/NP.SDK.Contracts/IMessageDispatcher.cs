using System;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Contracts
{
    public interface IMessageDispatcher
    {
        event Action<RuntimeMessage> MessageReceived;


        void Dispatch(
            RuntimeMessage message);


        void Dispatch(
            string command,
            string data);
    }
}