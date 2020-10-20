using System.Collections.Generic;

namespace FMS.Domain.Entities.Users
{
    /// <summary>
    /// Authorization Roles for the application.
    /// The constant strings are stored in the database.
    /// </summary>
    public static class UserRoles
    {
        /// Roles
        public const string UserAdmin = "Administrator";
        public const string SiteMaintenance = "SiteMaintenance";
        public const string FileCreator = "FileCreator";
        public const string FileEditor = "FileEditor";

        public static readonly List<string> AllRoles = new List<string>
            {UserAdmin, SiteMaintenance, FileCreator, FileEditor};

        public static string DisplayName(string role) =>
            role switch
            {
                UserAdmin => "User Administrator",
                SiteMaintenance => "Site Maintenance",
                FileCreator => "File Creator",
                FileEditor => "File Editor",
                _ => role
            };

        public static string Description(string role) =>
            role switch
            {
                UserAdmin => "Users with the User Administrator role are able to add and remove " +
                    "roles for other users.",
                SiteMaintenance =>
                    "Users with the Site Maintenance role are able to update values in lookup " +
                    "tables (drop-down lists) and modify cabinets.",
                FileCreator =>
                    "Users with the File Creator role are able to add new facilities, files, " +
                    "and retention records.",
                FileEditor =>
                    "Users with the File Editor role are able to add and edit facilities, files, and " +
                    "retention records, and delete files.",
                _ => role
            };
    }

    /// <summary>
    /// Authorization Policies for the application.
    /// The policies are configured in Startup.
    /// </summary>
    public static class UserPolicies
    {
        // Policies
        public const string FileCreatorOrEditor = "FileCreatorOrEditor";
    }
}