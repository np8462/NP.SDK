using System;

namespace NP.SDK.Core.Runtime
{
    /// <summary>
    /// Represents the current state of a runtime session.
    /// </summary>
    public enum RuntimeSessionState
    {
        /// <summary>
        /// Session has been created but is not connected yet.
        /// </summary>
        Created = 0,

        /// <summary>
        /// Client is connected and session is active.
        /// </summary>
        Connected = 1,

        /// <summary>
        /// Client has disconnected.
        /// Session is still kept by the manager.
        /// </summary>
        Disconnected = 2,

        /// <summary>
        /// Session is no longer active and can be removed.
        /// </summary>
        Closed = 3
    }
}