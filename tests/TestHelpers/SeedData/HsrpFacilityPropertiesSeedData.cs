using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<HsrpFacilityProperties> GetHsrpFacilityProperties()
        {
            return new List<HsrpFacilityProperties>()
            {
                new()
                {
                    Id = new Guid("C0EF4907-3C2D-45D1-9316-86F3BFA0091B"),
                    Active = true,
                    FacilityId = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = null
                },
                new()
                {
                    Id = new Guid("FA4B1A1B-B8B1-44A5-9897-D9C95761F948"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = new(2023, 3, 12)
                },
                new()
                {
                    Id = new Guid("5D9F854B-0026-4D67-A05D-75CB1135C9B9"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    DateListed = new(2016, 1, 13),
                    AdditionalOrgUnit = "AnotherOrg Unit",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    VRPDate = new(2018, 12, 13),
                    BrownfieldDate = new(2019, 2, 13),
                    DateDeListed = null
                },
                new()
                {
                    Id = new Guid("E6F234FF-4C66-4A1E-B99A-039F538951AA"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    DateListed = new(2018, 2, 13),
                    AdditionalOrgUnit = "",
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = null
                }
            };
        }
    }
}
