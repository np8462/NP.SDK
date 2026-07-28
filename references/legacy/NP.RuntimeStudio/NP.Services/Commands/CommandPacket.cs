namespace NP.Services.Commands
{
    public class CommandPacket
    {
        public string Id { get; set; }

        public string SessionId { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public string Command { get; set; }

        public string Data { get; set; }
    }
}