/*
namespace NP.Host.Models
{
    public class ToolResponse
    {
        public bool Success { get; set; }

        public string Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
*/

namespace NP.Services.Commands
{
    public class ToolResponse
    {
        public bool Success { get; set; }

        public string Result { get; set; }

        public string Error { get; set; }
    }
}