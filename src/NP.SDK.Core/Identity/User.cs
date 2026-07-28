using System.Collections.Generic;
using NP.SDK.Contracts.Identity;
using NP.SDK.Contracts.Identity.Enums;

namespace NP.SDK.Core.Identity
{
    /// <summary>
    /// Represents a user in NP.SDK.
    /// </summary>
    public class User : IUser
    {
        private readonly List<IRole> _roles;

        public string Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public UserStatus Status { get; set; }

        public IReadOnlyList<IRole> Roles
        {
            get { return _roles.AsReadOnly(); }
        }

        public User()
        {
            _roles = new List<IRole>();
            Status = UserStatus.Unknown;
        }

        public User(
            string id,
            string userName,
            string displayName)
            : this()
        {
            Id = id;
            UserName = userName;
            DisplayName = displayName;
        }

        public void AddRole(IRole role)
        {
            if (role == null)
                return;

            if (!_roles.Contains(role))
                _roles.Add(role);
        }

        public bool RemoveRole(IRole role)
        {
            if (role == null)
                return false;

            return _roles.Remove(role);
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}