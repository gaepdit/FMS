using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<HsrpFacilityProperties> GetHsrpFacilityProperties()
        {
            return new List<HsrpFacilityProperties>()
            {
                new()
                {
                    Id = new Guid("C0EF4907-3C2D-45D1-9316-86F3BFA0091B"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                },
                new()
                {
                    Id = new Guid("FA4B1A1B-B8B1-44A5-9897-D9C95761F948"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                },
                new()
                {
                    Id = new Guid("5D9F854B-0026-4D67-A05D-75CB1135C9B9"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13)
                },
                new()
                {
                    Id = new Guid("E6F234FF-4C66-4A1E-B99A-039F538951AA"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                },
                new()
                {
                    Id = new Guid("ABFC4C4E-E2E0-42A1-A2F8-EB94C0083C50"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                },
                new()
                {
                    Id = new Guid("2C84E5F9-62ED-4A1F-94C3-219C3AC5A6F6"),
                    Active = true,
                    FacilityId = new Guid(""),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    Geologist = "",
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                }
            };
        }
    }
}
