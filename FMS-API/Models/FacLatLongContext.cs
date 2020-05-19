using Microsoft.EntityFrameworkCore;

namespace FMS_API.Models
{
    public class FacLatLongContext : DbContext
    {
        public FacLatLongContext(DbContextOptions<FacLatLongContext> options) : base(options)
        {

        }
        DbSet<FacLatLong> FacLatLongItems { get; set; }
    }
}
