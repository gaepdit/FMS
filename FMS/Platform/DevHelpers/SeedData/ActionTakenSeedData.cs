using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<ActionTaken> GetActionTakens()
        {
            return new List<ActionTaken>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                }
            };
        }
    }
}
 