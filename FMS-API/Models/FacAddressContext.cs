using Microsoft.EntityFrameworkCore;

namespace FMS_API.Models
{
    public class FacAddressContext : DbContext
    {
        public FacAddressContext(DbContextOptions<FacAddressContext> options) : base(options)
        {

        }
        DbSet<FacAddress> FacAddressItems { get; set; }
    }
}
