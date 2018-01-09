using Microsoft.EntityFrameworkCore;

namespace Meteo.Services.Infrastructure
{
    public static class UsersContextFactory
    {
        public static ApplicationDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new ApplicationDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}