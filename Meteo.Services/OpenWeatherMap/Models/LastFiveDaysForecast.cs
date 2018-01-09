using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services.OpenWeatherMap.Models
{
    public class LastFiveDaysForecast
    {
        [JsonProperty("list")]
        public List<OneDayForecast> List { get; set; }
    }
}