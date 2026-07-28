using NP.Services.Bridge;
using NP.Services.Commands;

namespace NP.Services.RuntimeBridge
{
    public interface IRuntimeBridge
    {
        bool IsConnected
        {
            get;
        }

        void EnsureRunning();

        void Connect();

        void Disconnect();

        void SetContext(
            AiContext context);

        AiContext GetContext();

        string Send(
            MessagePacket packet);
    }
}