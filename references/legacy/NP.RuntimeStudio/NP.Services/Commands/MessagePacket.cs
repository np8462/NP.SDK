namespace NP.Services.Commands
{
    public enum BridgeAction
    {
        None,
        SetContext,
        Receive,
        SendFile,
        AskAI,
        InsertCode,
        ExecuteCommand,
        Ping
    }

    public class MessagePacket
    {
        public string id { get; set; }

        public string sessionId { get; set; }

        public string source { get; set; }

        public string target { get; set; }

        public string type { get; set; }

        public BridgeAction Action { get; set; }

        public ToolRequest payload { get; set; }

        //public AiContext Context { get; set; }
    }
}