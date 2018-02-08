using System;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.UI
{
    public interface IPrintingService
    {
        void PrintForData(Meteo.Services.OpenWeatherMap.Models.OneDayForecast jsonObj, string menuLang);
        void PrintDataFor5Days(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast jsonObj, string menuLang);
        void PrintFilteredDataHumidity(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast objFiltred, string menuLang);
        void PrintAllUsers(List<User> allUsers);
        void PrintAllMasterRecords(List<Master> allMasterRecords);
        void PrintDataFiltred(List<Forecast> DataFiltred);

    }
    public class PrintData : IPrintingService
    {
        static DateTime masterDate = DateTime.Now;
        static string format = "yyyy-MM-dd hh:mm:ss";
        static string str = masterDate.ToString(format);
        public void PrintForData(Meteo.Services.OpenWeatherMap.Models.OneDayForecast jsonObj, string menuLang)
        {
            if (menuLang == "it")
                {
                    Console.WriteLine("Pressione: " + jsonObj.Parameters.Pressure);
                    Console.WriteLine("Temperatura: " + jsonObj.Parameters.Temp);
                    Console.WriteLine("Umidità: " + jsonObj.Parameters.Humidity);
                    Console.WriteLine("Temperatura minima: " + jsonObj.Parameters.TempMin);
                    Console.WriteLine("Temperatura massima: " + jsonObj.Parameters.TempMax);
                    Console.WriteLine("Data e ora della stampa: " + str);
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Pressure: " + jsonObj.Parameters.Pressure);
                    Console.WriteLine("Temperature: " + jsonObj.Parameters.Temp);
                    Console.WriteLine("Humidity: " + jsonObj.Parameters.Humidity);
                    Console.WriteLine("Temperature min: " + jsonObj.Parameters.TempMin);
                    Console.WriteLine("Temperature max: " + jsonObj.Parameters.TempMax);
                    Console.WriteLine("Date and time of the print: " + str);
                    Console.WriteLine("");
                }
        }

        public void PrintDataFor5Days(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast jsonObj, string menuLang)
        {
            foreach (var measure in jsonObj.List)
            {
                if (menuLang == "it")
                {
                    Console.WriteLine("Pressione: " + measure.Parameters.Pressure);
                    Console.WriteLine("Temperatura: " + measure.Parameters.Temp);
                    Console.WriteLine("Umidità: " + measure.Parameters.Humidity);
                    Console.WriteLine("Temperatura minima: " + measure.Parameters.TempMin);
                    Console.WriteLine("Temperatura massima: " + measure.Parameters.TempMax);
                    Console.WriteLine("Data e ora della previsione: " + measure.TimeStamp);
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Pressure: " + measure.Parameters.Pressure);
                    Console.WriteLine("Temperature: " + measure.Parameters.Temp);
                    Console.WriteLine("Humidity: " + measure.Parameters.Humidity);
                    Console.WriteLine("Temperature min: " + measure.Parameters.TempMin);
                    Console.WriteLine("Temperature max: " + measure.Parameters.TempMax);
                    Console.WriteLine("Date and time of the forecast: " + measure.TimeStamp);
                    Console.WriteLine("");
                }
            }
        }

        public void PrintFilteredDataHumidity(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast objFiltred, string menuLang)
        {
            foreach (var measure in objFiltred.List)
            {
                if (menuLang == "it")
                {
                    Console.WriteLine("Ora e data: " + measure.TimeStamp);
                    // Console.WriteLine(measure.TimeStamp);
                }
                else
                {
                    Console.WriteLine("Date and time: " + measure.TimeStamp);
                    // Console.WriteLine(measure.TimeStamp);
                }

                foreach (var weather in measure.Weather)
                {
                    if (menuLang == "it")
                    {
                        Console.Write("Cielo: " + weather.Main);
                        Console.WriteLine("");
                        // Console.WriteLine(weather.Main);
                    }
                    else
                    {
                        Console.Write("Sky: " + weather.Main);
                        Console.WriteLine("");
                        // Console.WriteLine(weather.Main);
                    }
                }
            }
        }

        public void PrintAllUsers(List<User> allUsers)
        {
            foreach (var user in allUsers)
            {
                Console.WriteLine("IdUser: " + user.IdUser);
                Console.WriteLine("Name: " + user.Name);
                Console.WriteLine("Surname: " + user.Surname);
                Console.WriteLine("Password: " + user.Password);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("Language: " + user.Language);
                Console.WriteLine("Unit Of Measure: " + user.UnitOfMeasure);
                Console.WriteLine("Id Question: " + user.IdQuestion);
                Console.WriteLine("Answer: " + user.Answer);
                Console.WriteLine("Id Role: " + user.IdRole);
                Console.WriteLine("");
            }
        }

        public void PrintAllMasterRecords(List<Master> allMasterRecords)
        {
            foreach (var record in allMasterRecords)
            {
                Console.WriteLine("Id Master: " + record.IdMaster);
                Console.WriteLine("Choice 5 DayOrNow: " + record.Choice5DayOrNow);
                Console.WriteLine("Date Of Requist: " + record.DateOfRequist);
                Console.WriteLine("Id User: " + record.IdUser);
                Console.WriteLine("Id City: " + record.IdCity);
                Console.WriteLine("");
            }
        }
        public void PrintDataFiltred(List<Forecast> DataFiltred)
        {
            foreach (var record in DataFiltred)
            {
                Console.WriteLine("IdMaster: " + record.IdMaster);
                Console.WriteLine("Temperature: " + record.Temperature);
                Console.WriteLine("Weather Date: " + record.WeatherDate);
                Console.WriteLine("City Name: " + record.CityName);
                Console.WriteLine("IdCity: " + record.IdCity);
                Console.WriteLine("");
            }
        }


    }
}