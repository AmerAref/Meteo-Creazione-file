using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Meteo.Services.Models
{
    public class Forecast
    {
        

        public DateTime WeatherDate { get; set; }


        public DateTime DateOfRequist { get; set; }


        public double Value { get; set;  }

        public string UnitOfMeasure { get; set; }

    }
}