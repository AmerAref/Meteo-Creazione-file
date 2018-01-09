using Microsoft.EntityFrameworkCore;

namespace Meteo.Services
{
    public static class UsersContextFactory
    {
        public static Context Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new Context(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}