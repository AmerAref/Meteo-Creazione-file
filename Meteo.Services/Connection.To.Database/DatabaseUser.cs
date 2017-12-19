using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MySql;

namespace Meteo.Services
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
    public static class UsersContextFactory
    {
        public static UsersContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
            optionsBuilder.UseMySQL(connectionString);

            //Ensure database creation
            var context = new UsersContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
    public class User
    {
        public User()
        {
        }

        [Key]
        public int idUtente { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
    }
}