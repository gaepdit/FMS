using Microsoft.EntityFrameworkCore;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : DbContext
    {

        public FmsDbContext(DbContextOptions<FmsDbContext> options) : base(options) { }
            //=> options = new DbContextOptionsBuilder<FmsDbContext>().EnableSensitiveDataLogging(true).Options;
        public DbSet<BudgetCode> BudgetCodes { get; set; }
        public DbSet<ComplianceOfficer> ComplianceOfficers { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<EnvironmentalInterest> EnvironmentalInterests { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityStatus> FacilityStatuses { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileCabinet> FileCabinets { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<RetentionRecord> RetentionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));
            
            builder.Entity<County>().HasData(ProdSeedData.GetCounties());
        }
    }
}
