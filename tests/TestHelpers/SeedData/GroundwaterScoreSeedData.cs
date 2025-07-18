using System;
using System.Collections.Generic;
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
                    GWScore = "",
                    A = 0,
                    B1 = 0,
                    B2 = 0,
                    C = 0,
                    Description = "",
                    ChemName = "",
                    Other = "",
                    D2 = 0,
                    D3 = 0,
                    ChemicalId = new Guid("2365ba02-13a9-406c-9e39-f600f37d64e5"),
                    CASNO = "",
                    E1 = 0,
                    E2 = 0
                },
                new()
                {
                    Id = new Guid("5E02A46B-16BB-4F58-B747-836CA2FF298A"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    GWScore = "",
                    A = 0,
                    B1 = 0,
                    B2 = 0,
                    C = 0,
                    Description = "",
                    ChemName = "",
                    Other = "",
                    D2 = 0,
                    D3 = 0,
                    ChemicalId = new Guid("a39e6b98-0c37-432c-9a35-fccb9151703f"),
                    CASNO = "",
                    E1 = 0,
                    E2 = 0
                }
            };
        }
    }
}
