using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacTypeContext : DbContext
    {
        public FacTypeContext(DbContextOptions<FacTypeContext> options) : base(options)
        {

        }
        DbSet<FacType> FacTypeItems { get; set; }
    }
}
