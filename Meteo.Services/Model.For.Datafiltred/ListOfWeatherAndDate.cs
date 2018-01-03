using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services
{
    public class Data
    {
        [JsonProperty("weather")]
        public List<Weathers> Weather { get; set; }
        [JsonProperty("dt_txt")]
        public string Date { get; set; }
    }

}