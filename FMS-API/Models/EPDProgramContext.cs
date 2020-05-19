using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class EPDProgramContext : DbContext
    {
        public EPDProgramContext(DbContextOptions<EPDProgramContext> options) : base(options)
        {

        }
        DbSet<EPDProgram> EPDProgramItems { get; set; }
    }
}
