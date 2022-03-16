using Microsoft.EntityFrameworkCore;

namespace Therania.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Testing> Testings { get; set; }
}