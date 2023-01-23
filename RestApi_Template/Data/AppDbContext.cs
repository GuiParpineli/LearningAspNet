using Microsoft.EntityFrameworkCore;
using RestApi_Template.Models;

namespace RestApi_Template.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Books);
            new DbInitializer(builder).Seed();
        }

    }
}
