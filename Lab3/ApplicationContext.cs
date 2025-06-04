using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<BookYear> BookYear { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=books.db"); 
        }
    }
}