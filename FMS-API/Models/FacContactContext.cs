using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacContactContext : DbContext
    {
        public FacContactContext(DbContextOptions<FacContactContext> options) : base(options)
        {

        }
        DbSet<FacContact> FacContactItems { get; set; }
    }
}
