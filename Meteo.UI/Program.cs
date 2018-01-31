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
            var loginOrRegistration = new LoginOrRegistration(lang, queryBuilder);

            menu.ChangeLangages(lang);
            //menu login o registrazione 
            menu.ShowMenuAuthentication();
            var choseCreateNewAccuoutOrLogin = Console.ReadLine();




            switch (choseCreateNewAccuoutOrLogin)
            {
                case "1":
                    username = loginOrRegistration.Login();
                    break;
                case "2":

                    username = loginOrRegistration.RegistrationNewAccount();
                    break;
                case "3":


                    return;
            }
            user = queryBuilder.GetUser(username);
            menuLang = user.Language;
            measureUnit = user.UnitOfMeasure;
            userRole = user.IdRole.ToString();
            var exit = true;
            while (exit)
            {
                if (userRole == "1")
                {
                    try
                    {
                        menu.ShowFirtsMenuAdmin();
                        var admin = new AuthenticatedAdmin(menuLang, queryBuilder);
                        choiceSelect = Console.ReadLine();
                        admin.LoginAdmin(choiceSelect);

                     
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }




                {
                    menu.ShowFirst();
                    choiceSelect = Console.ReadLine();
                    var authenticationUserOrSecondMenuAdmin = new AuthenticatedUser();

                    authenticationUserOrSecondMenuAdmin.ForecastActions(username, choiceSelect, menuLang, measureUnit);
                }


            }

        }
    }
}


