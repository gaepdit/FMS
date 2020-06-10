using Microsoft.EntityFrameworkCore;
using FMS.Models;

namespace FMS.Data
{
    public class FMSContext : DbContext
    {
        public FMSContext(DbContextOptions<FMSContext> options) : base(options) { }
        DbSet<Budget> BudgetItems { get; set; }
        DbSet<ComplianceOfficer> ComplianceOfficerItems { get; set; }
        DbSet<County> CountyItems { get; set; }
        DbSet<EPDProgram> EPDProgramItems { get; set; }
        DbSet<EPDUnit> EPDUnitItems { get; set; }
        DbSet<Facility> FacilityItems { get; set; }
        DbSet<FacilityType> FacTypeItems { get; set; }
        DbSet<File> FileItems { get; set; }
        DbSet<FileCabinet> FileCabinetItems { get; set; }
        DbSet<RetentionInfo> RetentionInfoItems { get; set; }

        //DbSet<User> UserItems { get; set; }

    }
}
