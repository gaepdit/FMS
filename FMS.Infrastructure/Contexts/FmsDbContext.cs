using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FmsDbContext(DbContextOptions<FmsDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options) =>
            _httpContextAccessor = httpContextAccessor;

        // App entity tables
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

        // The "Counties" table is only used to add County data to the database for database-side use.
        // Counties are stored in memory and never accessed from the database, but other entities
        // store County Id as a foreign key.
        // ReSharper disable once UnusedMember.Global
        public DbSet<County> Counties { get; set; }

        // The "FacilityList" table is only used for retrieving "FacilityMapSummaryDto" results from
        // the [dbo].[getNearbyFacilities] stored procedure.
        // (This should not be needed in .NET Core 5.)
        // public DbSet<FacilityMapSummaryDto> FacilityList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));

            // Configure many-to-many relationships
            builder.Entity<CabinetFile>().HasKey(e => new {e.CabinetId, e.FileId});

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

            // Auditing
            var entityTypes = builder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                // Skip the "FacilityList" table
                if (entityType.ClrType.Name == nameof(FacilityMapSummaryDto)) continue;
                // Add auditing properties to all other entity tables
                builder.Entity(entityType.ClrType).Property<DateTimeOffset?>(AuditProperties.InsertDateTime);
                builder.Entity(entityType.ClrType).Property<DateTimeOffset?>(AuditProperties.UpdateDateTime);
                builder.Entity(entityType.ClrType).Property<string>(AuditProperties.InsertUser);
                builder.Entity(entityType.ClrType).Property<string>(AuditProperties.UpdateUser);
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAuditProperties();
            return base.SaveChanges();
        }

        private void SetAuditProperties()
        {
            var currentUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property(AuditProperties.UpdateDateTime).CurrentValue = DateTimeOffset.Now;
                entry.Property(AuditProperties.UpdateUser).CurrentValue = currentUser;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(AuditProperties.InsertDateTime).CurrentValue = DateTimeOffset.Now;
                    entry.Property(AuditProperties.InsertUser).CurrentValue = currentUser;
                }
            }
        }

        private static class AuditProperties
        {
            public const string InsertDateTime = "InsertDateTime";
            public const string UpdateDateTime = "UpdateDateTime";
            public const string InsertUser = "InsertUser";
            public const string UpdateUser = "UpdateUser";
        }
    }
}