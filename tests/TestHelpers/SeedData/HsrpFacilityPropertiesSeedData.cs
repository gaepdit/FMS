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
                    OrganizationalUnitId = new Guid("C78BF1F0-C10D-4130-A68F-FC636CE60277"),
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = null,
                    VRPTerminated = false,
                    BrownfieldTerminated = false
                },
                new()
                {
                    Id = new Guid("FA4B1A1B-B8B1-44A5-9897-D9C95761F948"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    DateListed = new(2018, 2, 13),
                    OrganizationalUnitId = new Guid("513A1958-0506-415E-A551-431E318ABB34"),
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = new(2023, 3, 12),
                    VRPTerminated = true,
                    BrownfieldTerminated = true
                },
                new()
                {
                    Id = new Guid("5D9F854B-0026-4D67-A05D-75CB1135C9B9"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    DateListed = new(2016, 1, 13),
                    OrganizationalUnitId = new Guid("803C846C-E681-42BA-9A12-F6A63C7E4FD8"),
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    VRPDate = new(2018, 12, 13),
                    BrownfieldDate = new(2019, 2, 13),
                    DateDeListed = null,
                    VRPTerminated = false,
                    BrownfieldTerminated = true
                },
                new()
                {
                    Id = new Guid("E6F234FF-4C66-4A1E-B99A-039F538951AA"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    DateListed = new(2018, 2, 13),
                    OrganizationalUnitId = new Guid("513A1958-0506-415E-A551-431E318ABB34"),
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    VRPDate = new(2018, 2, 13),
                    BrownfieldDate = new(2018, 2, 13),
                    DateDeListed = null,
                    VRPTerminated = true,
                    BrownfieldTerminated = false
                }
            };
        }
    }
}
