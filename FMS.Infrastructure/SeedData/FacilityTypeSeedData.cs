using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<FacilityType> GetFacilityTypes()
        {
            return new List<FacilityType>
            {
                new FacilityType
                {
                    Id = new Guid("6F19934A-6AF2-438B-8858-03FA6AC4E78A"),
                    Active = true,
                    Code = 1,
                    Name = "GEN"
                },
                new FacilityType
                {
                    Id = new Guid("1E51E549-9F79-42CE-8AA1-099A76E41BFC"),
                    Active = true,
                    Code = 3,
                    Name = "NPL"
                },
                new FacilityType
                {
                    Id = new Guid("F96E2355-12EE-4027-961C-25D87489A60A"),
                    Active = true,
                    Code = 4,
                    Name = "DOD"
                },
                new FacilityType
                {
                    Id = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),
                    Active = true,
                    Code = 5,
                    Name = "VRP"
                },
                new FacilityType
                {
                    Id = new Guid("83E3005C-BD7C-4E52-918E-E1166F9483CC"),
                    Active = true,
                    Code = 7,
                    Name = "HSI"
                },
                new FacilityType
                {
                    Id = new Guid("3FE54579-1762-4A77-9AD7-9D185E000A79"),
                    Active = true,
                    Code = 8,
                    Name = "TSD/CA"
                },
                new FacilityType
                {
                    Id = new Guid("C8F61579-46F8-47FD-B7C4-FEB17F26384B"),
                    Active = true,
                    Code = 9,
                    Name = "BROWN"
                },
                new FacilityType
                {
                    Id = new Guid("44DD76AD-3637-47AA-B4DC-A19423B7EFBF"),
                    Active = true,
                    Code = 11,
                    Name = "FUDS"
                }
            };
        }
    }
}
