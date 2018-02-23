using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meteo.Services.Models
{
    public class MeasureValue
    {
        [Key]
        public int IdMeasureValue { get; set; }

        public double Value { get; set; }

        [ForeignKey("Forecast")]
        public int IdForecast { get; set; }

        [ForeignKey("MeasureType")]
        public int IdMeasureType { get; set; }
    }
}
