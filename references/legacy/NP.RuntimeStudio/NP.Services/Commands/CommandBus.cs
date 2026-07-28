using System;

namespace NP.Services.Commands
{
    public class CommandBus
    {
        public event Action<CommandPacket> CommandReceived;

        public void Send(CommandPacket command)
        {
            if (CommandReceived != null)
                CommandReceived(command);
        }
    }
}