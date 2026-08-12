namespace NP.SDK.Contracts.Security
{
    /// <summary>
    /// Provides the underlying security mechanism.
    /// </summary>
    public interface ISecurityProvider
    {
        SecurityResult ValidateIdentity(
            SecurityContext context);

        SecurityResult ValidateAuthorization(
            SecurityContext context);

        SecurityResult ProtectTransport(
            SecurityContext context);

        string CreateToken(
            SecurityContext context);

        SecurityResult ValidateToken(
            SecurityContext context);
    }
}