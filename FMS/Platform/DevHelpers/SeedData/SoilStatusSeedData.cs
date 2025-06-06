using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<SoilStatus> GetSoilStatuses()
        {
            return new List<SoilStatus>
            {
                new() 
                {
                    Id = new Guid("013E4ABB-F6A5-4B95-BA5A-438BEDD0BECA"),
                    Active = true,
                    Name = "Soil Status 1",
                    Description = "Initial soil status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("C0AB6B37-F700-4DAE-8801-D9DC8E03453E"),
                    Active = true,
                    Name = "Soil Status 2",
                    Description = "Secondary soil status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("A1B2C3D4-E5F6-7890-ABCD-EF1234567890"),
                    Active = true,
                    Name = "Soil Status 3",
                    Description = "Tertiary soil status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    Active = true,
                    Name = "Soil Status 4",
                    Description = "Quaternary soil status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("FEDCBA98-7654-3210-FEDC-BA9876543210"),
                    Active = true,
                    Name = "Soil Status 5",
                    Description = "Quinary soil status for testing purposes."
                }
            };
        }
    }
}
