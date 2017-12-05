using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Meteo

{
    class Program
    {
        static void Main(string[] args)
        {

            var menu = new Menu();
            var exit = true;
            var config = new Config();
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
                                    config.ProcessMeteoByPlaceToday(place).Wait();

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
                                config.ProcessMeteoByCoordinatesToday(lat, lon).Wait();
                                try
                                {
                                    config.ProcessMeteoByCoordinatesToday(lat, lon).Wait();
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
                                    config.ProcessMeteoByPlaceLast5Day(place).Wait();
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
                                                config.FiltredMeteoByHumidityLast5Day(place, humidity).Wait();
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
                                            config.FiltredMeteoByDateTimeLast5Day(place, date, time).Wait();
                                            break;
                                        case "3":
                                            Console.WriteLine("Iserisci tipologia di tempo richiesta");
                                            var typeWeather = Console.ReadLine();
                                            config.FiltredMeteoByWeatherLast5Day(place, typeWeather).Wait();
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
                                    config.ProcessMeteoByCoordinatesLast5Day(lat, lon).Wait();
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
