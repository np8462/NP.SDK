using System;

namespace NP.SDK.Contracts.Logging
{
    /// <summary>
    /// Provides basic logging functionality for NP.SDK components.
    /// </summary>
    public interface ILogger
    {
        void Debug(string message);

        void Info(string message);

        void Warning(string message);

        void Error(string message);

        void Error(string message, Exception exception);
    }
}