using Newtonsoft.Json;

namespace Meteo.Services.OpenWeatherMap.Models
{
    public class Forecast
    {
        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}