using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FMS.Infrastructure.Contexts
{
    /// <summary>
    /// Facilitates some EF Core Tools commands. See "Design-time DbContext Creation":
    /// https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory
    /// </summary>
    [UsedImplicitly]
    public class FmsDbContextFactory : IDesignTimeDbContextFactory<FmsDbContext>
    {
        public FmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FmsDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=fms-temp;");
            return new FmsDbContext(optionsBuilder.Options, null);
        }
    }
}
