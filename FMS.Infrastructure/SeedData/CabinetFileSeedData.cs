using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<CabinetFile> GetCabinetFiles()
        {
            return new List<CabinetFile>
            {
                new CabinetFile
                {
                    CabinetId = new Guid("06C4AE30-4FFB-4C6B-9E5E-577D088554A4"), // C001
                    FileId = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53C") // 164-0001
                },
                new CabinetFile
                {
                    CabinetId = new Guid("06C4AE30-4FFB-4C6B-9E5E-577D088554A4"), // C001
                    FileId = new Guid("015A39B3-A522-4C13-9479-B17626247313"), // 170-0001
                },
                new CabinetFile
                {
                    CabinetId = new Guid("1548EABF-B62E-4938-8EC6-075F3F385BCF"), // C003
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                },
                new CabinetFile
                {
                    CabinetId = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"), // C004
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                },
                new CabinetFile
                {
                    CabinetId = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"), // C004
                    FileId = new Guid("b0e978c5-e2e5-43df-a3a2-c64e34273673"), // 180-0002
                },
                new CabinetFile
                {
                    CabinetId = new Guid("6FCDA843-53DC-4846-8AC4-A55AD6D88B11"), // C004
                    FileId = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb68"), // 180-0003
                },
                new CabinetFile
                {
                    CabinetId = new Guid("20FDB0C9-F173-414F-96DA-9CA63EB4065F"), // C005
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),
                }
            };
        }
    }
}
