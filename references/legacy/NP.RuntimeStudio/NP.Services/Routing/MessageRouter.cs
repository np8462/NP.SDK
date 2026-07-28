using NP.Services.Commands;
using NP.Services.Common;
using System.Windows.Forms;

namespace NP.Services.Routing
{
    public class MessageRouter
    {
        private Form _form;
        private CommandBus _commandBus;

        //public void Process(MessagePacket packet)
        //{
        //    CommandPacket cmd =
        //        CommandFactory.Create(packet);

        //    _commandBus.Send(cmd);
        //}
        public void Process(MessagePacket packet)
        {
            CommandPacket cmd = new CommandPacket
            {
                Source = "chrome",
                Target = "host",
                Command = packet.type,
                Data = JsonHelper.Serialize(packet.payload)
            };

            _commandBus.Send(cmd);
        }
        public MessageRouter(Form form, CommandBus commandBus)
        {
            _form = form;
            _commandBus = commandBus;
        }
        public void RouteLog(string message)
        {
            CommandPacket cmd = new CommandPacket();

            cmd.Command = "log";
            cmd.Data = message;

            _commandBus.Send(cmd);
        }

        //public void Process(MessagePacket packet)
        //{
        //    switch (packet.type)
        //    {
        //        case "test_message":
        //        case "tool_request":
        //        case "ai_prompt":
        //        case "open_document":
        //        case "compile_project":

        //            CommandPacket cmd = new CommandPacket();

        //            cmd.Source = "chrome";
        //            cmd.Target = "host";
        //            cmd.Command = "chrome_message";
        //            cmd.Data = packet.payload;

        //            _commandBus.Send(cmd);

        //            break;

        //        //case "tool_request":

        //        //    break;

        //        //case "open_document":

        //        //    break;

        //        //case "compile_project":

        //        //    break;

        //        //case "ai_prompt":

        //        //    break;

        //        default:

        //            RuntimeBuilder.Log("Unknown type : " + packet.type);

        //            break;
        //    }
        //}
    }
}