using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static Cabinet[] GetCabinets()
        {
            return new List<Cabinet>
            {
                new Cabinet
                {
                    Id = new Guid("06C4AE30-4FFB-4C6B-9E5E-577D088554A4"),
                    Name = "C001",
                },
                new Cabinet
                {
                    Id = new Guid("0C46CCE9-77DB-4882-B4F5-9CDBE6522D01"),
                    Name = "C002",
                },
                new Cabinet
                {
                    Id = new Guid("1548EABF-B62E-4938-8EC6-075F3F385BCF"),
                    Name = "C003",
                },
                new Cabinet
                {
                    Id = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"),
                    Active = false,
                    Name = "C004",
                },
                new Cabinet
                {
                    Id = new Guid("20FDB0C9-F173-414F-96DA-9CA63EB4065F"),
                    Name = "C005",
                },
                new Cabinet
                {
                    Id = new Guid("C8124DAF-D088-4B67-B846-AF5622FA4D9E"),
                    Name = "C006",
                }
            }.ToArray();
        }
    }
}
