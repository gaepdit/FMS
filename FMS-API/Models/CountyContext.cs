using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMS_API.Models
{
    public class CountyContext : DbContext
    {
        public CountyContext(DbContextOptions<CountyContext> options) : base(options)
        {

        }
        DbSet<County> CountyItems { get; set; }
    }
}
