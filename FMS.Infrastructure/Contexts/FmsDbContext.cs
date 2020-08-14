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


            //builder.Entity<BudgetCode>().HasData(DevSeedData.GetBudgetCodes());
            //builder.Entity<ComplianceOfficer>().HasData(DevSeedData.GetComplianceOfficers());
            //builder.Entity<County>().HasData(DevSeedData.GetCounties());
            //builder.Entity<EnvironmentalInterest>().HasData(DevSeedData.GetEnvironmentalInterests());
            //builder.Entity<Facility>().HasData(DevSeedData.GetFacilities());
            //builder.Entity<FacilityStatus>().HasData(DevSeedData.GetFacilityStatuses());
            //builder.Entity<FacilityType>().HasData(DevSeedData.GetFacilityTypes());
            //builder.Entity<File>().HasData(DevSeedData.GetFiles());
            //builder.Entity<FileCabinet>().HasData(DevSeedData.GetFileCabinets());
            //builder.Entity<OrganizationalUnit>().HasData(DevSeedData.GetOrganizationalUnits());
            //builder.Entity<RetentionRecord>().HasData(DevSeedData.GetRetentionRecords());


            //builder.Entity<EnforcementOrder>()
            //    .HasIndex(b => b.OrderNumber).IsUnique();
        }
    }
}
