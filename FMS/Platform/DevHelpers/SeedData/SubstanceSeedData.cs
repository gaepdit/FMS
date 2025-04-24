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
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("81900062-3B08-455E-AA87-1485E3D207AA"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("BAEAEFD6-F758-4BBE-971E-ACC44D038A39"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("33B4AC94-CCCE-4ECA-9428-CA3448CC569B"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("7CFCB7BD-C22D-44F6-8723-4C036F98308C"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("CA004614-90A9-4112-A96C-AE7790549A8C"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("CE9C2633-A7CD-4DE6-B1CE-30DC6369557C"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("F93B6825-D1EB-469A-A716-7C9B3E2A423E"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid("71DD8B22-6596-44F5-9BB9-B108A46469F4"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
{
                    Id = new Guid("FFEDC0D1-085C-4422-BFCF-83E8FA4A20E4"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
    {
                    Id = new Guid("A45057D5-DC61-4CC9-9E3C-C6F4505D9EAB"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                }
            };
        }
    }
}
