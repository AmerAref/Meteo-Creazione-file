using System;
using System.Collections.Generic;
using Meteo.Services.CityJsonModels;
using Newtonsoft.Json;

namespace Meteo.Services.CityJsonModels
{
    public class CityJson
    {
        [JsonProperty("id")]
        public string IdCity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }



        [JsonProperty("coord")]
        public Coordinates Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }



    }

}




