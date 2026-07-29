using System;
using NP.SDK.Contracts;
using NP.SDK.Core.Runtime;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Server.Dispatching
{
    /// <summary>
    /// Dispatches runtime messages to subscribers.
    /// </summary>
    public class MessageDispatcher : IMessageDispatcher
    {
        /// <summary>
        /// Raised when a message is dispatched.
        /// </summary>
        public event Action<RuntimeMessage> MessageReceived;

        /// <summary>
        /// Dispatches a runtime message.
        /// </summary>
        public void Dispatch(RuntimeMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            OnMessageReceived(message);
        }

        /// <summary>
        /// Dispatches a message by values.
        /// </summary>
        public void Dispatch(
            string command,
            string data)
        {
            RuntimeMessage message =
                new RuntimeMessage();

            message.Command = command;
            message.Data = data;

            Dispatch(message);
        }

        protected virtual void OnMessageReceived(
            RuntimeMessage message)
        {
            Action<RuntimeMessage> handler =
                MessageReceived;

            if (handler != null)
            {
                handler(message);
            }
        }
    }
}