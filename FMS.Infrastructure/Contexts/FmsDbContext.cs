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
        public DbSet<ActionTaken> ActionTaken { get; set; }


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

            ////                CREATE INDEX missing_index_856_855 ON [FMS].[dbo].[Facilities] ([FileId]) INCLUDE ([Active], [FacilityNumber], [FacilityTypeId], [OrganizationalUnitId], [BudgetCodeId], [Name], [ComplianceOfficerId], [FacilityStatusId], [Location], [Address], [City], [State], [PostalCode], [Latitude], [Longitude], [CountyId], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [IsRetained], [AdditionalDataRequested], [Comments], [DeferredOnSiteScoring], [DeterminationLetterDate], [HSInumber], [HasERecord], [HistoricalComplianceOfficer], [HistoricalUnit], [ImageChecked], [PreRQSMcleanup], [RNDateReceived], [VRPReferral])
            ////CREATE INDEX missing_index_856_855 ON [FMS].[dbo].[Facilities] ([FileId]) INCLUDE ([Active], [FacilityNumber], [FacilityTypeId], [OrganizationalUnitId], [BudgetCodeId], [Name], [ComplianceOfficerId], [FacilityStatusId], [Location], [Address], [City], [State], [PostalCode], [Latitude], [Longitude], [CountyId], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [IsRetained], [AdditionalDataRequested], [Comments], [DeferredOnSiteScoring], [DeterminationLetterDate], [HSInumber], [HasERecord], [HistoricalComplianceOfficer], [HistoricalUnit], [ImageChecked], [PreRQSMcleanup], [RNDateReceived], [VRPReferral])
            ////CREATE INDEX missing_index_465_464 ON [FMS].[dbo].[RetentionRecords] ([FacilityId]) INCLUDE ([Active], [StartYear], [EndYear], [ConsignmentNumber], [BoxNumber], [ShelfNumber], [RetentionSchedule], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser])
            ////CREATE INDEX missing_index_467_466 ON [FMS].[dbo].[RetentionRecords] ([FacilityId])
            ////CREATE INDEX missing_index_465_464 ON [FMS].[dbo].[RetentionRecords] ([FacilityId]) INCLUDE ([Active], [StartYear], [EndYear], [ConsignmentNumber], [BoxNumber], [ShelfNumber], [RetentionSchedule], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser])
            ////CREATE INDEX missing_index_467_466 ON [FMS].[dbo].[RetentionRecords] ([FacilityId])
            ////CREATE INDEX missing_index_607_606 ON [FMS].[dbo].[Facilities] ([Active], [FileId])
            ////CREATE INDEX missing_index_607_606 ON [FMS].[dbo].[Facilities] ([Active], [FileId])
            ////CREATE INDEX missing_index_609_608 ON [FMS].[dbo].[Facilities] ([Active]) INCLUDE ([FileId])
            ////CREATE INDEX missing_index_609_608 ON [FMS].[dbo].[Facilities] ([Active]) INCLUDE ([FileId])
            ////CREATE INDEX missing_index_997_996 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address])
            ////CREATE INDEX missing_index_997_996 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address])
            ////CREATE INDEX missing_index_1159_1158 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address], [City])
            ////CREATE INDEX missing_index_1159_1158 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address], [City])CREATE INDEX missing_index_856_855 ON [FMS].[dbo].[Facilities] ([FileId]) INCLUDE ([Active], [FacilityNumber], [FacilityTypeId], [OrganizationalUnitId], [BudgetCodeId], [Name], [ComplianceOfficerId], [FacilityStatusId], [Location], [Address], [City], [State], [PostalCode], [Latitude], [Longitude], [CountyId], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [IsRetained], [AdditionalDataRequested], [Comments], [DeferredOnSiteScoring], [DeterminationLetterDate], [HSInumber], [HasERecord], [HistoricalComplianceOfficer], [HistoricalUnit], [ImageChecked], [PreRQSMcleanup], [RNDateReceived], [VRPReferral])
            ////CREATE INDEX missing_index_856_855 ON [FMS].[dbo].[Facilities] ([FileId]) INCLUDE ([Active], [FacilityNumber], [FacilityTypeId], [OrganizationalUnitId], [BudgetCodeId], [Name], [ComplianceOfficerId], [FacilityStatusId], [Location], [Address], [City], [State], [PostalCode], [Latitude], [Longitude], [CountyId], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [IsRetained], [AdditionalDataRequested], [Comments], [DeferredOnSiteScoring], [DeterminationLetterDate], [HSInumber], [HasERecord], [HistoricalComplianceOfficer], [HistoricalUnit], [ImageChecked], [PreRQSMcleanup], [RNDateReceived], [VRPReferral])
            ////CREATE INDEX missing_index_465_464 ON [FMS].[dbo].[RetentionRecords] ([FacilityId]) INCLUDE ([Active], [StartYear], [EndYear], [ConsignmentNumber], [BoxNumber], [ShelfNumber], [RetentionSchedule], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser])
            ////CREATE INDEX missing_index_467_466 ON [FMS].[dbo].[RetentionRecords] ([FacilityId])
            ////CREATE INDEX missing_index_465_464 ON [FMS].[dbo].[RetentionRecords] ([FacilityId]) INCLUDE ([Active], [StartYear], [EndYear], [ConsignmentNumber], [BoxNumber], [ShelfNumber], [RetentionSchedule], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser])
            ////CREATE INDEX missing_index_467_466 ON [FMS].[dbo].[RetentionRecords] ([FacilityId])
            ////CREATE INDEX missing_index_607_606 ON [FMS].[dbo].[Facilities] ([Active], [FileId])
            ////CREATE INDEX missing_index_607_606 ON [FMS].[dbo].[Facilities] ([Active], [FileId])
            ////CREATE INDEX missing_index_609_608 ON [FMS].[dbo].[Facilities] ([Active]) INCLUDE ([FileId])
            ////CREATE INDEX missing_index_609_608 ON [FMS].[dbo].[Facilities] ([Active]) INCLUDE ([FileId])
            ////CREATE INDEX missing_index_997_996 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address])
            ////CREATE INDEX missing_index_997_996 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address])
            ////CREATE INDEX missing_index_1159_1158 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address], [City])
            ////CREATE INDEX missing_index_1159_1158 ON [FMS].[dbo].[Facilities] ([Active], [FacilityTypeId],[Address], [City])

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
