using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meteo.Services.CityJsonModels
{
    public class Cities
    {
        public List<CitiesJson> List { get; set; }
    }
}