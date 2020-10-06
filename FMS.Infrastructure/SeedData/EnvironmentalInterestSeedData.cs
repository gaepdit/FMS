using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<EnvironmentalInterest> GetEnvironmentalInterests()
        {
            return new List<EnvironmentalInterest>
            {
                new EnvironmentalInterest
                {
                    Id = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"),
                    Active = true,
                    Name = "RCRA",
                },
                new EnvironmentalInterest
                {
                    Id = new Guid("258117AE-4E08-428D-BA0F-F5DEBF9834E2"),
                    Active = true,
                    Name = "HSRA",
                },
                new EnvironmentalInterest
                {
                    Id = new Guid("C68D44B3-7283-40B1-8105-0B999CED87C5"),
                    Active = true,
                    Name = "BROWN",
                },
                new EnvironmentalInterest
                {
                    Id = new Guid("118B0A46-31D9-428F-85F9-8F5D7B45CE0D"),
                    Active = true,
                    Name = "CERCLA",
                },
                new EnvironmentalInterest
                {
                    Id = new Guid("462E0AD7-EBF6-40DD-93E9-3DB4C12B7034"),
                    Active = true,
                    Name = "DOD",
                }
            };
        }
    }
}
