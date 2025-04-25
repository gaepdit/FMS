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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
                    Id = new Guid(""),
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
