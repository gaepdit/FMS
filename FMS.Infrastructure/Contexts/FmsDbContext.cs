using Microsoft.EntityFrameworkCore;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : DbContext
    {

        public FmsDbContext(DbContextOptions<FmsDbContext> options) : base(options) => options = new DbContextOptionsBuilder<FmsDbContext>().EnableSensitiveDataLogging(true).Options;
        DbSet<BudgetCode> BudgetCodes { get; set; }
        DbSet<ComplianceOfficer> ComplianceOfficers { get; set; }
        DbSet<County> Counties { get; set; }
        DbSet<EnvironmentalInterest> EnvironmentalInterests { get; set; }
        DbSet<Facility> Facilities { get; set; }
        DbSet<FacilityStatus> FacilityStatuses { get; set; }
        DbSet<FacilityType> FacilityTypes { get; set; }
        DbSet<File> Files { get; set; }
        DbSet<FileCabinet> FileCabinets { get; set; }
        DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        DbSet<RetentionRecord> RetentionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));


            builder.Entity<BudgetCode>().HasData(DevSeedData.GetBudgetCodes());
            builder.Entity<ComplianceOfficer>().HasData(DevSeedData.GetComplianceOfficers());
            builder.Entity<County>().HasData(DevSeedData.GetCounties());
            builder.Entity<EnvironmentalInterest>().HasData(DevSeedData.GetEnvironmentalInterests());
            builder.Entity<Facility>().HasData(DevSeedData.GetFacilities());
            builder.Entity<FacilityStatus>().HasData(DevSeedData.GetFacilityStatuses());
            builder.Entity<FacilityType>().HasData(DevSeedData.GetFacilityTypes());
            builder.Entity<File>().HasData(DevSeedData.GetFiles());
            builder.Entity<FileCabinet>().HasData(DevSeedData.GetFileCabinets());
            builder.Entity<OrganizationalUnit>().HasData(DevSeedData.GetOrganizationalUnits());
            builder.Entity<RetentionRecord>().HasData(DevSeedData.GetRetentionRecords());


            //builder.Entity<EnforcementOrder>()
            //    .HasIndex(b => b.OrderNumber).IsUnique();
        }
    }
}
