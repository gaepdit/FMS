using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

namespace TestHelpers
{
    public static class RepositoryData
    {
        public static readonly List<BudgetCode> BudgetCodes = new()
        {
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC001"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC002"},
            new BudgetCode {Id = Guid.NewGuid(), Name = "BC003", Active = false},
        };

        public static readonly List<FacilityStatus> FacilityStatuses = new()
        {
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Active"},
            new FacilityStatus {Id = Guid.NewGuid(), Status = "Inactive"}
        };

        public static List<FacilityType> FacilityTypes = new()
        {
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "GEN", Description = "GEN1"},
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "NPL", Description = "NPL1"},
            new FacilityType {Id = Guid.NewGuid(), Active = true, Name = "RN", Description = "RN1"}
        };

        public static readonly List<OrganizationalUnit> OrganizationalUnits = new()
        {
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org One"},
            new OrganizationalUnit {Id = Guid.NewGuid(), Name = "Org Two"}
        };

        public static readonly List<File> Files = new()
        {
            new File {Id = Guid.NewGuid(), FileLabel = "099-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "111-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "102-0003"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0001"},
            new File {Id = Guid.NewGuid(), FileLabel = "103-0002", Active = false}
        };

        public static List<Facility> Facilities()
        {
            return new()
            {
                new Facility
                {
                    Id = new Guid("4C730563-8D90-4C34-B0C9-059A3E066267"),
                    FacilityNumber = "ABC",
                    FileId = Files[0].Id,
                    CountyId = 131,
                    Location = "somewhere",
                },
                new Facility
                {
                    Id = new Guid("26DCA0FD-F586-4D0D-AF2D-865E2B7922A2"),
                    FacilityNumber = "ABC123",
                    FileId = Files[0].Id,
                    CountyId = 131,
                    Location = "elsewhere",
                },
                new Facility
                {
                    Id = new Guid("885F8638-2FF6-4E7A-ADE9-13FEAA34ABD2"),
                    FacilityNumber = "DEF",
                    FileId = Files[1].Id,
                    CountyId = 131,
                    Location = "",
                    Active = false,
                    IsRetained = false,
                },
                new Facility
                {
                    Id = new Guid("633ACE4C-39B8-48EA-A5BC-6AF5DA56244F"),
                    FacilityNumber = "GHI",
                    FileId = Files[0].Id,
                    CountyId = 99,
                    Location = "nowhere",
                },
                new Facility
                {
                    Id = new Guid("61A70D58-F3EF-4716-93EC-698A8FABAE19"),
                    FacilityNumber = "JKL",
                    FileId = Files[3].Id,
                    CountyId = 102,
                    Location = "here",
                },
                new Facility
                {
                    Id = new Guid("08E54076-392C-46B8-883E-B302E215B053"),
                    FacilityNumber = "MNO",
                    FileId = Files[4].Id,
                    CountyId = 103,
                    Location = "",
                },
            };
        }

        public static readonly List<RetentionRecord> RetentionRecords = new()
        {
            new RetentionRecord
            {
                Id = new Guid("66400D35-0B56-46C8-B5E4-A569664DD589"),
                FacilityId = Facilities()[0].Id,
                StartYear = 2003, EndYear = 2009,
                BoxNumber = "BOX1"
            },
            new RetentionRecord
            {
                Id = new Guid("0F829C6B-4800-4B08-B779-48FC85A01338"),
                FacilityId = Facilities()[0].Id,
                StartYear = 2010, EndYear = 2016,
                BoxNumber = "BOX2"
            },
            new RetentionRecord
            {
                Id = new Guid("3B4E47D6-FEE4-4AA0-ACF4-5CCA4AF13537"),
                FacilityId = Facilities()[1].Id,
                StartYear = 2001, EndYear = 2002,
                BoxNumber = "BOX3"
            }
        };

        public static readonly List<Cabinet> Cabinets = new()
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