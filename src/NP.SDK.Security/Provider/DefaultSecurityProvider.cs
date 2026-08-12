using System;
using NP.SDK.Contracts.Identity;
using NP.SDK.Contracts.Identity.Enums;
using NP.SDK.Contracts.Security;

namespace NP.SDK.Security.Provider
{
    /// <summary>
    /// Default security provider used by NP.SDK.
    ///
    /// This provider contains the initial implementation of
    /// identity validation, authorization and transport security.
    ///
    /// The provider is intentionally replaceable through
    /// ISecurityProvider.
    /// </summary>
    public class DefaultSecurityProvider : ISecurityProvider
    {
        #region Identity

        public SecurityResult ValidateIdentity(
            SecurityContext context)
        {
            if (context == null)
            {
                return SecurityResult.Deny(
                    "SEC001",
                    "Security context is null.");
            }

            if (context.User == null)
            {
                return SecurityResult.Deny(
                    "SEC002",
                    "User is not authenticated.");
            }

            if (context.User.Status != UserStatus.Active)
            {
                return SecurityResult.Deny(
                    "SEC003",
                    "User is not active.");
            }

            return SecurityResult.Allow();
        }

        #endregion

        #region Authorization

        public SecurityResult ValidateAuthorization(
SecurityContext context)
        {
            if (context == null)
            {
                return SecurityResult.Deny(
                    "SEC003",
                    "Security context is null.");
            }

            if (context.User == null)
            {
                return SecurityResult.Deny(
                    "SEC004",
                    "User is not authenticated.");
            }

            if (context.User.Status !=
                NP.SDK.Contracts.Identity.Enums.UserStatus.Active)
            {
                return SecurityResult.Deny(
                    "SEC005",
                    "User is not active.");
            }

            if (String.IsNullOrEmpty(context.Operation))
            {
                return SecurityResult.Deny(
                    "SEC006",
                    "Operation is not specified.");
            }

            /*
             * Permission check
             */
            if (String.IsNullOrEmpty(context.Operation))
            {
                return SecurityResult.Deny(
                    "SEC006",
                    "Operation is not specified.");
            }

            if (String.IsNullOrEmpty(context.Resource))
            {
                return SecurityResult.Deny(
                    "SEC007",
                    "Resource is not specified.");
            }

            string permissionId =
                BuildPermissionId(
                    context.Operation,
                    context.Resource);

            if (!HasPermission(
                context.User,
                permissionId))
            {
                return SecurityResult.Deny(
                    "SEC008",
                    "User does not have the required permission.");
            }

            return SecurityResult.Allow();

        }
        //public SecurityResult ValidateAuthorization(
        //    SecurityContext context)
        //{
        //    if (context == null)
        //    {
        //        return SecurityResult.Deny(
        //            "SEC010",
        //            "Security context is null.");
        //    }

        //    if (context.User == null)
        //    {
        //        return SecurityResult.Deny(
        //            "SEC011",
        //            "User is not authenticated.");
        //    }

        //    if (context.User.Status != UserStatus.Active)
        //    {
        //        return SecurityResult.Deny(
        //            "SEC012",
        //            "User is not active.");
        //    }

        //    if (String.IsNullOrEmpty(context.Operation))
        //    {
        //        return SecurityResult.Deny(
        //            "SEC013",
        //            "Operation is not specified.");
        //    }

        //    if (String.IsNullOrEmpty(context.Resource))
        //    {
        //        return SecurityResult.Deny(
        //            "SEC014",
        //            "Resource is not specified.");
        //    }

        //    string permissionId =
        //        BuildPermissionId(
        //            context.Operation,
        //            context.Resource);

        //    if (HasPermission(
        //        context.User,
        //        permissionId))
        //    {
        //        return SecurityResult.Allow();
        //    }

        //    return SecurityResult.Deny(
        //        "SEC015",
        //        "User does not have the required permission.");
        //}


        private bool HasPermission(
    IUser user,
    string permissionName)
        {
            if (user == null)
                return false;

            if (String.IsNullOrEmpty(permissionName))
                return false;

            foreach (IRole role in user.Roles)
            {
                if (role == null)
                    continue;

                foreach (IPermission permission
                    in role.Permissions)
                {
                    if (permission == null)
                        continue;

                    if (String.Equals(
                        permission.Name,
                        permissionName,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //private bool HasPermission(
        //    IUser user,
        //    string permissionId)
        //{
        //    if (user == null)
        //        return false;

        //    if (String.IsNullOrEmpty(permissionId))
        //        return false;

        //    foreach (IRole role in user.Roles)
        //    {
        //        if (role == null)
        //            continue;

        //        foreach (IPermission permission
        //            in role.Permissions)
        //        {
        //            if (permission == null)
        //                continue;

        //            if (String.Equals(
        //                permission.Id,
        //                permissionId,
        //                StringComparison.OrdinalIgnoreCase))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

        private string BuildPermissionId(
            string operation,
            string resource)
        {
            if (String.IsNullOrEmpty(operation))
                return resource;

            if (String.IsNullOrEmpty(resource))
                return operation;

            return resource + "." + operation;
        }

        #endregion

        #region Transport

        public SecurityResult ProtectTransport(
            SecurityContext context)
        {
            if (context == null)
            {
                return SecurityResult.Deny(
                    "SEC020",
                    "Security context is null.");
            }

            /*
             * Initial implementation.
             *
             * Real transport protection such as TLS,
             * certificate validation and secure WebSocket
             * handling will be attached here later.
             */

            return SecurityResult.Allow();
        }

        #endregion

        #region Token

        public string CreateToken(
            SecurityContext context)
        {
            if (context == null)
                return null;

            if (context.User == null)
                return null;

            return context.User.Id;
        }

        public SecurityResult ValidateToken(
            SecurityContext context)
        {
            if (context == null)
            {
                return SecurityResult.Deny(
                    "SEC030",
                    "Security context is null.");
            }

            if (String.IsNullOrEmpty(
                context.SecurityToken))
            {
                return SecurityResult.Deny(
                    "SEC031",
                    "Security token is missing.");
            }

            return SecurityResult.Allow();
        }

        #endregion


    }
}