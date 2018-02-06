using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Meteo.Services.Models
{
    public class Forecast
    {
        [Key]
        public int IdForecast { get; set; }

        public string CityName { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public DateTime WeatherDate { get; set; }

        [ForeignKey("Master")]
        public int IdMaster { get; set; }

        [ForeignKey("City")]
        public int IdCity { get; set; }
    }
}