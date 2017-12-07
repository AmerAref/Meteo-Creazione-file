using System;
using System.Collections.Generic;
namespace Meteo.Services
{
    public class DataFiltredTime
    {
        public List<Dati> list { get; set; }
    }
    public class Dati
    {
        public List<Wheaters> weather { get; set; }
        public string dt_txt { get; set; }
    }
    public class Wheaters
    {
        public string main { get; set; }
        public string id { get; set; }

    }
}