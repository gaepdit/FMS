using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static FacilityType[] GetFacilityTypes()
        {
            return new List<FacilityType>
            {
                new FacilityType
                {
                    Id = new Guid("6F19934A-6AF2-438B-8858-03FA6AC4E78A"),
                    Active = true,
                    Code = 1,
                    Name = "FUDS"
                },
                new FacilityType
                {
                    Id = new Guid("883B4581-2C73-429D-B198-82E352FD72F0"),
                    Active = true,
                    Code = 2,
                    Name = "NPL"
                },
                new FacilityType
                {
                    Id = new Guid("1E51E549-9F79-42CE-8AA1-099A76E41BFC"),
                    Active = true,
                    Code = 3,
                    Name = "HSRA"
                },
                new FacilityType
                {
                    Id = new Guid("F96E2355-12EE-4027-961C-25D87489A60A"),
                    Active = true,
                    Code = 4,
                    Name = "VRP"
                },
                new FacilityType
                {
                    Id = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),
                    Active = true,
                    Code = 5,
                    Name = "GEN"
                },
                new FacilityType
                {
                    Id = new Guid("4C30CECF-B53E-4D09-B919-A9E07E4E9782"),
                    Active = true,
                    Code = 6,
                    Name = "BROWN"
                },
                new FacilityType
                {
                    Id = new Guid("83E3005C-BD7C-4E52-918E-E1166F9483CC"),
                    Active = true,
                    Code = 7,
                    Name = "NONGEN"
                }
            }.ToArray();
        }
    }
}
