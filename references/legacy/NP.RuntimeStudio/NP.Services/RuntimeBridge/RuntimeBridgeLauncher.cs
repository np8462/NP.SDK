using System;
using System.Diagnostics;
using System.IO;
using System.Configuration; 

namespace NP.Services.RuntimeBridge
{
    public static class RuntimeBridgeLauncher
    {
        public static bool IsRunning()
        {
            Process[] list =
                Process.GetProcessesByName(
                    "NP.Host.RuntimeBridge");

            return list.Length > 0;
        }

        //------------------------------------------------

        public static void EnsureRunning()
        {
            if (IsRunning())
                return;

            Start();
        }

        //------------------------------------------------
        //public static void Start()
        //{
        //    if (ProcessExists())
        //        return;

        //    if (string.IsNullOrEmpty(RuntimeBridgePath))
        //        throw new InvalidOperationException(
        //            "RuntimeBridgePath is not initialized.");

        //    Process.Start(RuntimeBridgePath);
        //}
        public static void Start()
        {
            //string exe =
            //    Path.Combine(
            //        AppDomain.CurrentDomain.BaseDirectory,
            //        "NP.Host.RuntimeBridge.exe");

            //string exe = ConfigurationManager.AppSettings["RuntimeBridgePath"];
            string exe = RuntimeBridgeLauncher.RuntimeBridgePath;
            Process.Start(exe);
        }

        public static string RuntimeBridgePath
        {
            get;
            set;
        }
    }
}