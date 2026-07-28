using NP.SDK.Contracts.Identity;

namespace NP.SDK.Core.Identity
{
    /// <summary>
    /// Represents a permission in NP.SDK.
    /// </summary>
    public class Permission : IPermission
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Permission()
        {
        }

        public Permission(
            string id,
            string name,
            string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}