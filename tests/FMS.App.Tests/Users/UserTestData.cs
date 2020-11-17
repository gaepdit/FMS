using System;
using System.Collections.Generic;
using FMS.Domain.Entities.Users;

namespace FMS.App.Tests.Users
{
    public static class UserTestData
    {
        public static List<ApplicationUser> ApplicationUsers => new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = "example.one@example.com",
                GivenName = "Sample",
                FamilyName = "User"
            },
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = "example.two@example.com",
                GivenName = "Another",
                FamilyName = "Sample"
            }
        };
    }
}