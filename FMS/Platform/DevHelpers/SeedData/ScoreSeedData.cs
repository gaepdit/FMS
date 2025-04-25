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
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    FacilityId = new Guid(""),
                    ScoredDate = new(2018, 2, 13),
                    ScoredById = new Guid(""),
                    Comments = "",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid(""),
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
