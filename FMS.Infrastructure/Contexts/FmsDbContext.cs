using FMS.Domain.Data;
using FMS.Domain.Entities;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public FmsDbContext(DbContextOptions<FmsDbContext> options) : base(options) { }

        // App entities
        public DbSet<BudgetCode> BudgetCodes { get; set; }
        public DbSet<ComplianceOfficer> ComplianceOfficers { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityStatus> FacilityStatuses { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<RetentionRecord> RetentionRecords { get; set; }
        public DbSet<CabinetFile> CabinetFileJoin { get; set; }

        // The "Counties" entity is only used to add a County table and data to the database for 
        // database-side use. Counties are stored in memory and never accessed from the database,
        // but other entities store County Id as a foreign key.
        // ReSharper disable once UnusedMember.Global
        public DbSet<County> Counties { get; set; }

        // The "FacilityList" entity is only used for retrieving results from the [dbo].[getNearbyFacilities]
        // stored procedure. (This should not be needed in .NET Core 5.)
        public DbSet<FacilityMapSummaryDto> FacilityList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));

            // Configure many-to-many relationships
            builder.Entity<CabinetFile>().HasKey(e => new { e.CabinetId, e.FileId });

            // Unique indexes
            builder.Entity<File>().HasIndex(e => e.FileLabel).IsUnique();
            builder.Entity<Cabinet>().HasIndex(e => e.Name).IsUnique();
            builder.Entity<FacilityType>().HasIndex(e => e.Name).IsUnique();

            // Identity Tables
            builder.Entity<ApplicationUser>().ToTable("AppUsers");
            builder.Entity<IdentityRole<Guid>>().ToTable("AppRoles");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens");

            // Data
            builder.Entity<County>().HasData(Data.Counties);
        }
    }
}
