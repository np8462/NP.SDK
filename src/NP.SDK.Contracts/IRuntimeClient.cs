using System;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Contracts
{
    /// <summary>
    /// Represents a connected runtime client.
    /// </summary>
    public interface IRuntimeClient
    {
        Guid Id
        {
            get;
        }

        string Name
        {
            get;
        }

        bool Connected
        {
            get;
        }

        void Send(
            RuntimeMessage message);
    }
}