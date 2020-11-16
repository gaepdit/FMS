using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace TestHelpers.SimpleRepository
{
    public static partial class SimpleRepositoryData
    {
        public static readonly List<FacilityType> FacilityTypes = new List<FacilityType>
        {
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "GEN", Description = "GEN1"},
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "NPL", Description = "NPL1"}
        };

        public static readonly List<BudgetCode> BudgetCodes = new List<BudgetCode>
        {
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC001"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC002"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC003", Active = false},
        };

        public static readonly List<OrganizationalUnit> OrganizationalUnits = new List<OrganizationalUnit>
        {
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org One"},
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org Two"}
        };

        public static readonly List<FacilityStatus> FacilityStatuses = new List<FacilityStatus>
        {
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Active"},
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Inactive"}
        };

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
                FacilityTypeId = FacilityTypes[0].Id,
                CountyId = 131,
                Location = "somewhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "ABC123",
                FileId = Files[0].Id,
                FacilityTypeId = FacilityTypes[0].Id,
                CountyId = 131,
                Location = "elsewhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "DEF",
                FileId = Files[1].Id,
                FacilityTypeId = FacilityTypes[0].Id,
                CountyId = 131,
                Location = "",
                Active = false,
                IsRetained = false,
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "GHI",
                FileId = Files[0].Id,
                FacilityTypeId = FacilityTypes[1].Id,
                CountyId = 099,
                Location = "nowhere",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "JKL",
                FileId = Files[3].Id,
                FacilityTypeId = FacilityTypes[1].Id,
                CountyId = 102,
                Location = "here",
            },
            new Facility
            {
                Id = Guid.NewGuid(),
                FacilityNumber = "MNO",
                FileId = Files[4].Id,
                FacilityTypeId = FacilityTypes[1].Id,
                CountyId = 103,
                Location = "",
            },
        };

        public static readonly List<RetentionRecord> RetentionRecords = new List<RetentionRecord>
        {
            new RetentionRecord
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities[0].Id,
                StartYear = 2003, EndYear = 2009,
                BoxNumber = "BOX1"
            },
            new RetentionRecord
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities[0].Id,
                StartYear = 2010, EndYear = 2016,
                BoxNumber = "BOX2"
            },
            new RetentionRecord
            {
                Id = Guid.NewGuid(),
                FacilityId = Facilities[1].Id,
                StartYear = 2001, EndYear = 2002,
                BoxNumber = "BOX3"
            }
        };

        public static readonly List<Cabinet> Cabinets = new List<Cabinet>
        {
            new Cabinet {Id = Guid.NewGuid(), Name = "C001", FirstFileLabel = "000-0000"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C002", FirstFileLabel = "103-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C003", FirstFileLabel = "110-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C004", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C005", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C006", FirstFileLabel = "150-0001", Active = false},
            new Cabinet {Id = Guid.NewGuid(), Name = "C007", FirstFileLabel = "150-0001"},
        };
    }
}