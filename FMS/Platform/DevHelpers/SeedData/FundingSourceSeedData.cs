using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<FundingSource> GetFundingSources()
        {
            return new List<FundingSource>()
            {
                new()
                {
                    Id = new Guid("97E9B7AF-5A60-4426-812A-8F6B5B2CB635"),
                    Active = true,
                    Name = "A",
                },
                new()
                {
                    Id = new Guid("DEACC0E4-AB9F-4A16-8B66-FB7512731AB5"),
                    Active = true,
                    Name = "LE",
                },
                new()
                {
                    Id = new Guid("65A97123-4BCA-478C-B110-E0F149DFCCD1"),
                    Active = true,
                    Name = "LI",
                },
                new()
                {
                    Id = new Guid("5DD80C99-665C-475C-8799-203915D6E668"),
                    Active = true,
                    Name = "P",
                },
                new()
                {
                    Id = new Guid("5F857A62-65B0-475D-B31C-A1F685C5C002"),
                    Active = true,
                    Name = "SE",
                },
                new()
                {
                    Id = new Guid("CEDC5C85-E355-4458-A283-323D8F7D075A"),
                    Active = true,
                    Name = "SI",
                }
            };
        }
    }
}
  