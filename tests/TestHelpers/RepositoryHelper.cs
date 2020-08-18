using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.SeedData;
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
            context.SeedTestData();
        }

        public FacilityRepository GetFacilityRepository()
        {
            return new FacilityRepository(new FmsDbContext(_options));
        }
    }
}
