using System;

namespace NP.SDK.Contracts
{
    /// <summary>
    /// Defines a runtime communication transport.
    /// </summary>
    public interface IRuntimeTransport
    {
        /// <summary>
        /// Indicates transport state.
        /// </summary>
        bool IsRunning
        {
            get;
        }


        /// <summary>
        /// Starts transport.
        /// </summary>
        void Start();


        /// <summary>
        /// Stops transport.
        /// </summary>
        void Stop();


        /// <summary>
        /// Sends data.
        /// </summary>
        void Send(string data);


        /// <summary>
        /// Raised when data received.
        /// </summary>
        event Action<string> DataReceived;
    }
}