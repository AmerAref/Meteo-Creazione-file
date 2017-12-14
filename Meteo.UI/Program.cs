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
            var emailManager = new EmailManager();
            var pswManager = new PswManager();
            var filemenager = new FileMenager();
            var menu = new Menu();
            var print = new PrintData();
            var exit = true;
            var insertNameFile = "Inserisci nome file da creare con tipo di estensione (nomefile.estensione)";
            var meteoApi = new MeteoAPI();
            var success = "Richiesta elaborata con successo";
            var choiceDoFile = "Vuoi creare il file con i dati precedentemente rischiesti? (S/n)";
            var choiceSendEmail = "Vuoi inviare tramite email il file appena creato?(S/n)";
            var insertPassword = "Inserisci password email mittente ";
            var insertNamePlace = "Inserisci località richiesta";
            var insertLon = "Insersci longitudine";
            var insertLat = "Inserisci latitudine";
            var insertSender = "inserisci email mittente";
            var insertReciver = "Inserisci email destinatario";
            var insertBody = "Iserisci Testo all'interno dell'email";
            var insertSubject = "Inserisci oggetto";
            var successCreateFile = "File creato con successo";
            var nameFileDelete = "Inserisci nome file da eliminare, con tipo di estensione (nomefile.estensione)";
            var password = "";

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
                                Console.WriteLine(insertNamePlace);
                                var place = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place).Result;
                                    print.PrintForData(jsonObj);
                                    Console.WriteLine(success);
                                    Console.WriteLine(choiceDoFile);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFile);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmail);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(successCreateFile);

                                        if (choiceSelected == "S")
                                        {
                                            Console.WriteLine(insertSender);
                                            var sender = Console.ReadLine();
                                            Console.WriteLine(insertReciver);
                                            var receiver = Console.ReadLine();
                                            Console.WriteLine(insertBody);
                                            var body = Console.ReadLine();
                                            Console.WriteLine(insertSubject);
                                            var subject = Console.ReadLine();
                                            var user = sender.Split('@')[0];
                                            Console.WriteLine("Inserisci password");
                                            emailManager.AttempsPasswordAndSendEmail(fileName, sender, receiver, body, subject, user, password);
                                        }
                                        else
                                        {
                                            Console.WriteLine(success);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(success);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                Console.WriteLine(insertLat);
                                var lat = Console.ReadLine();
                                Console.WriteLine(insertLon);
                                var lon = Console.ReadLine();
                                try
                                {

                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat).Result;
                                    print.PrintForData(jsonObj);
                                    Console.WriteLine(success);
                                    Console.WriteLine(choiceDoFile);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFile);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(successCreateFile);
                                        Console.WriteLine(choiceSendEmail);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            Console.WriteLine(insertSender);
                                            var sender = Console.ReadLine();
                                            Console.WriteLine(insertReciver);
                                            var receiver = Console.ReadLine();

                                            Console.WriteLine(insertBody);
                                            var body = Console.ReadLine();
                                            Console.WriteLine(insertSubject);
                                            var subject = Console.ReadLine();
                                            var user = sender.Split('@')[0];
                                            Console.WriteLine(insertPassword);
                                            emailManager.AttempsPasswordAndSendEmail(fileName, sender, receiver, body, subject, user, password);
                                        }
                                        else
                                        {
                                            Console.WriteLine(success);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(success);
                                    }
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
                                Console.WriteLine(insertNamePlace);
                                var place = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    Console.WriteLine(success);
                                    Console.WriteLine(choiceDoFile);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFile);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmail);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(successCreateFile);

                                        if (choiceSelected == "S")
                                        {
                                            Console.WriteLine(insertSender);
                                            var sender = Console.ReadLine();
                                            Console.WriteLine(insertReciver);
                                            var receiver = Console.ReadLine();

                                            Console.WriteLine(insertBody);
                                            var body = Console.ReadLine();
                                            Console.WriteLine(insertSubject);
                                            var subject = Console.ReadLine();
                                            var user = sender.Split('@')[0];
                                            Console.WriteLine(insertPassword);
                                            emailManager.AttempsPasswordAndSendEmail(fileName, sender, receiver, body, subject, user, password);
                                        }
                                        else
                                        {
                                            Console.WriteLine(success);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(success);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                Console.WriteLine(insertLat);
                                var lat = Console.ReadLine();
                                Console.WriteLine(insertLon);
                                var lon = Console.ReadLine();
                                try
                                {
                                    Console.WriteLine(insertNameFile);
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    Console.WriteLine(success);
                                    Console.WriteLine(choiceDoFile);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFile);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmail);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            Console.WriteLine(insertSender);
                                            var sender = Console.ReadLine();
                                            Console.WriteLine(insertReciver);
                                            var receiver = Console.ReadLine();
                                            Console.WriteLine(insertBody);
                                            var body = Console.ReadLine();
                                            Console.WriteLine(insertSubject);
                                            var subject = Console.ReadLine();
                                            var user = sender.Split('@')[0];
                                            Console.WriteLine(insertPassword);
                                            emailManager.AttempsPasswordAndSendEmail(fileName, sender, receiver, body, subject, user, password);
                                        }
                                        else
                                        {
                                            Console.WriteLine(success);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(success);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "3":
                                break;
                            case "4":
                                return;
                        }
                        break;
                    case "3":

                        menu.ShowFiltredMenu();
                        var choseFilter = Console.ReadLine();
                        switch (choseFilter)
                        {
                            case "1":
                                menu.InsertNamePlace();
                                var place = Console.ReadLine();
                                Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni");
                                var humidity = Console.ReadLine();
                                try
                                {
                                    meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Wait();
                                    Console.WriteLine(success);
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                menu.InsertNamePlace();
                                place = Console.ReadLine();
                                Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
                                var date = Console.ReadLine();
                                Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
                                var time = Console.ReadLine();
                                meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Wait();
                                Console.WriteLine(success);
                                break;
                            case "3":
                                menu.InsertNamePlace();
                                place = Console.ReadLine();
                                Console.WriteLine("Iserisci tipologia di tempo richiesta");
                                var typeWeather = Console.ReadLine();
                                meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place).Wait();
                                Console.WriteLine(success);
                                break;
                            case "4":
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine(nameFileDelete);
                        var fileNameDelete = Console.ReadLine();
                        filemenager.DeleteFile(fileNameDelete);

                        break;

                    case "5":
                        Console.WriteLine("Inserisci nome file da inviare tramite email");
                        var fileNameToSend = Console.ReadLine();
                        Console.WriteLine(insertSender);
                        var senderAnyFile = Console.ReadLine();
                        Console.WriteLine(insertReciver);
                        var receiverAnyFile = Console.ReadLine();
                        Console.WriteLine(insertBody);
                        var bodyAnyFile = Console.ReadLine();
                        Console.WriteLine(insertSubject);
                        var subjectAnyFile = Console.ReadLine();
                        var userAnyFile = senderAnyFile.Split('@')[0];
                        Console.WriteLine(insertPassword);
                        emailManager.AttempsPasswordAndSendEmail(fileNameToSend, senderAnyFile, receiverAnyFile, bodyAnyFile, subjectAnyFile, userAnyFile, password);


                        break;
                    case "6":
                        exit = false;
                        Console.WriteLine("Sessione terminata");
                        break;

                }
            }

        }
    }
}



