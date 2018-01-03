using Newtonsoft.Json;
namespace Meteo.Services
{
    public class Weathers
    {
        [JsonProperty("main")]

        public string Main { get; set; }
        [JsonProperty("id")]

        public string Id { get; set; }

    }
}

