using System.Collections.Generic;

namespace NP.SDK.Contracts.Identity
{
    /// <summary>
    /// Defines a user role and its permissions.
    /// </summary>
    public interface IRole
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }

        IReadOnlyList<IPermission> Permissions { get; }
    }
}
