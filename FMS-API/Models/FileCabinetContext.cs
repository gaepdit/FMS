using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FileCabinetContext : DbContext
    {
        public FileCabinetContext(DbContextOptions<FileCabinetContext> options) : base(options)
        {

        }
        DbSet<FileCabinet> FileCabinetItems { get; set; }
    }
}
