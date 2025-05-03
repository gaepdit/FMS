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
                    Id = new Guid(""),
                    Active = true,
                    Name = "Primary Contact",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Owner",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Office Manager",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Supervisor",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Owner's Representative",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Attorney",
                }
            };
        }
    }
}