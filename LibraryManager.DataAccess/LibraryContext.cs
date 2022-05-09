using LibraryManager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.DataAccess
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
