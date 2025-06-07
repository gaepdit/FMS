using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<GroundwaterStatus> GetGroundwaterStatuses()
        {
            return new List<GroundwaterStatus>
            {
                new() 
                {
                    Id = new Guid("39A33A21-400C-47EB-9114-E5C1F7BDA5EA"),
                    Active = true,
                    Name = "Groundwater Status 1",
                    Description = "Initial groundwater status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("1186BFF4-4DCA-4F8F-A0D6-CDB0157FD7A1"),
                    Active = true,
                    Name = "Groundwater Status 2",
                    Description = "Secondary groundwater status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("EE88D813-2199-4E6F-81EF-3F3837258B2E"),
                    Active = true,
                    Name = "Groundwater Status 3",
                    Description = "Tertiary groundwater status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("E4644119-683C-4793-9462-0B25C18797B5"),
                    Active = true,
                    Name = "Groundwater Status 4",
                    Description = "Quaternary groundwater status for testing purposes."
                },
                new() 
                {
                    Id = new Guid("E708CC7C-9362-4932-A6C0-3E101E74B5D9"),
                    Active = true,
                    Name = "Groundwater Status 5",
                    Description = "Quinary groundwater status for testing purposes."
                }
            };
        }
    }
}
