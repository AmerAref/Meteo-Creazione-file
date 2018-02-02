using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Meteo.Services.Models
{
    public class FiveDaysForecast
    {
        [Key]
        public int IdLastFiveDaysForecast { get; set; }

        public float Pressure { get; set; }
        public float Temp { get; set; }
        public int Humidity { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }

        [ForeignKey("Forecast")]
        int IdForecast { get; set; }
    }
}
