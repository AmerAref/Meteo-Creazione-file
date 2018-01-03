using Newtonsoft.Json;

namespace Meteo.Services
{
    public class MeasureToday
    {
        [JsonProperty("main")]
        public MeasuresParam Main { get; set; }
    }
}