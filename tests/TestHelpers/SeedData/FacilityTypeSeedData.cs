using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<FacilityType> GetFacilityTypes()
        {
            return new List<FacilityType>
            {
                new()
                {
                    Id = new Guid("6F19934A-6AF2-438B-8858-03FA6AC4E78A"),
                    Active = true,
                    Name = "GEN",
                    Description = "RCRA generator",
                },
                new()
                {
                    Id = new Guid("1E51E549-9F79-42CE-8AA1-099A76E41BFC"),
                    Active = true,
                    Name = "NPL",
                    Description = "NPL",
                },
                new()
                {
                    Id = new Guid("F96E2355-12EE-4027-961C-25D87489A60A"),
                    Active = true,
                    Name = "DOD",
                    Description = "DOD RCRA non-generator",
                },
                new()
                {
                    Id = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),
                    Active = true,
                    Name = "VRP",
                    Description = "VRP",
                },
                new()
                {
                    Id = new Guid("83E3005C-BD7C-4E52-918E-E1166F9483CC"),
                    Active = true,
                    Name = "HSI",
                    Description = "HSI",
                },
                new()
                {
                    Id = new Guid("3FE54579-1762-4A77-9AD7-9D185E000A79"),
                    Active = true,
                    Name = "TSDCA",
                    Description = "TSD/CA RCRA non-generator",
                },
                new()
                {
                    Id = new Guid("C8F61579-46F8-47FD-B7C4-FEB17F26384B"),
                    Active = true,
                    Name = "BROWN",
                    Description = "Brownfield",
                },
                new()
                {
                    Id = new Guid("44DD76AD-3637-47AA-B4DC-A19423B7EFBF"),
                    Active = true,
                    Name = "FUDS",
                    Description = "FUDS",
                },
                new()
                {
                    Id = new Guid("B7224976-5D67-40F8-8112-273AE3B91419"),
                    Active = true,
                    Name = "RN",
                    Description = "Release Notification",
                }
            };
        }
    }
}