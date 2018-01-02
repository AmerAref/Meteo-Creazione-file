using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services
{
    public class MeasuresParam
    {
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        [JsonProperty("temp")]
        public float Temp { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("temp_min")]
        public float TempMin { get; set; } // 
        [JsonProperty("temp_max")]
        public float TempMax { get; set; } // 
    }
}