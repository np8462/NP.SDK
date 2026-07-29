using System;

namespace NP.SDK.Contracts
{
    /// <summary>
    /// Defines runtime server behavior.
    /// </summary>
    public interface IRuntimeServer
    {
        /// <summary>
        /// Indicates server state.
        /// </summary>
        bool IsRunning
        {
            get;
        }


        /// <summary>
        /// Starts runtime server.
        /// </summary>
        void Start();


        /// <summary>
        /// Stops runtime server.
        /// </summary>
        void Stop();
    }
}