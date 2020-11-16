using FMS.Domain.Repositories;
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
            var context = new FmsDbContext(_options, default);
            context.Database.EnsureCreated();
            context.Files.AddRange(SimpleRepositoryData.Files);
            context.Facilities.AddRange(SimpleRepositoryData.Facilities);
            context.BudgetCodes.AddRange(SimpleRepositoryData.BudgetCodes);
            context.Cabinets.AddRange(SimpleRepositoryData.Cabinets);
            context.SaveChanges();
        }

        public IFacilityRepository GetFacilityRepository() =>
            new FacilityRepository(new FmsDbContext(_options, default), GetFileRepository(), GetCabinetRepository());

        public IFileRepository GetFileRepository() =>
            new FileRepository(new FmsDbContext(_options, default));

        public IItemsListRepository GetItemsListRepository() =>
            new ItemsListRepository(new FmsDbContext(_options, default));

        public ICabinetRepository GetCabinetRepository() =>
            new CabinetRepository(new FmsDbContext(_options, default));
    }
}