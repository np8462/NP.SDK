using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NP.Services.Bridge;
using NP.Services.Common;
using NP.Services.Commands;    

namespace NP.Services.RuntimeBridge
{
    public sealed class RuntimeBridgeClient
        : IRuntimeBridge
    {
        readonly RuntimeBridgeSocket _socket;

        public RuntimeBridgeClient()
        {
            _socket =
                new RuntimeBridgeSocket();
        }

        //------------------------------------

        public bool IsConnected
        {
            get
            {
                return _socket.IsConnected;
            }
        }

        //------------------------------------

        public void EnsureRunning()
        {
            RuntimeBridgeLauncher.EnsureRunning();

            Connect();
        }

        //------------------------------------

        public void Connect()
        {
            _socket.Connect();
        }

        //------------------------------------

        public void Disconnect()
        {
            _socket.Disconnect();
        }

        //------------------------------------

        public void SetContext(
            AiContext context)
        {
            MessagePacket packet =
                new MessagePacket();

            packet.type =
                "bridge";

            packet.payload =
                new ToolRequest();

            packet.payload.ToolName =
                "bridge";

            packet.payload.Action =
                "setContext";

            packet.payload.Context =
                context;

            _socket.Send(
                JsonHelper.Serialize(packet));
        }

        //------------------------------------

        public AiContext GetContext()
        {
            MessagePacket packet =
                new MessagePacket();

            packet.type =
                "bridge";

            packet.payload =
                new ToolRequest();

            packet.payload.ToolName =
                "bridge";

            packet.payload.Action =
                "receive";

            string json =
                _socket.Send(
                    JsonHelper.Serialize(packet));

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonHelper.Deserialize<AiContext>(
                json);
        }

        //------------------------------------

        public string Send(
            MessagePacket packet)
        {
            return _socket.Send(
                JsonHelper.Serialize(packet));
        }
    }
}