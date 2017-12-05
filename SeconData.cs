using System;
using System.Collections.Generic;
 
namespace Meteo
{
   

   public class MeasureForToday{
       public MeasuresParam main { get; set; }

   }
    public class MeasuresParam
    {
        public float pressure { get; set; }
        public float temp { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }
}