using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarSaleManage.Models;

namespace CarSaleManage.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vehicle> Vehicle { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Vehicle>()
                .HasOne(v => v.AppUser)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.AppUserId)
                .IsRequired(false);
        }
    }
}
