using System;
using Microsoft.EntityFrameworkCore;


namespace Meteo.Services
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> QuestionForUsers { get; set; }

        internal object GetConnectionString(string v)
        {
            throw new NotImplementedException();
        }
    }
}
   