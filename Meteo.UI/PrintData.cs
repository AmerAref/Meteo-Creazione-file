using System;
using Meteo.Services.OpenWeatherMap.Models;

namespace Meteo.UI
{
    public class PrintData
    {
        public void PrintForData(OneDayForecast jsonObj)

        {
            Console.WriteLine(jsonObj.Parameters.Pressure);
            Console.WriteLine("Temperature");
            Console.WriteLine(jsonObj.Parameters.Temp);
            Console.WriteLine("Humidity");
            Console.WriteLine(jsonObj.Parameters.Humidity);
            Console.WriteLine("Temperature min");
            Console.WriteLine(jsonObj.Parameters.TempMin);
            Console.WriteLine("Temperature max");
            Console.WriteLine(jsonObj.Parameters.TempMax);
        }

        public void PrintDataLast5Day(LastFiveDaysForecast jsonObj)
        {
            foreach (var measure in jsonObj.List)
            {
                Console.WriteLine("Pressure");
                Console.WriteLine(measure.Parameters.Pressure);
                Console.WriteLine("Temperature");
                Console.WriteLine(measure.Parameters.Temp);
                Console.WriteLine("Humidity");
                Console.WriteLine(measure.Parameters.Humidity);
                Console.WriteLine("Temperature min");
                Console.WriteLine(measure.Parameters.TempMin);
                Console.WriteLine("Temperature max");
                Console.WriteLine(measure.Parameters.TempMax);
            }
        }
    }
}
