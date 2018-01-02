using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Meteo.Services
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}