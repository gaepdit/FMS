using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static File[] GetFiles()
        {
            return new List<File>
            {
                new File
                {
                     Id = new Guid("790B04E8-F5F5-412E-95E2-B785E630A2A7"),
                    Active = true,
                    FileLabel = "248-0001",  //henry
                    FileCabinets = { },   //C054
                    Facilities = { }
                },
                new File
                {
                     Id = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),
                    Active = true,
                    FileLabel = "243-0001",   //cherokee
                    FileCabinets = { },    //C046,C047
                    Facilities = { }
                },
                new File
                {
                     Id = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"),
                    Active = true,
                    FileLabel = "180-0001",     //dawson
                    FileCabinets = { },    //C012
                    Facilities = { }
                },
                new File
                {
                     Id = new Guid("015A39B3-A522-4C13-9479-B17626247313"),
                    Active = true,
                    FileLabel = "170-0001",     //dade
                    FileCabinets = { },    //C010
                    Facilities = { }
                },
                new File
                {
                     Id = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53C"),
                    Active = true,
                    FileLabel = "164-0001",     //telfair
                    FileCabinets = { },    //C008
                    Facilities = { }
                }
            }.ToArray();
        }
    }
}
