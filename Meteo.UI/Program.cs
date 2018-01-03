using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Meteo.Services;
using Meteo.ExcelManager;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace Meteo.UI
{
    public static class Program
    {
        private static Dictionary<string, string> InsertDataForEmail()
        {
            var dictionaryForEmail = new Dictionary<string, string>();
            var insertSender = "inserisci email mittente";
            var insertReciver = "Inserisci email destinatario";
            var insertBody = "Iserisci Testo all'interno dell'email";
            var insertSubject = "Inserisci oggetto";
            Console.WriteLine(insertSender);
            var sender = Console.ReadLine();
            dictionaryForEmail.Add("senderKey", sender);

            Console.WriteLine();

            Console.WriteLine(insertReciver);
            var receiver = Console.ReadLine();
            Console.WriteLine(insertBody);
            var body = Console.ReadLine();
            Console.WriteLine(insertSubject);
            var subject = Console.ReadLine();

            var user = sender.Split('@')[0];

            dictionaryForEmail.Add("receiverKey", receiver);
            dictionaryForEmail.Add("bodyKey", body);
            dictionaryForEmail.Add("subjectKey", subject);
            dictionaryForEmail.Add("userKey", user);
            Console.WriteLine("Inserisci password");

            return dictionaryForEmail;
        }
        static void Main(string[] args)
        {
            var login = new Login();
            var registration = new Registration();
            var emailManager = new EmailManager();
            var pswManager = new PswManager();
            var filemenager = new FileMenager();
            var createXlsFile = new CreateXlsFile();
            var menu = new Menu();
            var print = new PrintData();
            var exit = true;
            var insertNameFile = "Inserisci nome file da creare con tipo di estensione (nomefile.estensione)";
            var meteoApi = new MeteoAPI();
            var success = "Richiesta elaborata con successo";
            var choiceDoFile = "Vuoi creare il file con i dati precedentemente rischiesti? (S/n)";
            var choiceSendEmail = "Vuoi inviare tramite email il file appena creato?(S/n)";
            var insertNamePlace = "Inserisci località richiesta";
            var insertMeasureUnit = "Inserisci l'unità di misura Celsius (metric) o Fahrenheit (imperial)";
            var insertLon = "Insersci longitudine";
            var insertLat = "Inserisci latitudine";
            var successCreateFile = "File creato con successo";
            var nameFileDelete = "Inserisci nome file da eliminare, con tipo di estensione (nomefile.estensione)";
            var choiceCreateXlsFile = "Vuoi creare un file XLS con i dati precedenti?(S/n)";
            var password = "";
            var insertUser = "Inserisci Username";
            var insertPsw = "Inserisci Password";
            var insertName = "Inserisci Nome";
            var insertSurname = "Insersci surname";
            var i = 0;
            var c = 2;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.Services/Connection.To.Database/DatabaseConnection.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("SampleConnection");

            var context = UsersContextFactory.Create(connectionString);

            context.SaveChanges();

            var attempts = true;
            while (attempts)
            {
                menu.ShowMenuAuthentication();

                var choseCreateNewAccuout = Console.ReadLine();

                if (choseCreateNewAccuout == "1")
                {
                    while (i < 3)
                    {
                        Console.WriteLine(insertUser);
                        var usernameAuthentication = Console.ReadLine();
                        Console.WriteLine(insertPsw);
                        var passwordAuthentication = Console.ReadLine();

                        var encryptedPwd = context.Users.SingleOrDefault(x => x.Username.Equals(usernameAuthentication)).Password;
                        var authPwd = login.EncryptInsertedPwd(passwordAuthentication);

                        var autentication = login.LoginAttempts(context, usernameAuthentication, authPwd);

                        if (i >= 1 && i < 3)
                        {
                            Console.WriteLine("\nReinsersci Username e password");
                            Console.WriteLine($"Numero dei tenativi rimasti {c}");
                            c--;
                        }
                        if (autentication.Any())
                        {
                            Console.WriteLine($"Benvenuto {usernameAuthentication}");
                            attempts = false;
                            i = 3;
                        }
                        i++;
                    }
                }
                if (choseCreateNewAccuout == "2")
                {
                    while (i < 100)
                    {
                        Console.WriteLine(insertName);
                        var nameNewAccuont = Console.ReadLine();
                        Console.WriteLine(insertSurname);
                        var surnameNewAccount = Console.ReadLine();
                        Console.WriteLine(insertUser);
                        var usernameNewAccount = Console.ReadLine();
                        Console.WriteLine(insertPsw);
                        var pswNewAccount = Console.ReadLine();
                        Console.WriteLine("Inserisci ancora una volta la password");
                        var pswNewAccountComparison = Console.ReadLine();

                        if (pswNewAccount == pswNewAccountComparison)
                        {
                            var encryptedPwd = registration.EncryptPwd(pswNewAccount);

                            context.Users.Add(
                                new User
                                {
                                    Password = encryptedPwd,
                                    Username = usernameNewAccount,
                                    Surname = surnameNewAccount,
                                    Name = nameNewAccuont
                                }
                            );
                            i = 100;
                            attempts = false;
                        }
                    }
                }
            }
            context.SaveChanges();

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
                                Console.WriteLine(insertMeasureUnit);
                                var measureUnitToday = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnitToday).Result;
                                    print.PrintForData(jsonObj);
                                    Console.WriteLine(success);
                                    var prop = "Pressure";
                                    var Propr = jsonObj.Main.GetType().GetProperty(prop).GetValue(jsonObj.Main, null);
                                    Console.WriteLine(Propr);

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
                                            var dataForEmail = Program.InsertDataForEmail();
                                            var senderValue = dataForEmail["senderKey"];
                                            var receiverValue = dataForEmail["receiverKey"];
                                            var bodyValue = dataForEmail["bodyKey"];
                                            var subjectValue = dataForEmail["subjectKey"];
                                            var userValue = dataForEmail["userKey"];
                                            emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                        }
                                        Console.WriteLine(choiceCreateXlsFile);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForToday(fileName, jsonObj, place);
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
                                Console.WriteLine(insertMeasureUnit);
                                var measureUnitCoordinates = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnitCoordinates).Result;
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
                                            var dataForEmail = Program.InsertDataForEmail();
                                            var senderValue = dataForEmail["senderKey"];
                                            var receiverValue = dataForEmail["receiverKey"];
                                            var bodyValue = dataForEmail["bodyKey"];
                                            var subjectValue = dataForEmail["subjectKey"];
                                            var userValue = dataForEmail["userKey"];
                                            emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                        }
                                        Console.WriteLine(choiceCreateXlsFile);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForTodayByCoordinates(fileName, jsonObj, lat, lon);
                                        }
                                        Console.WriteLine(success);

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
                                Console.WriteLine(insertMeasureUnit);
                                var measureUnitFiveDays = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnitFiveDays).Result;
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
                                            var dataForEmail = Program.InsertDataForEmail();
                                            var senderValue = dataForEmail["senderKey"];
                                            var receiverValue = dataForEmail["receiverKey"];
                                            var bodyValue = dataForEmail["bodyKey"];
                                            var subjectValue = dataForEmail["subjectKey"];
                                            var userValue = dataForEmail["userKey"];
                                            emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                        }
                                        Console.WriteLine(choiceCreateXlsFile);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5Days(fileName, jsonObj, place);
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
                                Console.WriteLine(insertMeasureUnit);
                                var measureUnitCoordinatesFiveDays = Console.ReadLine();
                                try
                                {
                                    Console.WriteLine(insertNameFile);
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnitCoordinatesFiveDays).Result;
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
                                            var dataForEmail = Program.InsertDataForEmail();
                                            var senderValue = dataForEmail["senderKey"];
                                            var receiverValue = dataForEmail["receiverKey"];
                                            var bodyValue = dataForEmail["bodyKey"];
                                            var subjectValue = dataForEmail["subjectKey"];
                                            var userValue = dataForEmail["userKey"];
                                            emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                        }

                                        Console.WriteLine(choiceCreateXlsFile);
                                        choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5DaysByCoordinates(fileName, jsonObj, lat, lon);
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
                                Console.WriteLine(insertNamePlace);
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
                                Console.WriteLine(insertNamePlace);
                                place = Console.ReadLine();
                                Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
                                var date = Console.ReadLine();
                                Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
                                var time = Console.ReadLine();
                                meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Wait();
                                Console.WriteLine(success);
                                break;
                            case "3":
                                Console.WriteLine(insertNamePlace);
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
                        var fileNameToSendAnyFile = Console.ReadLine();
                        var dataForEmailAnyFile = Program.InsertDataForEmail();
                        var senderValueAnyFile = dataForEmailAnyFile["senderKey"];
                        var receiverValueAnyFile = dataForEmailAnyFile["receiverKey"];
                        var bodyValueAnyFile = dataForEmailAnyFile["bodyKey"];
                        var subjectValueAnyFile = dataForEmailAnyFile["subjectKey"];
                        var userValueAnyFile = dataForEmailAnyFile["userKey"];
                        emailManager.AttempsPasswordAndSendEmail(fileNameToSendAnyFile, senderValueAnyFile, receiverValueAnyFile, bodyValueAnyFile, subjectValueAnyFile, userValueAnyFile, password);
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