using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public PubContext() {}
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=PubDatabase;Trusted_Connection=True;");
        }
    }
}