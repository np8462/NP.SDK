using NP.SDK.Core.Logging;
using System;

namespace NP.SDK.Server.Logging
{
    /// <summary>
    /// Runtime logger.
    /// </summary>
    public class RuntimeLogger
    {
        public event Action<string> LogWritten;
        //Logger _logs;

        public void Write(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            OnLogWritten(
                DateTime.Now.ToString("HH:mm:ss")
                + "  "
                + message);

            //_logs.Add(message);


            //if (LogWritten != null)
            //{
            //    LogWritten(message);
            //}
        }

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