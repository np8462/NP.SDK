using System;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(
                new RuntimeApplicationContext());
        }
    }
}