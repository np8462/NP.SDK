using System;
using System.Collections.Generic;
using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Server.Dispatching
{
    /// <summary>
    /// Dispatches runtime commands to registered handlers.
    /// </summary>
    public class CommandDispatcher
    {
        private readonly Dictionary<string, ICommandHandler> _handlers;

        public CommandDispatcher()
        {
            _handlers =
                new Dictionary<string, ICommandHandler>(
                    StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Registers a command handler.
        /// </summary>
        public void Register(
            ICommandHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (String.IsNullOrWhiteSpace(handler.Command))
                throw new ArgumentException(
                    "Handler command cannot be empty.",
                    "handler");

            _handlers[handler.Command] = handler;
        }

        /// <summary>
        /// Removes a registered handler.
        /// </summary>
        public bool Unregister(
            string command)
        {
            if (String.IsNullOrWhiteSpace(command))
                return false;

            return _handlers.Remove(command);
        }

        /// <summary>
        /// Returns true if the command exists.
        /// </summary>
        public bool Contains(
            string command)
        {
            if (String.IsNullOrWhiteSpace(command))
                return false;

            return _handlers.ContainsKey(command);
        }

        /// <summary>
        /// Executes a runtime command.
        /// </summary>
        public bool Dispatch(
            RuntimeMessage message)
        {
            if (message == null)
                return false;

            ICommandHandler handler;

            if (!_handlers.TryGetValue(
                    message.Command,
                    out handler))
            {
                return false;
            }

            handler.Execute(message);

            return true;
        }

        /// <summary>
        /// Removes all registered handlers.
        /// </summary>
        public void Clear()
        {
            _handlers.Clear();
        }

        /// <summary>
        /// Gets the number of registered handlers.
        /// </summary>
        public int Count
        {
            get
            {
                return _handlers.Count;
            }
        }

        /// <summary>
        /// Returns registered command names.
        /// </summary>
        public IEnumerable<string> Commands
        {
            get
            {
                return _handlers.Keys;
            }
        }
    }
}