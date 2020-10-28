using System.Linq;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.SeedData;
using FMS.Infrastructure.SeedData.TestData;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace TestHelpers
{
    public class RepositoryHelper
    {
        private readonly DbContextOptions<FmsDbContext> _options = SqliteInMemory.CreateOptions<FmsDbContext>();

        public RepositoryHelper()
        {
            var context = new FmsDbContext(_options);

            context.Database.EnsureCreated();
            context.SeedData();
            context.SeedTestData();

            if (!context.RetentionRecords.Any()) context.RetentionRecords.AddRange(TestData.GetRetentionRecords());
            context.SaveChanges();
        }

        public FacilityRepository GetFacilityRepository() =>
            new FacilityRepository(new FmsDbContext(_options), GetFileRepository());

        public FileRepository GetFileRepository() =>
            new FileRepository(new FmsDbContext(_options));
    }
}