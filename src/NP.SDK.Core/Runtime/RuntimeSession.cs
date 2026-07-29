using System;

namespace NP.SDK.Core.Runtime
{
    /// <summary>
    /// Represents a connected runtime session.
    /// </summary>
    public class RuntimeSession
    {
        public RuntimeSession()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Session identifier.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Session name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Creation time.
        /// </summary>
        public DateTime CreatedAt
        {
            get;
            private set;
        }

        /// <summary>
        /// Last activity time.
        /// </summary>
        public DateTime LastActivity
        {
            get;
            set;
        }

        /// <summary>
        /// Current runtime context.
        /// </summary>
        public RuntimeContext Context
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this session is connected.
        /// </summary>
        public bool Connected
        {
            get;
            set;
        }
    }
}