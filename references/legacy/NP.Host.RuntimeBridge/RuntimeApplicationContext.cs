using System;
using System.Drawing;
using System.Windows.Forms;

namespace NP.Host.RuntimeBridge
{
    public sealed class RuntimeApplicationContext
        : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;

        public RuntimeApplicationContext()
        {
            _notifyIcon =
                new NotifyIcon();

            CreateNotifyIcon();

            StartRuntimeBridge();
        }

        //--------------------------------------------------

        private void CreateNotifyIcon()
        {
            _notifyIcon.Icon =
                SystemIcons.Application;
            //_notifyIcon.Icon =
            //    Properties.Resources.App;

            _notifyIcon.Text =
                "NP Runtime Bridge";

            _notifyIcon.Visible =
                true;

            _notifyIcon.ContextMenu =
                new ContextMenu(
                    new MenuItem[]
                    {
                        new MenuItem(
                            "Status",
                            OnStatus),

                        new MenuItem("-"),

                        new MenuItem(
                            "Exit",
                            OnExit)
                    });
        }

        //--------------------------------------------------

        private void StartRuntimeBridge()
        {
            try
            {
                RuntimeBridgeBootstrap.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Runtime Bridge");
            }
        }

        //--------------------------------------------------

        private void OnStatus(
            object sender,
            EventArgs e)
        {
            //WebSocket _socket = new WebSocket(
            //        "ws://127.0.0.1:5050/bridge");

            MessageBox.Show(
                "Runtime Bridge is running.",
                "NP Runtime Bridge",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        //--------------------------------------------------

        private void OnExit(
            object sender,
            EventArgs e)
        {
            try
            {
                RuntimeBridgeBootstrap.Stop();

                _notifyIcon.Visible =
                    false;

                _notifyIcon.Dispose();
            }
            catch
            {
            }

            ExitThread();
        }
    }
}