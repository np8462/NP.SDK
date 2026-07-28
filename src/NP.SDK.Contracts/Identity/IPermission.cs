using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NP.SDK.Contracts.Identity
{
    /// <summary>
    /// Defines a permission that can be assigned to a role.
    /// </summary>
    public interface IPermission
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }
    }
}

