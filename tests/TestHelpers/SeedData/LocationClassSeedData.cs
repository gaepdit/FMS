using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<LocationClass> GetLocationClasses()
        {
            return new List<LocationClass>
            {
                new()
                {
                    Id = new Guid("c761bc46-6745-4ffc-83eb-fe1074a9c6ad"),
                    Active = true,
                    Name = "I",
                    Description = "The Director has determined that this site requires corrective action.",
                },
                new()
                {
                    Id = new Guid("ec4ab203-8c3e-4ffb-a849-94365b87c2ab"),
                    Active = true,
                    Name = "II",
                    Description = "Pending",
                },
                new()
                {
                    Id = new Guid("1b33027b-261f-4639-bd10-c3e9b29b5d90"),
                    Active = true,
                    Name = "III",
                    Description = "The Director has determined that this site requires corrective action.",
                },
                new()
                {
                    Id = new Guid("cc3a1dfd-3e42-4fb6-8461-d186ca6f2b28"),
                    Active = true,
                    Name = "IV",
                    Description = "The Director has determined that this site requires corrective action.",
                },
                new()
                {
                    Id = new Guid("8cbc6825-2d27-43d9-91c0-d1633ee2e113"),
                    Active = true,
                    Name = "V",
                    Description = "The Director has determined that this site requires corrective action.",
                },
                new()
                {
                    Id = new Guid("efd9c844-7bf0-4309-9fa1-3c8cece0f5fd"),
                    Active = true,
                    Name = "ER",
                    Description = "Error",
                },
            };
        }
    }
}
