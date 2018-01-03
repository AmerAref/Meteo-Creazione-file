using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services
{
    public class ListMeasureLast5Day
    {
        [JsonProperty("list")]

        public List<Measure> List { get; set; }
    }
}