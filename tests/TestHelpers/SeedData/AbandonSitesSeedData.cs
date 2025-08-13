using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<AbandonSites> GetAbandonSites()
        {
            return new List<AbandonSites>()
            {
                new()
                {
                    Id = new Guid("A907DA6D-518F-434E-B122-0B5D13C47BBA"),
                    Active = true,
                    Name = "A",
                    Description = "Description A",
                },
                new()
                {
                    Id = new Guid("E917B3C2-3F5A-480A-B26C-3B34BF7A33AF"),
                    Active = true,
                    Name = "I",
                    Description = "Description I",
                },
                new()
                {
                    Id = new Guid("B0076048-DB94-4ADE-AAE9-2FA11D5EBB40"),
                    Active = true,
                    Name = "D",
                    Description = "Description D",
                },
                new()
                {
                    Id = new Guid("C59889CE-E911-4794-A071-92C362546217"),
                    Active = false,
                    Name = "X",
                    Description = "Description X",
                }
            };
        }
    }
}
