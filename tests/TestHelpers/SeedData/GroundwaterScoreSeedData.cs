using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<GroundwaterScore> GetGroundwaterScores()
        {
            return new List<GroundwaterScore>()
            {
                new()
                {
                    Id = new Guid("AA430C6B-B0C1-4283-A7B1-9C1DD2860A63"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    GWScore = 32.65m,
                    A = 3,
                    B1 = 5,
                    B2 = 4,
                    C = 6,
                    Description = "contamination in ditch behind facility",
                    ChemName = "",
                    Other = "Other stuff",
                    D2 = 2,
                    D3 = 1,
                    SubstanceId = new Guid("8D86BDC2-6E46-4722-8B14-E2EF671BBB36"),
                    CASNO = "",
                    E1 = 8,
                    E2 = 18
                },
                new()
                {
                    Id = new Guid("5E02A46B-16BB-4F58-B747-836CA2FF298A"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    GWScore = 31.4m,
                    A = 3,
                    B1 = 7,
                    B2 = 1,
                    C = 4,
                    Description = "In Stream behind Store",
                    ChemName = "",
                    Other = "Other stuff again",
                    D2 = 8,
                    D3 = 2,
                    SubstanceId = new Guid("7CFCB7BD-C22D-44F6-8723-4C036F98308C"),
                    CASNO = "",
                    E1 = 3,
                    E2 = 9
                }
            };
        }
    }
}
