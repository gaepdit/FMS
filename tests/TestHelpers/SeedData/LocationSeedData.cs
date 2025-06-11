using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Location> GetLocations()
        {
            return new List<Location>()
            {
                new()
                {
                    Id = new Guid("533EBBD5-4144-41D5-9338-C3C5317C3E5E"),
                    Active = true,
                    FacilityId = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    Class = "I",
                },
                new()
                {
                    Id = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    Class = "II",
                },
                new()
                {
                    Id = new Guid("41CD50F4-353D-450A-AB4E-8BE3EAF27CA8"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    Class = "III",
                },
                new()
                {
                    Id = new Guid("77D2B98D-0C55-46EA-9583-B62BD934762C"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    Class = "IV",
                },
                new()
                {
                    Id = new Guid("A99FF86A-2F97-48DB-830B-9C92F88DE681"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    Class = "V",
                },
                new()
                {
                    Id = new Guid("98B3C9ED-F747-48AE-8B92-497A35927E90"),
                    Active = true,
                    FacilityId = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    Class = "ER",
                }
            };
        }
    }
}
