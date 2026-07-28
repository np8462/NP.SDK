using System.Collections.Generic;
using NP.SDK.Contracts.Identity.Enums;

namespace NP.SDK.Contracts.Identity
{
    /// <summary>
    /// Defines a user within NP.SDK.
    /// </summary>
    public interface IUser
    {
        string Id { get; }

        string UserName { get; }

        string DisplayName { get; }

        UserStatus Status { get; }

        IReadOnlyList<IRole> Roles { get; }
    }
}
