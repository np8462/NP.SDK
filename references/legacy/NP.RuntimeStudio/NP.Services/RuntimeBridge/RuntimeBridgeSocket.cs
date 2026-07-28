using WebSocketSharp;

namespace NP.Services.RuntimeBridge
{
    internal sealed class RuntimeBridgeSocket
    {
        private WebSocket _socket;

        public bool IsConnected
        {
            get
            {
                if (_socket == null)
                    return false;

                return _socket.ReadyState ==
                    WebSocketState.Open;
            }
        }

        //----------------------------------------

        public void Connect()
        {
            if (IsConnected)
                return;

            _socket =
                new WebSocket(
                    "ws://127.0.0.1:5050/bridge");

            _socket.Connect();
        }

        //----------------------------------------

        public void Disconnect()
        {
            if (_socket == null)
                return;

            _socket.Close();

            _socket = null;
        }

        //----------------------------------------

        public string Send(string json)
        {
            if (!IsConnected)
                return null;

            string result = null;

            _socket.OnMessage +=
                (s, e) =>
                {
                    result = e.Data;
                };

            _socket.Send(json);

            int timeout = 0;

            while (result == null &&
                   timeout < 500)
            {
                System.Threading.Thread.Sleep(10);

                timeout += 10;
            }

            return result;
        }
    }
}