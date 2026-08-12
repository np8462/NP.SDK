using System;
using NP.SDK.Contracts.Identity;
using NP.SDK.Contracts.Security;

namespace NP.SDK.Core.Authorization
{
    /// <summary>
    /// Provides authorization checks for NP.SDK operations.
    /// </summary>
    public class AuthorizationService
    {
        private readonly ISecurityAdapter _security;

        public AuthorizationService(
            ISecurityAdapter security)
        {
            if (security == null)
                throw new ArgumentNullException("security");

            _security = security;
        }

        public bool Can(
            IUser user,
            string operation,
            string resource)
        {
            SecurityContext context =
                new SecurityContext
                {
                    User = user,
                    Operation = operation,
                    Resource = resource
                };

            SecurityResult identity =
                _security.Authenticate(context);

            if (!identity.Success)
                return false;

            SecurityResult authorization =
                _security.Authorize(context);

            return authorization.Success;
        }
    }
}

//using NP.SDK.Contracts.Identity;
//using NP.SDK.Contracts.Security;

//namespace NP.SDK.Core.Authorization
//{
//    /// <summary>
//    /// Provides authorization services for NP.SDK users.
//    /// </summary>
//    public class AuthorizationService
//    {
//        private readonly ISecurityAdapter _security;

//        /// <summary>
//        /// Creates a new authorization service.
//        /// </summary>
//        public AuthorizationService(
//            ISecurityAdapter security)
//        {
//            _security = security;
//        }

//        /// <summary>
//        /// Determines whether a user is allowed to perform
//        /// the specified operation on the specified resource.
//        /// </summary>
//        public bool Can(
//            IUser user,
//            string operation,
//            string resource)
//        {
//            if (user == null)
//                return false;

//            if (string.IsNullOrEmpty(operation))
//                return false;

//            if (string.IsNullOrEmpty(resource))
//                return false;

//            SecurityContext context =
//                new SecurityContext
//                {
//                    User = user,
//                    Operation = operation,
//                    Resource = resource
//                };

//            // -----------------------------------------
//            // Identity
//            // -----------------------------------------

//            if (_security == null)
//                return false;

//            SecurityResult identity =
//                _security.Authenticate(context);

//            if (identity == null ||
//                !identity.Success)
//            {
//                return false;
//            }

//            // -----------------------------------------
//            // Authorization
//            // -----------------------------------------

//            SecurityResult authorization =
//                _security.Authorize(context);

//            if (authorization == null ||
//                !authorization.Success)
//            {
//                return false;
//            }

//            return true;
//        }
//    }
//}