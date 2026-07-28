using NP.Services.Bridge;
using System;

namespace NP.Services.Commands
{
    public class ToolRequest
    {
        public string ToolName { get; set; }

        public string Action { get; set; }

        public string Data { get; set; }

        public string Content { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }

        public string PageTitle { get; set; }

        public DateTime? Time { get; set; }

        public AiContext Context { get; set; }
    }
}