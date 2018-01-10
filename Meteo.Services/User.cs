using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meteo.Services
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Answer { get; set; }
        public string Language { get; set; }
        public string UnitOfMeasure { get; set; }

        [ForeignKey("Question")]
        public string IdQuestion { get; set; }
        public string Question { get; set; }

        [ForeignKey("Roles")]
        public int IdRole { get; set; }
        public Role Role { get; set; }
    }
}