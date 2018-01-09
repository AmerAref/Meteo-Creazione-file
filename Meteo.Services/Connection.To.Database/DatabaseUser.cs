using System;
using Microsoft.EntityFrameworkCore;


namespace Meteo.Services
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> QuestionForUsers { get; set; }

        internal object GetConnectionString(string v)
        {
            throw new NotImplementedException();
        }
    }
}
   