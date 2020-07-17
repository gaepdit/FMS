using Microsoft.EntityFrameworkCore;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : DbContext
    {
        public FmsDbContext(DbContextOptions<FmsDbContext> options) : base(options) { }
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

            builder.Entity<County>().HasData(ProdSeedData.GetCounties());
            //builder.Entity<LegalAuthority>().HasData(ProdSeedData.GetLegalAuthorities());
            //builder.Entity<Address>().HasData(ProdSeedData.GetAddresses());
            //builder.Entity<EpdContact>().HasData(ProdSeedData.GetEpdContacts());

            //builder.Entity<EnforcementOrder>()
            //    .HasIndex(b => b.OrderNumber).IsUnique();
        }
    }
}
