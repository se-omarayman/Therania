using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Therania.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Testing> Testings { get; set; }
    public DbSet<TherapistUser> TherapistUsers { get; set; }
    public DbSet<PatientUser> PatientUsers { get; set; }
}