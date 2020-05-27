using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Data
{
    public class FMSContext : DbContext
    {
        public FMSContext(DbContextOptions<FMSContext> options) : base(options) { }
        DbSet<Address> AddressItems { get; set; }
        DbSet<Budget> BudgetItems { get; set; }
        DbSet<CompOfficer> CompOfficerItems { get; set; }
        DbSet<County> CountyItems { get; set; }
        DbSet<Email> EmailItems { get; set; }
        DbSet<EPDProgram> EPDProgramItems { get; set; }
        DbSet<EPDUnit> EPDUnitItems { get; set; }
        DbSet<FacAddress> FacAddressItems { get; set; }
        DbSet<FacContact> FacContactItems { get; set; }
        DbSet<FacContactType> FacContactTypeItems { get; set; }
        DbSet<Facility> FacilityItems { get; set; }
        DbSet<FacLatLong> FacLatLongItems { get; set; }
        DbSet<FacType> FacTypeItems { get; set; }
        DbSet<File> FileItems { get; set; }
        DbSet<FileCabinet> FileCabinetItems { get; set; }
        DbSet<FileLoc> FileLocItems { get; set; }
        DbSet<Phone> PhoneItems { get; set; }
        DbSet<User> UserItems { get; set; }

    }
}
