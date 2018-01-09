using System.ComponentModel.DataAnnotations;

namespace Meteo.Services
{
    public class User
    {
        [Key]
        public int IdUtente { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}