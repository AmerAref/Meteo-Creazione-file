using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services.OpenWeatherMap.Models
{
    public class OneDayForecast
    {
        [JsonProperty("main")]
        public ForecastParameter Parameters { get; set; }

        [JsonProperty("weather")]
        public List<Forecast> Weather { get; set; }

        [JsonProperty("dt_txt")]
        public string TimeStamp { get; set; }

        [JsonProperty("id")]

        public int Id { get; set; }
    }
}