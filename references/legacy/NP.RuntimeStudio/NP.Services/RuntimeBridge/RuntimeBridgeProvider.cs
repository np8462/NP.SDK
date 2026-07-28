using NP.Services.RuntimeBridge;

public static class RuntimeBridgeProvider
{
    private static IRuntimeBridge _bridge;

    public static IRuntimeBridge Current
    {
        get
        {
            if (_bridge == null)
            {
                _bridge =
                    new RuntimeBridgeClient();

                _bridge.EnsureRunning();
            }

            return _bridge;
        }
    }
}