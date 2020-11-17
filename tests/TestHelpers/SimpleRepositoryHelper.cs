using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace TestHelpers
{
    public class SimpleRepositoryHelper
    {
        private readonly DbContextOptions<FmsDbContext> _options = SqliteInMemory.CreateOptions<FmsDbContext>();

        public SimpleRepositoryHelper()
        {
            using (var context = new FmsDbContext(_options, default))
            {
                context.Database.EnsureCreated();

                context.Cabinets.AddRange(SimpleRepositoryData.Cabinets);
                context.BudgetCodes.AddRange(SimpleRepositoryData.BudgetCodes);
                context.FacilityStatuses.AddRange(SimpleRepositoryData.FacilityStatuses);
                context.FacilityTypes.AddRange(SimpleRepositoryData.FacilityTypes);
                context.OrganizationalUnits.AddRange(SimpleRepositoryData.OrganizationalUnits);
                context.Files.AddRange(SimpleRepositoryData.Files);
                context.SaveChanges();
            }

            using (var context = new FmsDbContext(_options, default))
            {
                var counties = context.Counties.ToList();
                var facilities = Facilities(counties);
                var records = RetentionRecords(facilities);

                context.Facilities.AddRange(facilities);
                context.RetentionRecords.AddRange(records);

                context.SaveChanges();
            }
        }

        public IFacilityRepository GetFacilityRepository() =>
            new FacilityRepository(new FmsDbContext(_options, default));

        public IFileRepository GetFileRepository() =>
            new FileRepository(new FmsDbContext(_options, default));

        public IItemsListRepository GetItemsListRepository() =>
            new ItemsListRepository(new FmsDbContext(_options, default));

        public ICabinetRepository GetCabinetRepository() =>
            new CabinetRepository(new FmsDbContext(_options, default));

        // Copies of same data from SimpleRepositoryData
        // to prevent entity tracking errors when seeding
        // test database above 
        private static List<Facility> Facilities(List<County> counties)
        {
            return new List<Facility>
            {
                new Facility
                {
                    Id = new Guid("4C730563-8D90-4C34-B0C9-059A3E066267"),
                    FacilityNumber = "ABC",
                    FileId = SimpleRepositoryData.Files[0].Id,
                    County = counties.Find(e => e.Id == 131),
                    Location = "somewhere",
                },
                new Facility
                {
                    Id = new Guid("26DCA0FD-F586-4D0D-AF2D-865E2B7922A2"),
                    FacilityNumber = "ABC123",
                    FileId = SimpleRepositoryData.Files[0].Id,
                    County = counties.Find(e => e.Id == 131),
                    Location = "elsewhere",
                },
                new Facility
                {
                    Id = new Guid("885F8638-2FF6-4E7A-ADE9-13FEAA34ABD2"),
                    FacilityNumber = "DEF",
                    FileId = SimpleRepositoryData.Files[1].Id,
                    County = counties.Find(e => e.Id == 131),
                    Location = "",
                    Active = false,
                    IsRetained = false,
                },
                new Facility
                {
                    Id = new Guid("633ACE4C-39B8-48EA-A5BC-6AF5DA56244F"),
                    FacilityNumber = "GHI",
                    FileId = SimpleRepositoryData.Files[0].Id,
                    County = counties.Find(e => e.Id == 99),
                    Location = "nowhere",
                },
                new Facility
                {
                    Id = new Guid("61A70D58-F3EF-4716-93EC-698A8FABAE19"),
                    FacilityNumber = "JKL",
                    FileId = SimpleRepositoryData.Files[3].Id,
                    County = counties.Find(e => e.Id == 102),
                    Location = "here",
                },
                new Facility
                {
                    Id = new Guid("08E54076-392C-46B8-883E-B302E215B053"),
                    FacilityNumber = "MNO",
                    FileId = SimpleRepositoryData.Files[4].Id,
                    County = counties.Find(e => e.Id == 103),
                    Location = "",
                },
            };
        }

        private static List<RetentionRecord> RetentionRecords(IReadOnlyList<Facility> facilities)
        {
            return new List<RetentionRecord>
            {
                new RetentionRecord
                {
                    Id = new Guid("66400D35-0B56-46C8-B5E4-A569664DD589"),
                    FacilityId = SimpleRepositoryData.Facilities[0].Id,
                    StartYear = 2003, EndYear = 2009,
                    BoxNumber = "BOX1"
                },
                new RetentionRecord
                {
                    Id = new Guid("0F829C6B-4800-4B08-B779-48FC85A01338"),
                    FacilityId = SimpleRepositoryData.Facilities[0].Id,
                    StartYear = 2010, EndYear = 2016,
                    BoxNumber = "BOX2"
                },
                new RetentionRecord
                {
                    Id = new Guid("3B4E47D6-FEE4-4AA0-ACF4-5CCA4AF13537"),
                    FacilityId = SimpleRepositoryData.Facilities[1].Id,
                    StartYear = 2001, EndYear = 2002,
                    BoxNumber = "BOX3"
                },
            };
        }
    }
}