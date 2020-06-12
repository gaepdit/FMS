using Microsoft.EntityFrameworkCore;
using FMS.Models.Models;

namespace FMS.Data
{
    public class FmsDbContext : DbContext
    {
        public FmsDbContext(DbContextOptions<FmsDbContext> options) : base(options) { }
        DbSet<BudgetCode> BudgetCodes { get; set; }
        DbSet<ComplianceOfficer> ComplianceOfficers { get; set; }
        DbSet<County> Counties { get; set; }
        DbSet<EnvironmentalInterest> EnvironmentalInterests { get; set; }
        DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        DbSet<Facility> Facilities { get; set; }
        DbSet<FacilityType> FacilityTypes { get; set; }
        DbSet<File> Files { get; set; }
        DbSet<FileCabinet> FileCabinets { get; set; }
        DbSet<RetentionRecord> RetentionRecords { get; set; }
    }
}
