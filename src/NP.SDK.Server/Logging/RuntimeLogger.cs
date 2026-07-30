using System;
using System.Collections.Generic;

namespace NP.SDK.Server.Logging
{
    /// <summary>
    /// Runtime logger.
    /// </summary>
    public class RuntimeLogger
    {
        private readonly List<string> _items =
            new List<string>();

        /// <summary>
        /// Raised whenever a new log is written.
        /// </summary>
        public event Action<string> LogWritten;

        /// <summary>
        /// Gets all runtime logs.
        /// </summary>
        public IEnumerable<string> Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Writes a runtime log entry.
        /// </summary>
        public void Write(
            string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string line =
                DateTime.Now.ToString("HH:mm:ss")
                + "  "
                + message;

            _items.Add(line);

            OnLogWritten(line);
        }

        /// <summary>
        /// Clears all logs.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Raises LogWritten event.
        /// </summary>
        protected virtual void OnLogWritten(
            string message)
        {
            Action<string> handler =
                LogWritten;

            if (handler != null)
            {
                handler(message);
            }
        }
    }
}