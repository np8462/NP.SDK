using System;
using NP.SDK.Contracts.Logging;
using NP.SDK.Core.Logging;

namespace NP.SDK.Sandbox.Tests
{
    public static class LoggingTest
    {
        public static void Run()
        {
            ILogger logger = new Logger();

            logger.Debug("Debug message");
            logger.Info("Application started.");
            logger.Warning("This is a warning.");

            try
            {
                int x = 10;
                int y = 0;

                int result = x / y;
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred.", ex);
            }
        }
    }
}