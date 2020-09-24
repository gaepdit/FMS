using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<File> GetFiles()
        {
            return new List<File>
            {
                new File
                {
                    Id = new Guid("790B04E8-F5F5-412E-95E2-B785E630A2A7"),
                    FileLabel = "248-0001",  //henry
                },
                new File
                {
                    Id = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),
                    FileLabel = "243-0001",   //cherokee
                },
                new File
                {
                    Id = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"),
                    FileLabel = "180-0001",     //dawson
                },
                new File
                {
                    Id = new Guid("b0e978c5-e2e5-43df-a3a2-c64e34273673"),
                    FileLabel = "180-0002",     //dawson
                },
                new File
                {
                    Id = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb68"),
                    FileLabel = "180-0003",
                },
                new File
                {
                    Id = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb69"),
                    FileLabel = "099-0002",
                    Active = false,
                },
                new File
                {
                    Id = new Guid("015A39B3-A522-4C13-9479-B17626247313"),
                    FileLabel = "170-0001",     //dade
                },
                new File
                {
                    Id = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53C"),
                    FileLabel = "164-0001",     //telfair
                },
                new File
                {
                    Id = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53D"),
                    Active = false,
                    FileLabel = "099-0001",     //telfair
                },
            };
        }
    }
}
