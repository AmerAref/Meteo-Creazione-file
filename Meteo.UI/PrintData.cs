using System;
using Meteo.Services;

namespace Meteo.UI
{
    public class PrintData
    {
        public void PrintForData(MeasureToday jsonObj)

        {
            Console.WriteLine(jsonObj.Main.Pressure);
            Console.WriteLine("Temperature");
            Console.WriteLine(jsonObj.Main.Temp);
            Console.WriteLine("Humidity");
            Console.WriteLine(jsonObj.Main.Humidity);
            Console.WriteLine("Temperature min");
            Console.WriteLine(jsonObj.Main.TempMin);
            Console.WriteLine("Temperature max");
            Console.WriteLine(jsonObj.Main.TempMax);

        }

        public void PrintDataLast5Day(ListMeasureLast5Day jsonObj)
        {
            foreach (var measure in jsonObj.List)
            {
                Console.WriteLine("Pressure");
                Console.WriteLine(measure.Main.Pressure);
                Console.WriteLine("Temperature");
                Console.WriteLine(measure.Main.Temp);
                Console.WriteLine("Humidity");
                Console.WriteLine(measure.Main.Humidity);
                Console.WriteLine("Temperature min");
                Console.WriteLine(measure.Main.TempMin);
                Console.WriteLine("Temperature max");
                Console.WriteLine(measure.Main.TempMax);

            }
        }
    }
}
