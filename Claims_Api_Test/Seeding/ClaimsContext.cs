using Claims_Api_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Claims_Api_Test.Seeding;

public class ClaimsContext : DbContext
{
    public ClaimsContext(DbContextOptions<ClaimsContext> options)
        : base(options)
    {
    }

    public DbSet<Claim> Claims { get; set; }
    public DbSet<ClaimType> ClaimTypes { get; set; }
    public DbSet<Company> Companies { get; set; }
}
