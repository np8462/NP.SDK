using System;
using System.Collections.Generic;

namespace NP.SDK.Core.Runtime
{
    /// <summary>
    /// Represents the runtime execution context shared between
    /// clients, services and transports.
    /// </summary>
    public class RuntimeContext
    {
        public RuntimeContext()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;

            Metadata =
                new Dictionary<string, string>();
        }

        /// <summary>
        /// Context unique identifier.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreatedAt
        {
            get;
            private set;
        }

        /// <summary>
        /// Current project name.
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// Current file name.
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Full file path.
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Selected source code.
        /// </summary>
        public string SelectedCode
        {
            get;
            set;
        }

        /// <summary>
        /// Programming language.
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Additional runtime values.
        /// </summary>
        public IDictionary<string, string> Metadata
        {
            get;
            private set;
        }
    }
}