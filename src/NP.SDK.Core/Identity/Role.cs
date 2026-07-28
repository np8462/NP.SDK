using System.Collections.Generic;
using NP.SDK.Contracts.Identity;

namespace NP.SDK.Core.Identity
{
    /// <summary>
    /// Represents a role in NP.SDK.
    /// </summary>
    public class Role : IRole
    {
        private readonly List<IPermission> _permissions;

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IReadOnlyList<IPermission> Permissions
        {
            get { return _permissions.AsReadOnly(); }
        }

        public Role()
        {
            _permissions = new List<IPermission>();
        }

        public Role(
            string id,
            string name,
            string description)
            : this()
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public void AddPermission(IPermission permission)
        {
            if (permission == null)
                return;

            if (!_permissions.Contains(permission))
                _permissions.Add(permission);
        }

        public bool RemovePermission(IPermission permission)
        {
            if (permission == null)
                return false;

            return _permissions.Remove(permission);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}