using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Meteo.Services.Models
{
    public class Forecast
    {
        [Key]
        public int IdForecast { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CityName { get; set; }

        [ForeignKey("Master")]
        public int IdMaster { get; set; }

        [ForeignKey("City")]
        public int IdCity { get; set; }
    }
}