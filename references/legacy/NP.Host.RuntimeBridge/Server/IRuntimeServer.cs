namespace NP.Host.RuntimeBridge.Server
{
    public delegate void RuntimeMessageReceivedHandler(
        string message);

    public interface IRuntimeServer
    {
        bool IsRunning { get; }

        bool IsServer { get; }

        void Start();

        void Stop();

        void Send(string message);

        event RuntimeMessageReceivedHandler
            MessageReceived;
    }

    public enum RuntimeServerType
    {
        Http,
        WebSocket
    }
}