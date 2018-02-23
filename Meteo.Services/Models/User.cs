using System.ComponentModel.DataAnnotations;

namespace Meteo.Services.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string UnitOfMeasure { get; set; }

        public int IdQuestion { get; set; }
        public string Answer { get; set; }

        public int IdRole { get; set; }
        public int IdLanguage { get; set; }
    }
}