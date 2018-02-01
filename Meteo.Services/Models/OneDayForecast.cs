using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Meteo.Services.Models
{
    public class OneDayForecast
    {
        [Key]
        public int IdOneDayForecast { get; set; }

        public double Pressure { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }

        [ForeignKey("Forecast")]
        int IdForecast { get; set; }
    }
}
