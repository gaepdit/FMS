using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace TestHelpers.SimpleRepository
{
    public class SimpleRepositoryHelper
    {
        private readonly DbContextOptions<FmsDbContext> _options = SqliteInMemory.CreateOptions<FmsDbContext>();

        public SimpleRepositoryHelper()
        {
            var context = new FmsDbContext(_options);
            context.Database.EnsureCreated();
            context.Files.AddRange(SimpleRepositoryData.Files);
            context.Facilities.AddRange(SimpleRepositoryData.Facilities);
            context.BudgetCodes.AddRange(SimpleRepositoryData.BudgetCodes);
            context.SaveChanges();
        }

        public FacilityRepository GetFacilityRepository() =>
            new FacilityRepository(new FmsDbContext(_options), GetFileRepository());

        public FileRepository GetFileRepository() =>
            new FileRepository(new FmsDbContext(_options));

        public ItemsListRepository GetItemsListRepository() =>
            new ItemsListRepository(new FmsDbContext(_options));
    }
}