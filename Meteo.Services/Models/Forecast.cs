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

        [ForeignKey("City")]
        public int IdCity { get; set; }

        public double Pressure;
        public double Humidity;
        public double Temperature;
        public double TemperatureMin;
        public double TemperatureMax;

        public DateTime WeatherDate { get; set; }

        [ForeignKey("Master")]
        public int IdMaster { get; set; }
    }
}