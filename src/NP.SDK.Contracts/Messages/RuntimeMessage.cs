using System;

namespace NP.SDK.Contracts.Messages
{
    /// <summary>
    /// Represents a runtime communication message.
    /// </summary>
    public class RuntimeMessage
    {
        public RuntimeMessage()
        {
            Id =
                Guid.NewGuid();

            CreatedAt =
                DateTime.Now;
        }


        /// <summary>
        /// Unique message identifier.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }


        /// <summary>
        /// Command name.
        /// Example: ping, execute, bridge
        /// </summary>
        public string Command
        {
            get;
            set;
        }


        /// <summary>
        /// Message payload data.
        /// </summary>
        public string Data
        {
            get;
            set;
        }


        /// <summary>
        /// Message source.
        /// Example:
        /// HTTP
        /// WebSocket
        /// Chrome
        /// VisualStudio
        /// </summary>
        public string Source
        {
            get;
            set;
        }


        /// <summary>
        /// Related runtime session.
        /// </summary>
        public Guid? SessionId
        {
            get;
            set;
        }


        /// <summary>
        /// Message creation time.
        /// </summary>
        public DateTime CreatedAt
        {
            get;
            private set;
        }
    }
}