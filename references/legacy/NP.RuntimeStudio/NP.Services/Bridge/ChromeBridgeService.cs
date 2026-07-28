using NP.Services.Commands;
using NP.UI.Forms;
using NP.Services.Runtime;
using System.Windows.Forms;

namespace NP.Services.Bridge
{
    public class ChromeBridgeService
    {
        private HostForm _form;

        public ChromeBridgeService(HostForm form, CommandBus commandBus)
        {
            _form = form;

            commandBus.CommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(CommandPacket cmd)
        {
            if (cmd.Command != "chrome_message")
                return;

            _form.Log("ChromeBridge : " + cmd.Data);
        }
    }
}