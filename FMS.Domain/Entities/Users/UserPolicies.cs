namespace FMS.Domain.Entities.Users
{
    /// <summary>
    /// Authorization Policies for the application.
    /// The policies are configured in Startup.
    /// </summary>
    public static class UserPolicies
    {
        public const string FileCreatorOrEditor = "FileCreatorOrEditor";
    }
}