using NP.SDK.Contracts;
using NP.SDK.Contracts.Messages;

namespace NP.SDK.Server.Dispatching
{
    public class PingCommandHandler :
        ICommandHandler
    {
        public string Command
        {
            get
            {
                return "ping";
            }
        }

        public void Execute(
            RuntimeMessage message)
        {
            System.Diagnostics.Debug.WriteLine(
                "PING : "
                + message.Data);
        }
    }

    public class TestCommandHandler :
    ICommandHandler
    {
        public string Command
        {
            get
            {
                return "test";
            }
        }

        public void Execute(
            RuntimeMessage message)
        {
            System.Diagnostics.Debug.WriteLine(
                "TEST RECEIVED : "
                + message.Data);
        }
    }
}