using System.ComponentModel.DataAnnotations;

namespace Meteo.Services.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public int IdCity { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }

        
    }
}
