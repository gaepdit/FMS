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
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<AbandonedInactive> AbandonedInactives { get; set; }
        public DbSet<ActionTaken> ActionsTaken { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<EventContractor> EventContractors { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<AllowedActionTaken> AllowedActionsTaken { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<ParcelType> ParcelTypes { get; set; }
        public DbSet<GapsAssessment> GapsAssessments { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<HsrpFacilityProperties> HsrpFacilityProperties { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<LocationClass> LocationClasses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<OverallStatus> OverallStatuses { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<OnsiteScore> OnsiteScores { get; set; }
        public DbSet<SoilStatus> SoilStatuses { get; set; }
        public DbSet<SourceStatus> SourceStatuses { get; set; }
        public DbSet<GroundwaterStatus> GroundwaterStatuses { get; set; }
        public DbSet<GroundwaterScore> GroundwaterScores { get; set; }
        public DbSet<Substance> Substances { get; set; }
        public DbSet<Status> Statuses { get; set; }
        


        // The "Counties" table is only used to add County data to the database for database-side use.
        // Counties are stored in memory and never accessed from the database, but other entities
        // store County Id as a foreign key.
        // ReSharper disable once UnusedMember.Global
        public DbSet<County> Counties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));

            // Unique indexes
            builder.Entity<File>().HasIndex(e => e.FileLabel).IsUnique();
            builder.Entity<Cabinet>().HasIndex(e => e.Name).IsUnique();
            builder.Entity<FacilityType>().HasIndex(e => e.Name).IsUnique();
            builder.Entity<Facility>().HasIndex(e => e.FileId).
                IncludeProperties(e => new {e.Active, e.FacilityNumber, e.FacilityTypeId, e.OrganizationalUnitId, e.BudgetCodeId, e.Name, e.ComplianceOfficerId, e.FacilityStatusId, e.Location, e.Address, e.City, e.State, e.PostalCode, e.Latitude, e.Longitude, e.CountyId, e.IsRetained, e.AdditionalDataRequested, e.Comments, e.DeferredOnSiteScoring, e.DeterminationLetterDate, e.HSInumber, e.HasERecord, e.HistoricalComplianceOfficer, e.HistoricalUnit, e.ImageChecked, e.PreRQSMcleanup, e.RNDateReceived, e.VRPReferral });
            builder.Entity<Facility>().HasIndex(e => new { e.Active, e.FileId });
            builder.Entity<Facility>().HasIndex(e => e.Active).IncludeProperties(e => new { e.FileId });
            builder.Entity<Facility>().HasIndex(e => new { e.Active, e.FacilityTypeId, e.Address });
            builder.Entity<Facility>().HasIndex(e => new { e.Active, e.FacilityTypeId, e.Address, e.City });
            builder.Entity<RetentionRecord>().HasIndex(e => e.FacilityId);
            builder.Entity<RetentionRecord>().HasIndex(e => e.FacilityId).
                IncludeProperties(e => new { e.Active, e.StartYear, e.EndYear, e.ConsignmentNumber, e.BoxNumber, e.ShelfNumber, e.RetentionSchedule });

            // Identity Tables
            builder.Entity<ApplicationUser>().ToTable("AppUsers")
                .HasIndex(user => user.ObjectId).IsUnique();
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
            foreach (var entityType in entityTypes.Select(e => e.ClrType)
                         // Skip the "FacilityList" table
                         .Where(e => e.Name != nameof(FacilityMapSummaryDto)))
            {
                // Add auditing properties to all other entity tables
                builder.Entity(entityType).Property<DateTimeOffset?>(AuditProperties.InsertDateTime);
                builder.Entity(entityType).Property<DateTimeOffset?>(AuditProperties.UpdateDateTime);
                builder.Entity(entityType).Property<string>(AuditProperties.InsertUser);
                builder.Entity(entityType).Property<string>(AuditProperties.UpdateUser);
            }
            
            // Fix primary key error for IdentityPasskeyData
            builder.Entity<IdentityPasskeyData>(e => e.HasNoKey());
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
