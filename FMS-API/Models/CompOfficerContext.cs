using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMS_API.Models
{
    public class CompOfficerContext : DbContext
    {
        public CompOfficerContext(DbContextOptions<CompOfficerContext> options) : base(options)
        {

        }
        DbSet<CompOfficer> CompOfficerItems { get; set; }
    }
}
