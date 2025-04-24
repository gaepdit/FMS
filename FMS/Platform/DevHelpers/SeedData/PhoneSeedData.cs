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
                    Id = new Guid("DAA49782-BEB0-4FC0-A541-930ACDFAC9AA"),
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
                    Id = new Guid("81CF0EB3-99A1-4F8A-8007-9C707BD60D1A"),
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
                    Id = new Guid("F7EB523C-3958-4F67-B0A2-0D08DFC380CF"),
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
                    Id = new Guid("63B6644C-6629-4012-BB95-9C78385FF518"),
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
                    Id = new Guid("15EB55FC-9BBF-4453-A53C-612D096C0D49"),
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
                    Id = new Guid("ABB8CE7E-3AFD-412E-9A7E-6BF73B60AFD5"),
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

