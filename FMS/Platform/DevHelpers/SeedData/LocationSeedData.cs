using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Location> GetLocations()
        {
            return new List<Location>()
            {
                new()
                {
                    Id = new Guid("533EBBD5-4144-41D5-9338-C3C5317C3E5E"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "I",
                },
                new()
                {
                    Id = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "II",
                },
                new()
                {
                    Id = new Guid("41CD50F4-353D-450A-AB4E-8BE3EAF27CA8"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "III",
                },
                new()
                {
                    Id = new Guid("77D2B98D-0C55-46EA-9583-B62BD934762C"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "IV",
                },
                new()
                {
                    Id = new Guid("A99FF86A-2F97-48DB-830B-9C92F88DE681"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "V",
                },
                new()
                {
                    Id = new Guid("98B3C9ED-F747-48AE-8B92-497A35927E90"),
                    Active = true,
                    FacilityId = new Guid(""),
                    Class = "ER",
                }
            };
        }
    }
}
