using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class EPDUnitContext : DbContext
    {
        public EPDUnitContext(DbContextOptions<EPDUnitContext> options) : base(options)
        {

        }
        DbSet<EPDUnit> EPDUnitItems { get; set; }
    }
}
