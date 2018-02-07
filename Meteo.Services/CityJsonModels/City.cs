using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services.CityJsonModels
{
    public class City
    {
        public List<CityJson> List { get; set; }
    }
}