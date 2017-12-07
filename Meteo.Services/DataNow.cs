using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services
{
    public class MeasureForToday
    {
        public MeasuresParam main { get; set; }
    }
    public class MeasuresParam
    {
        [JsonProperty("pressure")]
        public float pressure { get; set; }
        public float temp { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; } // TempMin
        public float temp_max { get; set; } // TempMax
    }
}