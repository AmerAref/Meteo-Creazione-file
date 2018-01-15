using System.ComponentModel.DataAnnotations;

namespace Meteo.Services.Models
{
    public class Question
    {
        [Key]
        public int IdQuestion { get; set; }
        public string DefaultQuestion { get; set; }
    }
}