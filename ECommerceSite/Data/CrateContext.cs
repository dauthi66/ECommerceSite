using ECommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSite.Data
{
    public class CrateContext : DbContext
    {
        //constructor for database, inherit DbcontextOptions, argument options. Used in program.cs
        public CrateContext(DbContextOptions<CrateContext> options) : base(options)
        {

        }

        public DbSet<Crate> Crates { get; set; } = null!;

        public DbSet<Member> Members { get; set; } = null!;
    }
}
