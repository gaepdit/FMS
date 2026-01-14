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
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    LocationClassId = new Guid("1b33027b-261f-4639-bd10-c3e9b29b5d90"),
                },
                new()
                {
                    Id = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    LocationClassId = new Guid("ec4ab203-8c3e-4ffb-a849-94365b87c2ab"),
                }
            };
        }
    }
}
