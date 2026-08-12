namespace NP.SDK.Contracts.Security
{
    /// <summary>
    /// Represents the result of a security operation.
    /// </summary>
    public class SecurityResult
    {
        public bool Success { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public static SecurityResult Allow()
        {
            return new SecurityResult
            {
                Success = true,
                Code = "OK",
                Message = "Access granted."
            };
        }

        public static SecurityResult Deny(
            string code,
            string message)
        {
            return new SecurityResult
            {
                Success = false,
                Code = code,
                Message = message
            };
        }
    }
}