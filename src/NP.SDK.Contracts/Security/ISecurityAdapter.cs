namespace NP.SDK.Contracts.Security
{
    /// <summary>
    /// Adapts NP.SDK security operations
    /// to a security provider.
    /// </summary>
    public interface ISecurityAdapter
    {
        SecurityResult Authenticate(
            SecurityContext context);

        SecurityResult Authorize(
            SecurityContext context);

        SecurityResult ValidateTransport(
            SecurityContext context);

        string CreateToken(
            SecurityContext context);

        SecurityResult ValidateToken(
            SecurityContext context);
    }
}