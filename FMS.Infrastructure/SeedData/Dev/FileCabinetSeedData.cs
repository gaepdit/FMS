using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static FileCabinet[] GetFileCabinets()
        {
            return new List<FileCabinet>
            {
                new FileCabinet
                {
                     Id = new Guid("06C4AE30-4FFB-4C6B-9E5E-577D088554A4"),
                    Active = true,
                    Name = "C008",
                    StartCounty = { }, // new County { Id = 160, Name = "Jeff Davis" },
                    EndCounty = { }, //  new County { Id = 167, Name = "Rabun" },
                    StartSequence = 6,
                    EndSequence = 5
                },
                new FileCabinet
                {
                     Id = new Guid("0C46CCE9-77DB-4882-B4F5-9CDBE6522D01"),
                    Active = true,
                     Name = "C010",
                    StartCounty = { }, //   new County { Id = 169, Name = "Camden" },
                    EndCounty = { }, //  new County { Id = 177, Name = "Ware" },
                    StartSequence = 19,
                    EndSequence = 3
                },
                new FileCabinet
                {
                     Id = new Guid("1548EABF-B62E-4938-8EC6-075F3F385BCF"),
                    Active = true,
                    Name = "C012",
                    StartCounty = { }, //  new County { Id = 177, Name = "Ware" },
                    EndCounty = { }, //  new County { Id = 182, Name = "Macon" },
                    StartSequence = 28,
                    EndSequence = 16
                },
                new FileCabinet
                {
                     Id = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"),
                    Active = true,
                    Name = "C046",
                    StartCounty = { }, //  new County { Id = 242, Name = "Chatham" },
                    EndCounty = { }, //  new County { Id = 243, Name = "Cherokee" },
                    StartSequence = 282,
                    EndSequence = 63
                },
                new FileCabinet
                {
                     Id = new Guid("20FDB0C9-F173-414F-96DA-9CA63EB4065F"),
                    Active = true,
                    Name = "C047",
                    StartCounty = { }, //  new County { Id = 243, Name = "Cherokee" },
                    EndCounty =  { }, // new County { Id = 245, Name = "Cobb" },
                    StartSequence = 66,
                    EndSequence = 282
                },
                new FileCabinet
                {
                     Id = new Guid("C8124DAF-D088-4B67-B846-AF5622FA4D9E"),
                    Active = true,
                     Name = "C054",
                    StartCounty = { }, //  new County { Id = 246, Name = "Floyd" },
                    EndCounty = { }, //  new County { Id = 250, Name = "Coweta" },
                    StartSequence = 72,
                    EndSequence = 5
                }
            }.ToArray();
        }
    }
}
