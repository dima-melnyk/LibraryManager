using LibraryManager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.DataAccess
{
    public class LibraryContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Book> Books { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Image)
                .WithOne(i => i.Book);
        }
    }
}
