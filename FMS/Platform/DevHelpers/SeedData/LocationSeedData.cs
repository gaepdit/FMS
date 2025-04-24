using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Location> GetLocations()
        {
            return new List<Location>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Score = "",
                }
            };
        }
    }
}
