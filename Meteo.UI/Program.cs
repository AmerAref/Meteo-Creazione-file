using System;
using System.Collections.Generic;
using Meteo.Services;
using Meteo.ExcelManager;
using Meteo.Services.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Meteo.Services.Models;
using Ninject;
using Meteo.UI.AdminActions;

namespace Meteo.UI
{
    public static class Program
    {
        private static Dictionary<string, string> InsertDataForEmail(string lang)
        {
            string insertSender = "", insertReciver = "", insertBody = "", insertSubject = "";
            var dictionaryForEmail = new Dictionary<string, string>();
            if (lang == "it")
            {
                insertSender = "Inserisci email del mittente";
                insertReciver = "Inserisci email del destinatario";
                insertBody = "Iserisci il testo all'interno dell'email";
                insertSubject = "Inserisci l'oggetto";
            }
            else
            {
                insertSender = "Insert sender's email";
                insertReciver = "Insert recipient'semail";
                insertBody = "Isert the body text of the email";
                insertSubject = "Insert the object";
            }
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
            if (lang == "it")
            { Console.WriteLine("Inserisci password"); }
            else
            { Console.WriteLine("Insert password"); }

            return dictionaryForEmail;
        }

        static void Main(string[] args)
        {
            var menuLang = "";
            var measureUnit = "";
            var emailManager = new EmailManager();
            var filemenager = new FileMenager();
            var createXlsFile = new CreateXlsFile();
            var createXlsFromFile = new CreateXlsFromFiles();
            var print = new PrintData();
            var exit = true;
            var meteoApi = new MeteoApi();
            var password = "";
            var roleChoiceSelect = "";
            var meteoChoiceForDB = "";
            string userRole = "";
            var user = new User();
            string formatDateForFile = "yyyy-MM-dd";
            string str = "";
            var username = "";
            var queryBuilder = QueryBuilderServices.QueryBuilder();

            var lang = Console.ReadLine();
            var loginServices = new LoginOrRegistration(menuLang);

            var menu = new Menu(queryBuilder, lang, menuLang);
            var admin = new AuthenticatedAdmin();

            menu.SelectLanguageStart();
            lang = Console.ReadLine();


            try
            {
                username = loginServices.AuthenticationOrRegistration();
            }
            catch
            {
                Console.WriteLine("Inserimento errato");
            }

            // salvataggio modifiche su DB 

            while (exit)
            {
                user = queryBuilder.GetUser(username);
                menuLang = user.Language;
                measureUnit = user.UnitOfMeasure;
                userRole = user.IdRole.ToString();

                if (userRole == "1")
                {
                    try
                    {
                        admin.LoginAdmin();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {

                    menu.ShowFirst();



                }
            }
        }
    }
}

