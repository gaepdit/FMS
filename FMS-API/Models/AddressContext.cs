using Microsoft.EntityFrameworkCore;

namespace FMS_API.Models
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions<AddressContext> options):base (options)
        {
           
        }
        DbSet<Address> AddressItems { get; set; }

    }
}