using System;
using Meteo.Services.OpenWeatherMap.Models;

namespace Meteo.UI
{
    public class PrintData
    {
        public void PrintForData(OneDayForecast jsonObj, string menuLang)

        {
            if (menuLang == "it")
            {
                Console.WriteLine("Pressione");
                Console.WriteLine(jsonObj.Parameters.Pressure);
                Console.WriteLine("Temperatura");
                Console.WriteLine(jsonObj.Parameters.Temp);
                Console.WriteLine("Umidità");
                Console.WriteLine(jsonObj.Parameters.Humidity);
                Console.WriteLine("Temperatura minima");
                Console.WriteLine(jsonObj.Parameters.TempMin);
                Console.WriteLine("Temperatura massima");
                Console.WriteLine(jsonObj.Parameters.TempMax);
            }
            else
            {
                Console.WriteLine("Pressure");
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

        }

        public void PrintDataLast5Day(LastFiveDaysForecast jsonObj, string menuLang)
        {
            foreach (var measure in jsonObj.List)
            {
                if (menuLang == "it")
                {
                    Console.WriteLine("Pressione");
                    Console.WriteLine(measure.Parameters.Pressure);
                    Console.WriteLine("Temperatura");
                    Console.WriteLine(measure.Parameters.Temp);
                    Console.WriteLine("Umidità");
                    Console.WriteLine(measure.Parameters.Humidity);
                    Console.WriteLine("Temperatura minima");
                    Console.WriteLine(measure.Parameters.TempMin);
                    Console.WriteLine("Temperatura massima");
                    Console.WriteLine(measure.Parameters.TempMax);
                }
                else
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

        public void PrintFilteredDataHumidity(LastFiveDaysForecast objFiltred, string menuLang)
        {
            foreach (var measure in objFiltred.List)
            {
                if (menuLang == "it")
                {
                    Console.WriteLine("Ora e data");
                    Console.WriteLine(measure.TimeStamp);
                }
                else
                {
                    Console.WriteLine("TimeStamp");
                    Console.WriteLine(measure.TimeStamp);
                }



                foreach (var weather in measure.Weather)
                {
                    if (menuLang == "it")
                    {
                        Console.Write("Cielo");
                        Console.WriteLine(weather.Main);

                    }
                    else
                    {
                        Console.Write("Sky");
                        Console.WriteLine(weather.Main);
                    }



                }
            }
        }


    }
}
