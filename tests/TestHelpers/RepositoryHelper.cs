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

        public ItemsListRepository GetItemsListRepository() =>
            new ItemsListRepository(new FmsDbContext(_options));

        public FacilityRepository GetFacilityRepository() =>
            new FacilityRepository(new FmsDbContext(_options));

        public CabinetRepository GetCabinetRepository() =>
            new CabinetRepository(new FmsDbContext(_options));

        public FileRepository GetFileRepository() =>
            new FileRepository(new FmsDbContext(_options));
    }
}
