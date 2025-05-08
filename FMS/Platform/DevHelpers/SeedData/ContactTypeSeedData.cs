using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<ContactType> GetContactTypes()
        {
            return new List<ContactType>()
            {
                new()
                {
                    Id = new Guid("58FD9186-CD30-4CB9-AE0F-347B224F9217"),
                    Active = true,
                    Name = "Primary Contact",
                },
                new()
                {
                    Id = new Guid("A5498528-773C-42ED-BDB5-D0F5F67CDE34"),
                    Active = true,
                    Name = "Owner",
                },
                new()
                {
                    Id = new Guid("7805FD4F-081C-48CD-91A1-06E9A28FE7CB"),
                    Active = true,
                    Name = "Office Manager",
                },
                new()
                {
                    Id = new Guid("25AFB8D8-1718-4FC7-AE01-EDE08AB1785E"),
                    Active = true,
                    Name = "Supervisor",
                },
                new()
                {
                    Id = new Guid("CD420436-06EB-4E33-8B1A-B6D8EE482390"),
                    Active = true,
                    Name = "Owner's Representative",
                },
                new()
                {
                    Id = new Guid("56F74F8A-2F6D-4445-A0E0-529B1C885C2B"),
                    Active = true,
                    Name = "Attorney",
                }
            };
        }
    }
}