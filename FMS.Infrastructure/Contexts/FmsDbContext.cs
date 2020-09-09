﻿using FMS.Domain.Data;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FMS.Infrastructure.Contexts
{
    public class FmsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<RetentionRecord> RetentionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder ?? throw new ArgumentNullException(nameof(builder)));

            // Additional model properties
            builder.Entity<Cabinet>().HasIndex(e => e.Name).IsUnique();

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
