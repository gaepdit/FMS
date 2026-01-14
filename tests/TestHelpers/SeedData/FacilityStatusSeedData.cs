using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<FacilityStatus> GetFacilityStatuses()
        {
            return new List<FacilityStatus>
            {
                new()
                {
                    Id = new Guid("A3B22F16-B136-4DD6-870E-829413843D62"),
                    Active = true,
                    Status = "Active",
                },
                new()
                {
                    Id = new Guid("095142F7-E2B0-4E63-973E-A5FAABAC4DED"),
                    Active = true,
                    Status = "Inactive",
                },
                new()
                {
                    Id = new Guid("455F32E5-8FFF-49F0-9E82-0041332FC284"),
                    Active = true,
                    Status = "unknown",
                },
                new()
                {
                    Id = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Active = true,
                    Status = "NON-RCRA",
                },
                new()
                {
                    Id = new Guid("68C6FBEC-CB38-41CF-ADD9-9355BD2FB214"),
                    Active = true,
                    Status = "COMPLAINT",
                },
                new()
                {
                    Id = new Guid("8866d267-0883-462e-b04d-4d1894fa1a06"),
                    Active = true,
                    Status = "Event Tracking On",
                },
                new()
                {
                    Id = new Guid("899849DD-ECF8-4A67-982F-6EAD601DB291"),
                    Active = true,
                    Status = "DELISTED",
                }
            };
        }
    }
}