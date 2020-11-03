using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace TestHelpers.SimpleRepository
{
    public static class SimpleRepositoryData
    {
        // Data

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

        public static readonly List<Cabinet> Cabinets = new List<Cabinet>
        {
            new Cabinet {Id = Guid.NewGuid(), Name = "C001", FirstFileLabel = "000-0000"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C002", FirstFileLabel = "103-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C003", FirstFileLabel = "110-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C004", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C005", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C006", FirstFileLabel = "999-0001", Active = false},
        };

        public static readonly List<CabinetFile> CabinetFiles = new List<CabinetFile>
        {
            new CabinetFile {FileId = Files[0].Id, CabinetId = Cabinets[0].Id},
            new CabinetFile {FileId = Files[1].Id, CabinetId = Cabinets[3].Id},
            new CabinetFile {FileId = Files[1].Id, CabinetId = Cabinets[4].Id},
            new CabinetFile {FileId = Files[2].Id, CabinetId = Cabinets[0].Id},
            new CabinetFile {FileId = Files[3].Id, CabinetId = Cabinets[0].Id},
            new CabinetFile {FileId = Files[4].Id, CabinetId = Cabinets[2].Id},
            new CabinetFile {FileId = Files[5].Id, CabinetId = Cabinets[2].Id},
        };


        // DTO helpers

        public static CabinetSummaryDto GetCabinetSummary(Guid id) =>
            new CabinetSummaryDto(Cabinets.Find(e => e.Id == id));

        public static CabinetDetailDto GetCabinetDetail(Guid id)
        {
            var cabinet = Cabinets.Find(e => e.Id == id);
            return new CabinetDetailDto(cabinet);
        }
    }
}