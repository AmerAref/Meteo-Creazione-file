using System.ComponentModel.DataAnnotations;

namespace Meteo.Services.Models
{
    public class MeasureTrigger
    {
        [Key]
        public int IdTrigger { get; set; }

        public double MinLatitude { get; set; }

        public double MaxLongitude { get; set; }

        public double MinTemperatureCelsius { get; set; }

        public double MaxTemperatureCelsius { get; set; }

        public double MinTemperatureFahrenheit { get; set; }

        public double MaxTemperatureFahrenheit { get; set; }
    }
}