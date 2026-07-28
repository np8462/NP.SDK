using NP.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NP.Host.RuntimeBridge
{
    public interface IRuntimeBridgeClient
    {
        bool IsConnected
        {
            get;
        }

        void EnsureRunning();

        void SetContext(
            AiContext context);

        AiContext GetContext();

        void Send(
            MessagePacket packet);
    }
}
