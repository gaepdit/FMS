using System.Collections.Generic;

namespace FMS.Domain.Entities.Users
{
    public static class UserConstants
    {
        /// <summary>
        /// Users with the Administrator role are able to manage user profiles,
        /// update values in lookup tables (drop-down lists), and edit Cabinets.
        /// </summary>
        public const string AdminRole = "Administrator";

        /// <summary>
        /// Users with the Creator role are able to add new Facilities and Files.
        /// </summary>
        public const string CreatorRole = "Creator";

        /// <summary>
        /// Users with the Editor Role are able to edit existing Facilities and
        /// delete Files.
        /// </summary>
        public const string EditorRole = "Editor";

        public static readonly List<string> AllRoles = new List<string> {AdminRole, CreatorRole, EditorRole};
    }
}