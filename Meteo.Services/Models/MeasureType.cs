using System.ComponentModel.DataAnnotations;

namespace Meteo.Services.Models
{
    class MeasureType
    {
        [Key]
        public int IdMeasureType { get; set; }

        public string MeasureName { get; set; }

        public string UnitOfMeasure { get; set; }

        public string Language { get; set; }
    }
}
