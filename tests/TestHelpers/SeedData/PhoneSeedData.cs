using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Phone> GetPhones()
        {
            return new List<Phone>()
            {
                new()
                {
                    Id = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Active = true,
                    ContactId = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Number = "(518)234-8901",
                    PhoneType = "Office"
                },
                new()
                {
                    Id = new Guid("81CF0EB3-99A1-4F8A-8007-9C707BD60D1A"),
                    Active = true,
                    ContactId = new Guid("2BD765CD-B463-431B-8C88-3D384891E680"),
                    Number = "(678)495-8901",
                    PhoneType = "Office"
                },
                new()
                {
                    Id = new Guid("F7EB523C-3958-4F67-B0A2-0D08DFC380CF"),
                    Active = true,
                    ContactId = new Guid("9EF29782-37B0-4EB5-B9AA-BF01CB98B246"),
                    Number = "(890)234-7631",
                    PhoneType = "Office"
                },
                new()
                {
                    Id = new Guid("63B6644C-6629-4012-BB95-9C78385FF518"),
                    Active = true,
                    ContactId = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Number = "(878)645-3901",
                    PhoneType = "Cell"
                },
                new()
                {
                    Id = new Guid("15EB55FC-9BBF-4453-A53C-612D096C0D49"),
                    Active = false,
                    ContactId = new Guid("72E434BE-1F16-4AE6-9E67-9E0B4B01401B"),
                    Number = "(890)873-8401",
                    PhoneType = "Home"
                },
                new()
                {
                    Id = new Guid("ABB8CE7E-3AFD-412E-9A7E-6BF73B60AFD5"),
                    Active = true,
                    ContactId = new Guid("2BD765CD-B463-431B-8C88-3D384891E680"),
                    Number = "(865)432-1901",
                    PhoneType = "Cell"
                }
            };
        }
    }
}

