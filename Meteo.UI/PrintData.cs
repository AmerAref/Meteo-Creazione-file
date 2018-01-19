using System;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.UI
{
    public class PrintData
    {
        public void PrintForData(Meteo.Services.OpenWeatherMap.Models.OneDayForecast jsonObj, string menuLang)

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

        public void PrintDataLast5Day(Meteo.Services.OpenWeatherMap.Models.LastFiveDaysForecast jsonObj, string menuLang)
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
        public void PrintFilteredDataHumidity(Meteo.Services.OpenWeatherMap.Models.LastFiveDaysForecast objFiltred, string menuLang)
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

        public void PrintAllUsers(List<User> allUsers)
        {
            foreach(var user in allUsers)
            {
                Console.WriteLine("IdUser");
                Console.WriteLine(user.IdUser);
                Console.WriteLine("Name");
                Console.WriteLine(user.Name);
                Console.WriteLine("Surname");
                Console.WriteLine(user.Surname);
                Console.WriteLine("Password");
                Console.WriteLine(user.Password);
                Console.WriteLine("Username");
                Console.WriteLine(user.Username);
                Console.WriteLine("Language");
                Console.WriteLine(user.Language);
                Console.WriteLine("UnitOfMeasure");
                Console.WriteLine(user.UnitOfMeasure);
                Console.WriteLine("IdQuestion");
                Console.WriteLine(user.IdQuestion);
                Console.WriteLine("Answer");
                Console.WriteLine(user.Answer);
                Console.WriteLine("IdRole");
                Console.WriteLine(user.IdRole);
                Console.WriteLine("");
            }
        }

        public void PrintAllMasterRecords (List<Master> allMasterRecords)
        {
            foreach(var record in allMasterRecords)
            {
                Console.WriteLine("IdMaster");
                Console.WriteLine(record.IdMaster);
                Console.WriteLine("Choice5DayOrNow");
                Console.WriteLine(record.Choice5DayOrNow);
                Console.WriteLine("DateOfRequist");
                Console.WriteLine(record.DateOfRequist);
                Console.WriteLine("IdUser");
                Console.WriteLine(record.IdUser);
                Console.WriteLine("");
            }
        }
    }
}