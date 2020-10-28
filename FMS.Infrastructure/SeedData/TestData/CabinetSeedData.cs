using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace FMS.Infrastructure.SeedData
{
    public static partial class TestData
    {
        public static List<Cabinet> GetCabinets()
        {
            return new List<Cabinet>
            {
                new Cabinet
                {
                    Id = new Guid("06C4AE30-4FFB-4C6B-9E5E-577D088554A4"),
                    CabinetNumber = 1,
                    FirstFileLabel = "000-0000",
                },
                new Cabinet
                {
                    Id = new Guid("0C46CCE9-77DB-4882-B4F5-9CDBE6522D01"),
                    CabinetNumber = 2,
                    FirstFileLabel = "100-0000",
                },
                new Cabinet
                {
                    Id = new Guid("1548EABF-B62E-4938-8EC6-075F3F385BCF"),
                    CabinetNumber = 3,
                    FirstFileLabel = "164-0001",
                },
                new Cabinet
                {
                    Id = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"),
                    Active = false,
                    CabinetNumber = 4,
                    FirstFileLabel = "170-0000",
                },
                new Cabinet
                {
                    Id = new Guid("20FDB0C9-F173-414F-96DA-9CA63EB4065F"),
                    CabinetNumber = 5,
                    FirstFileLabel = "180-0000",
                },
                new Cabinet
                {
                    Id = new Guid("C8124DAF-D088-4B67-B846-AF5622FA4D9E"),
                    CabinetNumber = 6,
                    FirstFileLabel = "200-0000",
                }
            };
        }
    }
}