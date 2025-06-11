using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<ParcelType> GetParcelTypes()
        {
            return new List<ParcelType>()
            {
                new()
                {
                    Id = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Active = true,
                    Name = "List",
                },
                new()
                {
                    Id = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Active = true,
                    Name = "SubList",
                },
                new()
                {
                    Id = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Active = true,
                    Name = "Impacted",
                },
                new()
                {
                    Id = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Active = true,
                    Name = "VRP Only",
                }
            };
        }
    }
}
        