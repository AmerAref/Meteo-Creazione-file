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
using Meteo.UI.AuthenticationUser;

namespace Meteo.UI
{
    public static class Program
    {
        public static Dictionary<string, string> InsertDataForEmail(string lang)
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
            var meteoApi = new MeteoApi();
            var choiceSelect = "";
            string userRole = "";
            var user = new User();
            var username = "";
            var queryBuilder = QueryBuilderServices.QueryBuilder();

            var lang = "";

            var menu = new Menu(queryBuilder);
            //scleta prima lingua nel menu
            menu.SelectLanguageStart();
            lang = Console.ReadLine();
            var loginOrRegistration = new LoginOrRegistration(lang);

            menu.ChangeLangages(lang);
            //menu login o registrazione 
            menu.ShowMenuAuthentication();
            var choseCreateNewAccuoutOrLogin = Console.ReadLine();

            // scelta effettuata 



            switch (choseCreateNewAccuoutOrLogin)
            {
                case "1":
                    // ritonro l'username utente loggato  
                    username = loginOrRegistration.Login();
                    break;
                case "2":

                    // ritonro l'username utente registrato  
                    username = loginOrRegistration.RegistrationNewAccount();
                    break;
                case "3":


                    return;
            }
            user = queryBuilder.GetUser(username);
            menuLang = user.Language;
            measureUnit = user.UnitOfMeasure;
            userRole = user.IdRole.ToString();


            if (userRole == "1")
            {
                try
                {
                    menu.ShowFirtsMenuAdmin();
                    var admin = new AuthenticatedAdmin(menuLang);
                    choiceSelect = Console.ReadLine();
                    admin.LoginAdmin(choiceSelect);
                    if (choiceSelect == "3")
                    {
                        menu.ShowSecondMenuAdmin();
                        var secondAdminChoice = Console.ReadLine();
                        admin.ModifyUserTable(secondAdminChoice);



                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {

                menu.ShowFirst();
                choiceSelect = Console.ReadLine();
                var authenticationUserOrSecondMenuAdmin = new AuthenticatedUser();

                authenticationUserOrSecondMenuAdmin.ForecastActions(username, choiceSelect, menuLang, measureUnit);
                if (choiceSelect == "1")
                {
                    menu.ShowSecondMenu();


                }



            }
        }
    }
}


