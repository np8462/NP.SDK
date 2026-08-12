using NP.SDK.Contracts.Security;
using NP.SDK.Security.Provider;

namespace NP.SDK.Security.Adapter
{
    /// <summary>
    /// Default adapter between NP.SDK security contracts
    /// and the default security provider.
    /// </summary>
    public class DefaultSecurityAdapter : ISecurityAdapter
    {
        private readonly ISecurityProvider _provider;

        public DefaultSecurityAdapter()
            : this(new DefaultSecurityProvider())
        {
        }

        public DefaultSecurityAdapter(
            ISecurityProvider provider)
        {
            _provider = provider;
        }

        public SecurityResult Authenticate(
            SecurityContext context)
        {
            return _provider.ValidateIdentity(context);
        }

        public SecurityResult Authorize(
            SecurityContext context)
        {
            return _provider.ValidateAuthorization(context);
        }

        public SecurityResult ValidateTransport(
            SecurityContext context)
        {
            return _provider.ProtectTransport(context);
        }

        public string CreateToken(
            SecurityContext context)
        {
            return _provider.CreateToken(context);
        }

        public SecurityResult ValidateToken(
            SecurityContext context)
        {
            return _provider.ValidateToken(context);
        }
    }
}

//using NP.SDK.Contracts.Security;

//namespace NP.SDK.Security.Adapter
//{
//    /// <summary>
//    /// Default adapter between NP.SDK and the configured
//    /// security provider.
//    /// 
//    /// This class intentionally contains only the adapter
//    /// responsibility and does not implement the actual
//    /// security mechanism.
//    /// </summary>
//    public class DefaultSecurityAdapter : ISecurityAdapter
//    {
//        private readonly ISecurityProvider _provider;

//        /// <summary>
//        /// Creates a new security adapter using the specified provider.
//        /// </summary>
//        /// <param name="provider">
//        /// Security provider used by this adapter.
//        /// </param>
//        public DefaultSecurityAdapter(
//            ISecurityProvider provider)
//        {
//            _provider = provider;
//        }

//        /// <summary>
//        /// Validates the identity of the current security context.
//        /// </summary>
//        public SecurityResult Authenticate(
//            SecurityContext context)
//        {
//            if (_provider == null)
//            {
//                return SecurityResult.Deny(
//                    "SEC100",
//                    "Security provider is not configured.");
//            }

//            return _provider.ValidateIdentity(context);
//        }

//        /// <summary>
//        /// Validates whether the current context is authorized
//        /// to perform the requested operation.
//        /// </summary>
//        public SecurityResult Authorize(
//            SecurityContext context)
//        {
//            if (_provider == null)
//            {
//                return SecurityResult.Deny(
//                    "SEC101",
//                    "Security provider is not configured.");
//            }

//            return _provider.ValidateAuthorization(context);
//        }

//        /// <summary>
//        /// Validates or protects the transport layer.
//        /// </summary>
//        public SecurityResult ValidateTransport(
//            SecurityContext context)
//        {
//            if (_provider == null)
//            {
//                return SecurityResult.Deny(
//                    "SEC102",
//                    "Security provider is not configured.");
//            }

//            return _provider.ProtectTransport(context);
//        }

//        /// <summary>
//        /// Creates a security token for the specified context.
//        /// </summary>
//        public string CreateToken(
//            SecurityContext context)
//        {
//            if (_provider == null)
//                return null;

//            return _provider.CreateToken(context);
//        }

//        /// <summary>
//        /// Validates a security token.
//        /// </summary>
//        public SecurityResult ValidateToken(
//            SecurityContext context)
//        {
//            if (_provider == null)
//            {
//                return SecurityResult.Deny(
//                    "SEC103",
//                    "Security provider is not configured.");
//            }

//            return _provider.ValidateToken(context);
//        }
//    }
//}