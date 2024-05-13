using System.Collections.Immutable;

namespace FMS.Domain.Entities.Users
{
    /// <summary>
    /// Authorization Roles for the application.
    /// </summary>
    public static class UserRoles
    {
        // Roles
        // (These are the strings that are stored in the AppRoles table in the database.
        // Avoid modifying these strings.) 
        public const string UserMaintenance = "Administrator";
        public const string SiteMaintenance = "SiteMaintenance";
        public const string FileCreator = "FileCreator";
        public const string FileEditor = "FileEditor";

        public static readonly ImmutableArray<string> AllRoles =
            ImmutableArray.Create(UserMaintenance, SiteMaintenance, FileCreator, FileEditor);

        public static string DisplayName(string role) =>
            role switch
            {
                UserMaintenance => "User Maintenance",
                SiteMaintenance => "Site Maintenance",
                FileCreator => "Facility Creator",
                FileEditor => "Facility Editor",
                _ => role,
            };

        public static string Description(string role) =>
            role switch
            {
                UserMaintenance => "Users with the User Maintenance role are able to add and remove " + "roles for other users.",
                SiteMaintenance =>
                    "Users with the Site Maintenance role are able to update values in lookup " +
                    "tables (drop-down lists) and create new cabinets" + ", select different File Label, and edit Facility Number." ,
                FileCreator =>
                    "Users with the Facility Creator role are able to add new facilities, files, " +
                    "and retention records.",
                FileEditor =>
                    "Users with the Facility Editor role are able to add and edit facilities, files, and " +
                    "retention records, and delete files." + " Excludes edits to existing File Label and Facility Number.",
                _ => DisplayName(role),
            };
    }
}
