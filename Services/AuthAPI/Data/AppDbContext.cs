using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderNow.Services.AuthAPI.Models;

namespace OrderNow.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Roles to DB
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Name = "CUSTOMER",
            //    NormalizedName = "CUSTOMER"
            //});
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Name = "ADMIN",
            //    NormalizedName = "ADMIN"
            //});
        }
    }
}
