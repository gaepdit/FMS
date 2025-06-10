using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<SourceStatus> GetSourceStatuses()
        {
            return new List<SourceStatus>
            {
                new()
                {
                    Id = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    Active = true,
                    Name = "Source Status 1",
                    Description = "Initial source status for testing purposes."
                },
                new()
                {
                    Id = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    Active = true,
                    Name = "Source Status 2",
                    Description = "Secondary source status for testing purposes."
                },
                new()
                {
                    Id = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    Active = true,
                    Name = "Source Status 3",
                    Description = "Tertiary source status for testing purposes."
                },
                new()
                {
                    Id = new Guid("D1969522-9E72-4AF0-9CFD-54F6E60D5720"),
                    Active = true,
                    Name = "Source Status 4",
                    Description = "Quaternary source status for testing purposes."
                },
                new()
                {
                    Id = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    Active = true,
                    Name = "Source Status 5",
                    Description = "Quinary source status for testing purposes."
                }
            };
        }
    }
}
