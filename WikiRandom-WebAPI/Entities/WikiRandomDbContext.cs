using Microsoft.EntityFrameworkCore;

namespace WikiRandom_WebAPI.Entities
{
    public class WikiRandomDbContext : DbContext
    {
        private readonly string _dbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=WikiRandomDb;Trusted_Connection=True";
        public DbSet<Article> Articles { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(p => p.Content)
                .HasMaxLength(255);

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString);
        }


    }
}
