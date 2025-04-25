using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Phone> GetPhones()
        {
            return new List<Phone>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ContactId = new Guid(""),
                    CountryCode = 1,
                    AreaCode = 234,
                    Prefix = 567,
                    Number = 8901,
                    Type = ""
                }
            };
        }
    }
}

