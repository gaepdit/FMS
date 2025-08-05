using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<AbandonSites> GetAbandonSites()
        {
            return new List<AbandonSites>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "A",
                    Description = "Description A",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "I",
                    Description = "Description I",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "D",
                    Description = "Description D",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = false,
                    Name = "X",
                    Description = "Description X",
                }
            };
        }
    }
}
