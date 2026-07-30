using System;
using System.Collections.Generic;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Server.Dispatching
{
    /// <summary>
    /// Executes runtime commands.
    /// </summary>
    public class CommandDispatcher
    {
        private readonly Dictionary<
            string,
            Action<RuntimeMessage>> _handlers =
                new Dictionary<
                    string,
                    Action<RuntimeMessage>>();

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        public void Register(
            string command,
            Action<RuntimeMessage> handler)
        {
            if (String.IsNullOrWhiteSpace(command))
                throw new ArgumentNullException("command");

            if (handler == null)
                throw new ArgumentNullException("handler");

            _handlers[command] = handler;
        }

        /// <summary>
        /// Executes a runtime command.
        /// </summary>
        public bool Execute(
            RuntimeMessage message)
        {
            if (message == null)
                return false;

            Action<RuntimeMessage> handler;

            if (_handlers.TryGetValue(
                message.Command,
                out handler))
            {
                handler(message);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a registered command.
        /// </summary>
        public bool Unregister(
            string command)
        {
            return _handlers.Remove(command);
        }

        /// <summary>
        /// Clears all command handlers.
        /// </summary>
        public void Clear()
        {
            _handlers.Clear();
        }
    }
}