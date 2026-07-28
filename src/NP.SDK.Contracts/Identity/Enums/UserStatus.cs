namespace NP.SDK.Contracts.Identity.Enums
{
    /// <summary>
    /// Represents the current status of a user.
    /// Numeric values are explicitly assigned to keep them stable.
    /// </summary>
    public enum UserStatus
    {
        Unknown = 0,
        Active = 1,
        Disabled = 2,
        Locked = 3,
        Pending = 4
    }
}