using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Meteo.Services;

namespace Meteo.UI
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            var exit = true;
            while (exit)
            {
                menu.ShowFirst();
                var sceltaPrimaria = Console.ReadLine();
                switch (sceltaPrimaria)
                {
                    case "1":
                        menu.ShowMenu();
                        var choseThisDay = Console.ReadLine();
                        switch (choseThisDay)
                        {
                            case "1":
                                menu.InsertNamePlace();
                                var place = Console.ReadLine();
                                try
                                {
                                    var url = $"http://api.openweathermap.org/data/2.5/weather?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
                                    var MeteoAPI = new MeteoAPI(url);
                                    Console.WriteLine("Inserisci nome file da creare con tipo di estensione (nomefile.estensione)");
                                    var fileName = Console.ReadLine();
                                    MeteoAPI.ProcessMeteoByPlaceToday(url, fileName).Wait();
                                    Console.WriteLine("Richiesta elaborata con successo");
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                menu.InsertCoordinates();
                                var lat = Console.ReadLine();
                                var lon = Console.ReadLine();
                                try
                                {
                                    var url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=0dc9854b15fa5612e84597073b150cd3";
                                    var MeteoAPI = new MeteoAPI(url);
                                    Console.WriteLine("Inserisci nome file da creare con tipo di estensione (nomefile.estensione)");
                                    var fileName = Console.ReadLine();
                                    MeteoAPI.ProcessMeteoByCoordinatesToday(url, fileName).Wait();
                                    Console.WriteLine("Richiesta elaborata con successo");
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "3":

                                break;
                        }
                        break;
                    case "2":
                        menu.ShowMenu();
                        var choseLast5Day = Console.ReadLine();
                        switch (choseLast5Day)
                        {
                            case "1":
                                menu.InsertNamePlace();
                                var place = Console.ReadLine();
                                try
                                {
                                    var url = $"http://samples.openweathermap.org/data/2.5/forecast?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
                                    var MeteoAPI = new MeteoAPI(url);
                                    Console.WriteLine("Inserisci nome file da creare con tipo di estensione (nomefile.estensione)");
                                    var fileName = Console.ReadLine();
                                    MeteoAPI.ProcessMeteoByPlaceLast5Day(url, fileName).Wait();
                                    Console.WriteLine("Richiesta elaborata con successo");
                                    menu.ShowFiltredMenu();
                                    var choseFilter = Console.ReadLine();
                                    switch (choseFilter)
                                    {
                                        case "1":

                                            Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni");
                                            var humidity = Console.ReadLine();
                                            try
                                            {
                                                MeteoAPI.FiltredMeteoByHumidityLast5Day(url, humidity).Wait();
                                                Console.WriteLine("Richiesta elaborata con successo");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Errore");
                                            }
                                            break;
                                        case "2":
                                            Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
                                            var date = Console.ReadLine();
                                            Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
                                            var time = Console.ReadLine();
                                            MeteoAPI.FiltredMeteoByDateTimeLast5Day(url, date, time).Wait();
                                            break;
                                        case "3":
                                            Console.WriteLine("Iserisci tipologia di tempo richiesta");
                                            var typeWeather = Console.ReadLine();
                                            MeteoAPI.FiltredMeteoByWeatherLast5Day(url, typeWeather).Wait();
                                            break;
                                        case "4":
                                            break;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                menu.InsertCoordinates();
                                var lat = Console.ReadLine();
                                var lon = Console.ReadLine();
                                try
                                {
                                    var url = $"http://samples.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid=0dc9854b15fa5612e84597073b150cd3";
                                    var MeteoAPI = new MeteoAPI(url);
                                    Console.WriteLine("Inserisci nome file da creare con tipo di estensione (nomefile.estensione)");
                                    var fileName = Console.ReadLine();
                                    MeteoAPI.ProcessMeteoByCoordinatesLast5Day(url, fileName).Wait();
                                    Console.WriteLine("Richiesta elaborata con successo");
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "3":
                                break;
                            case "4":
                                Console.WriteLine("Sessione terminata");
                                break;
                        }
                        break;
                    case "3":
                        exit = false;
                        Console.WriteLine("Sessione terminata");
                        break;
                }
            }
        }
    }
}
