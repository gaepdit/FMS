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
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ChemicalId = new Guid(""),
                    Groundwater = false,
                    Soil = false,
                    UseForScoring = false
                },
                new()
                {
                    Id = new Guid(""),
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
