using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Therania.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Testing> Testings => Set<Testing>();
    public DbSet<Therapist> Therapist => Set<Therapist>();
    public DbSet<Patient> Patient => Set<Patient>();
}