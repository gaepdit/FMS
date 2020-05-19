using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacilityContext : DbContext
    {
        public FacilityContext(DbContextOptions<FacilityContext> options) : base(options)
        {

        }

        public DbSet<Facility> FacilityItems { get; set; }
    }
}
