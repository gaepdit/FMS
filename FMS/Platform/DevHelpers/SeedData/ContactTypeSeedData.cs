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
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                }
            };
        }
    }
}