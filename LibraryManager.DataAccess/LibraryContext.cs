using LibraryManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManager.DataAccess
{
    public class LibraryContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
               new IdentityRole<int>("User") { Id = 1, NormalizedName = "USER" },
               new IdentityRole<int>("Admin") { Id = 2, NormalizedName = "ADMIN" });

            var hasher = new PasswordHasher<IdentityUser<int>>();
            modelBuilder.Entity<IdentityUser<int>>().HasData(
                new IdentityUser<int>
                {
                    Id = 1,
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@nulp.com",
                    NormalizedEmail = "ADMIN@NULP.COM",
                    PasswordHash = hasher.HashPassword(null, "adm1n_Pass"),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 2
                });
        }
    }
}
