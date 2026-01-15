using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.TestData.SeedData;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace FMS.Infrastructure.Tests
{
    public class RepositoryHelper : IDisposable
    {
        private readonly DbContextOptions<FmsDbContext> _options = SqliteInMemory.CreateOptions<FmsDbContext>();
        private FmsDbContext _context { get; set; }

        private RepositoryHelper()
        {
            _context = new FmsDbContext(_options, default);
            _context.Database.EnsureCreated();
        }

        public static async Task<RepositoryHelper> CreateRepositoryHelperAsync()
        {
            var helper = new RepositoryHelper();
            await SeedData.SeedDataAsync(helper._context, default);
            return helper;
        }

        public void ClearChangeTracker() => _context.ChangeTracker.Clear();

        public IFacilityRepository GetFacilityRepository()
        {
            return new FacilityRepository(new FmsDbContext(_options, default));
        }

        public IFileRepository GetFileRepository()
        {
            return new FileRepository(new FmsDbContext(_options, default));
        }

        public IItemsListRepository GetItemsListRepository()
        {
            return new ItemsListRepository(new FmsDbContext(_options, default));
        }

        public ICabinetRepository GetCabinetRepository()
        {
            return new CabinetRepository(new FmsDbContext(_options, default));
        }

        // Copies of same data from RepositoryData
        // to prevent entity tracking errors when seeding
        // test database above 

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _context?.Dispose();
        }
    }
}