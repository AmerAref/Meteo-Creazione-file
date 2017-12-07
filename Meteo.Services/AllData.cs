using System;
using System.Collections.Generic;
 
namespace Meteo.Services
{
    public class Repo
    {
        public List<Measure>  list { get; set; }
    }

   public class Measure{
       public ParamMeasures main { get; set; }
      
   }
    public class ParamMeasures
    {
        public float pressure { get; set; }
        public float temp { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }
}