using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace TestHelpers.SimpleRepository
{
    public static class SimpleRepositoryData
    {
        public static readonly List<File> Files = new List<File>
        {
            new File {Id = Guid.NewGuid(), FileLabel = "099-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "111-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0003"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0002", Active = false}
        };

        public static readonly List<Facility> Facilities = new List<Facility>
        {
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "ABC",
                FileId = Files[0].Id,
                CountyId = 131,
                Location = "somewhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "ABC123",
                FileId = Files[0].Id,
                CountyId = 131,
                Location = "elsewhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "DEF",
                FileId = Files[1].Id,
                CountyId = 131,
                Location = "",
                Active = false,
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "GHI",
                FileId = Files[0].Id,
                CountyId = 099,
                Location = "nowhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "JKL",
                FileId = Files[3].Id,
                CountyId = 102,
                Location = "here",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "MNO",
                FileId = Files[4].Id,
                CountyId = 103,
                Location = "",
            },
        };

        public static readonly List<BudgetCode> BudgetCodes = new List<BudgetCode>
        {
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC001"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC002"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC003", Active = false},
        };
    }
}