/* using Microsoft.EntityFrameworkCore;
using Meteo.Services.Models;

namespace Meteo.Services.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbCont1ext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Forecast> Forecast { get; set; }
        public DbSet<OneDayForecast> OneDayForecast { get; set; }
        public DbSet<LastFiveDaysForecast> LastFiveDaysForecast { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
             //Context Configuration
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             //ENTITY MAPPING
        }
    }
} */