using System;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.UI
{
    public interface IPrintingService
    {
        void PrintForData(Meteo.Services.OpenWeatherMap.Models.OneDayForecast jsonObj, string menuLang, MeasureControl triggerMeasures);
        void PrintDataFor5Days(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast jsonObj, string menuLang, MeasureControl triggerMeasures);
        void PrintAllUsers(List<User> allUsers);
        void PrintAllMasterRecords(List<Master> allMasterRecords);
        void PrintDataFiltred(List<Forecast> DataFiltred);

    }
    public class PrintData : IPrintingService
    {
        static DateTime masterDate = DateTime.Now;
        static string format = "yyyy-MM-dd hh:mm:ss";
        static string str = masterDate.ToString(format);
        public void PrintForData(Meteo.Services.OpenWeatherMap.Models.OneDayForecast jsonObj, string menuLang, MeasureControl triggerMeasures)
        {
            if (menuLang == "it")
            {
                var minTempTriggerCelsius = triggerMeasures.MinTemperatureCelsius;
                var maxTempTriggerCelsius = triggerMeasures.MaxTemperatureCelsius;

                Console.WriteLine("Pressione: " + jsonObj.Parameters.Pressure);
                Console.WriteLine("Umidità: " + jsonObj.Parameters.Humidity);
                if (jsonObj.Parameters.Temp < minTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.Temp > maxTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura: " + jsonObj.Parameters.Temp);
                if (jsonObj.Parameters.TempMin < minTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.TempMin > maxTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura minima: " + jsonObj.Parameters.TempMin);
                if (jsonObj.Parameters.TempMax < minTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.TempMax > maxTempTriggerCelsius)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura massima: " + jsonObj.Parameters.TempMax);
                Console.ResetColor();
                Console.WriteLine("Data e ora della stampa: " + str);
                Console.WriteLine("");
            }
            else
            {
                var minTempTriggerFahrenheit = triggerMeasures.MinTemperatureFahrenheit;
                var maxTempTriggerFahrenheit = triggerMeasures.MaxTemperatureFahrenheit;

                Console.WriteLine("Pressure: " + jsonObj.Parameters.Pressure);
                Console.WriteLine("Humidity: " + jsonObj.Parameters.Humidity);
                if (jsonObj.Parameters.Temp < minTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.Temp > maxTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura: " + jsonObj.Parameters.Temp);
                if (jsonObj.Parameters.TempMin < minTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.TempMin > maxTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura minima: " + jsonObj.Parameters.TempMin);
                if (jsonObj.Parameters.TempMax < minTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (jsonObj.Parameters.TempMax > maxTempTriggerFahrenheit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.WriteLine("Temperatura massima: " + jsonObj.Parameters.TempMax);
                Console.ResetColor();
                Console.WriteLine("Date and time of the print: " + str);
                Console.WriteLine("");
            }
        }

        public void PrintDataFor5Days(Meteo.Services.OpenWeatherMap.Models.FiveDaysForecast jsonObj, string menuLang, MeasureControl triggerMeasures)
        {
            foreach (var measure in jsonObj.List)
            {
                if (menuLang == "it")
                {
                    var minTempTriggerCelsius = triggerMeasures.MinTemperatureCelsius;
                    var maxTempTriggerCelsius = triggerMeasures.MaxTemperatureCelsius;

                    Console.WriteLine("Pressione: " + measure.Parameters.Pressure);
                    Console.WriteLine("Umidità: " + measure.Parameters.Humidity);
                    if (measure.Parameters.Temp < minTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.Temp > maxTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura: " + measure.Parameters.Temp);
                    if (measure.Parameters.TempMin < minTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.TempMin > maxTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura minima: " + measure.Parameters.TempMin);
                    if (measure.Parameters.TempMax < minTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.TempMax > maxTempTriggerCelsius)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura massima: " + measure.Parameters.TempMax);
                    Console.ResetColor();

                    Console.WriteLine("Data e ora della previsione: " + measure.TimeStamp);
                    Console.WriteLine("");
                }
                else
                {
                    var minTempTriggerFahrenheit = triggerMeasures.MinTemperatureFahrenheit;
                    var maxTempTriggerFahrenheit = triggerMeasures.MaxTemperatureFahrenheit;

                    Console.WriteLine("Pressure: " + measure.Parameters.Pressure);
                    Console.WriteLine("Humidity: " + measure.Parameters.Humidity);
                    if (measure.Parameters.Temp < minTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.Temp > maxTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura: " + measure.Parameters.Temp);
                    if (measure.Parameters.TempMin < minTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.TempMin > maxTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura minima: " + measure.Parameters.TempMin);
                    if (measure.Parameters.TempMax < minTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (measure.Parameters.TempMax > maxTempTriggerFahrenheit)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.WriteLine("Temperatura massima: " + measure.Parameters.TempMax);
                    Console.ResetColor();
                    Console.WriteLine("Date and time of the forecast: " + measure.TimeStamp);
                    Console.WriteLine("");
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