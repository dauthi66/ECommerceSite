using ECommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSite.Data
{
    public class CrateContext : DbContext
    {
        //constructor for database, inherit DbcontextOptions, argument options
        public CrateContext(DbContextOptions<CrateContext> options) : base(options)
        {

        }

        public DbSet<Crate> Crates { get; set; }
    }
}
