using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services
{
    public class Measure
    {
        [JsonProperty("main")]
   
        public ParamMeasures Main { get; set; }

    }
}
