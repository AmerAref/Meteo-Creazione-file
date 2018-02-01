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
        int IdMaster { get; set; }
    }
}