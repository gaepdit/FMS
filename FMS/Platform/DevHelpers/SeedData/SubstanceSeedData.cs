using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Substance> GetSubstances()
        {
            return new List<Substance>()
            {
                new()
                {
                    Id = new Guid("8D86BDC2-6E46-4722-8B14-E2EF671BBB36"),
                    Active = true,
                    FacilityId = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    ChemicalId = new Guid("2365ba02-13a9-406c-9e39-f600f37d64e5"),
                    Groundwater = true,
                    Soil = false,
                    UseForScoring = true
                },
                new()
                {
                    Id = new Guid("81900062-3B08-455E-AA87-1485E3D207AA"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    ChemicalId = new Guid("a39e6b98-0c37-432c-9a35-fccb9151703f"),
                    Groundwater = false,
                    Soil = true,
                    UseForScoring = true
                },
                new()
                {
                    Id = new Guid("BAEAEFD6-F758-4BBE-971E-ACC44D038A39"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ChemicalId = new Guid("e197d2d5-193b-409f-88bb-f0191f3f508d"),
                    Groundwater = true,
                    Soil = true,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("33B4AC94-CCCE-4ECA-9428-CA3448CC569B"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    ChemicalId = new Guid("a6c4e176-f45d-47e9-a61b-4fe5f3341dec"),
                    Groundwater = true,
                    Soil = true,
                    UseForScoring = true
                },
                new()
                {
                    Id = new Guid("7CFCB7BD-C22D-44F6-8723-4C036F98308C"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ChemicalId = new Guid("c87369b4-bbc2-41fd-b24f-48f0199126fb"),
                    Groundwater = true,
                    Soil = false,
                    UseForScoring = true
                },
                new()
                {
                    Id = new Guid("CA004614-90A9-4112-A96C-AE7790549A8C"),
                    Active = true,
                    FacilityId = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    ChemicalId = new Guid("DC132688-7B95-4328-8655-057C95EB59D1"),
                    Groundwater = false,
                    Soil = true,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("CE9C2633-A7CD-4DE6-B1CE-30DC6369557C"),
                    Active = false,
                    FacilityId = new Guid("D6C596EA-0530-460F-A105-2FB772F8F0B2"),
                    ChemicalId = new Guid("5ECEF580-F3CB-4C60-8D3E-AA19AE49A03F"),
                    Groundwater = true,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("F93B6825-D1EB-469A-A716-7C9B3E2A423E"),
                    Active = true,
                    FacilityId = new Guid("309436BC-F7E7-4BFD-8455-E868129D6F45"),
                    ChemicalId = new Guid("1BD5A2B6-5D1A-4477-B621-E7DC4E6CB504"),
                    Groundwater = true,
                    Soil = true,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("71DD8B22-6596-44F5-9BB9-B108A46469F4"),
                    Active = true,
                    FacilityId = new Guid("109436BC-F7E7-4BFD-8455-E868129D6F45"),
                    ChemicalId = new Guid("816D044A-8B2C-4C71-AB75-C1137971E6D6"),
                    Groundwater = true,
                    Soil = false,
                    UseForScoring = true
                },
                new()
{
                    Id = new Guid("FFEDC0D1-085C-4422-BFCF-83E8FA4A20E4"),
                    Active = true,
                    FacilityId = new Guid("810DDE72-5459-4ECC-81D8-A51554C9FF3F"),
                    ChemicalId = new Guid("31A34C9F-5600-4D18-85E5-5242B4D8BB26"),
                    Groundwater = false,
                    Soil = true,
                    UseForScoring = false
                },
                new()
    {
                    Id = new Guid("A45057D5-DC61-4CC9-9E3C-C6F4505D9EAB"),
                    Active = true,
                    FacilityId = new Guid("BF25C413-0EE1-4280-84BD-0B2631F4EEC7"),
                    ChemicalId = new Guid("AF3F6523-91C9-41E2-B08A-6BEAD2599FAA"),
                    Groundwater = false,
                    Soil = true,
                    UseForScoring = false
                }
            };
        }
    }
}
