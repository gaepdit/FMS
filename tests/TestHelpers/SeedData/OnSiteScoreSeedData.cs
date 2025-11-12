using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<OnsiteScore> GetOnSiteScores()
        {
            return new List<OnsiteScore>()
            {
                new()
                {
                    Id = new Guid("013E4ABB-F6A5-4B95-BA5A-438BEDD0BECA"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    OnsiteScoreValue = 35.3m,
                    A = 6,
                    B = 5,
                    C = 8,
                    Description = "soil surrounding Tanks",
                    ChemName1D = "",
                    Other1D = "",
                    D2 = 2,
                    D3 = 1,
                    SubstanceId = new Guid("DC132688-7B95-4328-8655-057C95EB59D1"),
                    CASNO = "",
                    E1 = 9,
                    E2 = 0
                },
                new()
                {
                    Id = new Guid("C0AB6B37-F700-4DAE-8801-D9DC8E03453E"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    OnsiteScoreValue = 3.8m,
                    A = 3,
                    B = 3,
                    C = 2,
                    Description = "soil at rear fill area",
                    ChemName1D = "",
                    Other1D = "",
                    D2 = 6,
                    D3 = 8,
                    SubstanceId = new Guid("5ECEF580-F3CB-4C60-8D3E-AA19AE49A03F"),
                    CASNO = "",
                    E1 = 0,
                    E2 = 2
                }
            };
        }
    }
}
