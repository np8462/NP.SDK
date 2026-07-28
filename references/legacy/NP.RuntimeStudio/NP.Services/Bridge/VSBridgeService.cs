using System;
using NP.Services.Commands;
using NP.Services.Common;

namespace NP.Services.Bridge
{
    public class VSBridgeService
    {
        public event Action<string> OnInsertText;

        public void Handle(CommandPacket cmd)
        {
            if (cmd.Target != "vs2012")
                return;

            if (cmd.Command != "vs_command")
                return;

            VsCommand payload =
                JsonHelper.Deserialize<VsCommand>(cmd.Data);

            if (payload.Command == "insert_text")
            {
                if (OnInsertText != null)
                {
                    OnInsertText(payload.Data);
                }
            }
        }
    }
}

//using NP.Host.Core;
//using NP.Services.Commands;
//using System;

//    public class VSBridgeService
//    {
//        public event Action<string> OnInsertText;

//        public void Handle(CommandPacket cmd)
//        {
//            if (cmd.Target != "vs2012")
//                return;

//            if (cmd.Command == "vs_command")
//            {
//                var payload =
//                    JsonHelper.Deserialize<VsCommand>(cmd.Data);

//                if (payload.Command == "insert_text")
//                {
//                    OnInsertText?.Invoke(payload.Data);
//                }
//            }
//        }
//    }



/*
using NP.Host.Core;
using NP.Services.Commands;

namespace NP.Host.Services
{
    public class VSBridgeService
    {
        private MainForm _form;

        public VSBridgeService(MainForm form, CommandBus commandBus)
        {
            _form = form;

            commandBus.CommandReceived += OnCommandReceived;
        }

        private void OnCommandReceived(CommandPacket cmd)
        {
            if (cmd.Target != "vs2012")
                return;

            RuntimeBuilder.Log("VSBridge : " + cmd.Command);
        }
    }
}
*/