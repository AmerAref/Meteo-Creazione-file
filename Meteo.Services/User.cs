using System.ComponentModel.DataAnnotations;

namespace Meteo.Services
{
    public class User
    {
        public User() { }

        [Key]
        public int idUtente { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}