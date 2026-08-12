using NP.SDK.Contracts.Identity;
using System;

namespace NP.SDK.Contracts.Security
{
    /// <summary>
    /// Describes the security context of an operation.
    /// </summary>
    public class SecurityContext
    {
        public IUser User { get; set; }

        public string Operation { get; set; }

        public string Resource { get; set; }

        public string Credential { get; set; }

        public string SecurityToken { get; set; }

        public bool IsTestMode { get; set; }

        public SecurityContext()
        {
            IsTestMode = false;
        }

    //    private bool HasPermission(
    //IUser user,
    //string permissionName)
    //    {
    //        if (user == null)
    //            return false;

    //        if (String.IsNullOrEmpty(permissionName))
    //            return false;

    //        foreach (IRole role in user.Roles)
    //        {
    //            if (role == null)
    //                continue;

    //            foreach (IPermission permission in role.Permissions)
    //            {
    //                if (permission == null)
    //                    continue;

    //                if (String.Equals(
    //                    permission.Name,
    //                    permissionName,
    //                    StringComparison.OrdinalIgnoreCase))
    //                {
    //                    return true;
    //                }
    //            }
    //        }

    //        return false;
    //    }
    }
}