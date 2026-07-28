using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NP.SDK.Core.Logging
{
    using System;
    using System.Diagnostics;
    using NP.SDK.Contracts.Logging;
    using System.IO;

    public class Logger : ILogger
    {
        public void Debug(string message)
        {
            Write(LogLevel.Debug, message, null);
        }

        public void Info(string message)
        {
            Write(LogLevel.Info, message, null);
        }

        public void Warning(string message)
        {
            Write(LogLevel.Warning, message, null);
        }

        public void Error(string message)
        {
            Write(LogLevel.Error, message, null);
        }

        public void Error(string message, Exception exception)
        {
            Write(LogLevel.Error, message, exception);
        }

        private void Write(
            LogLevel level,
            string message,
            Exception exception)
        {
            string text = string.Format(
                "[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}",
                DateTime.Now,
                level,
                message);

            if (exception != null)
            {
                text += Environment.NewLine + exception;
            }

            System.Diagnostics.Debug.WriteLine(text);
            WriteToJsonFile(text);
        }

        private void WriteToJsonFile(string text)
        {
            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, "NP.SDK.Log.json");

            string jsonEntry =
                "{\r\n" +
                "  \"Time\": \"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\r\n" +
                "  \"Message\": \"" + EscapeJson(text) + "\"\r\n" +
                "}";

            string existing = "[]";

            if (File.Exists(filePath))
                existing = File.ReadAllText(filePath, Encoding.UTF8);

            existing = existing.Trim();

            if (existing == "[]" || String.IsNullOrWhiteSpace(existing))
            {
                File.WriteAllText(
                    filePath,
                    "[\r\n" + jsonEntry + "\r\n]",
                    Encoding.UTF8);

                return;
            }

            if (existing.EndsWith("]"))
            {
                existing = existing.Substring(
                    0,
                    existing.Length - 1).TrimEnd();

                existing += ",\r\n" +
                             jsonEntry +
                             "\r\n]";

                File.WriteAllText(
                    filePath,
                    existing,
                    Encoding.UTF8);
            }
        }

        private string EscapeJson(string value)
        {
            if (value == null)
                return String.Empty;

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }
    }
}