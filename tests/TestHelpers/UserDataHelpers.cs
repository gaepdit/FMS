using FMS.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        public static List<ApplicationUser> GetApplicationUsers() =>
            new List<ApplicationUser> {
                new ApplicationUser
                {
                    Id = new Guid("06bca04c-19bb-4c41-b554-e57a56a2c6b7"),
                    Email = "example.one@example.com",
                    GivenName = "Sample",
                    FamilyName = "User"
                },
                new ApplicationUser
                {
                    Id = new Guid("43a21a8a-1fc6-4348-9004-e1aec42392b7"),
                    Email = "example.two@example.com",
                    GivenName = "Another",
                    FamilyName = "Sample"
                }
            };
    }
}
