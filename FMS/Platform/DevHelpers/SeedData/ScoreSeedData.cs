using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Score> GetScores()
        {
            return new List<Score>()
            {
                new()
                {
                    Id = new Guid("C0FCD976-5361-4253-B140-CA39BCEC35E7"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("2268E4C6-CFDC-4786-B383-CB65829DE520"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("CE64BD64-4110-44CA-A2B0-A1AE7A389B61"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("8F63491E-A3A6-4ABE-BF89-479CB2915059"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("F8F07B9C-DAC4-4D54-B97F-BA1CE5133D54"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("5119BF82-D9C6-4014-B951-2F1B850BAA99"),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                }
            };
        }
    }
}
