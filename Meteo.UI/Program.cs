﻿using System.Reflection;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Meteo.Services.Models;
using Meteo.UI.AdminManager;
using Meteo.UI.FirstAuthentication;
using Meteo.UI.ForecastManager;
using Ninject;

namespace Meteo.UI
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var user = new User();
            var lang = "";
            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetAssembly(typeof(MySqlManager)));
            var queryBuilder = kernel.Get<IQueryBuilder>();
            var manager = new MySqlManager(queryBuilder);
            var menu = new Menu(queryBuilder, lang, new ExitService());

            lang = menu.SelectLanguageStart();

            menu = new Menu(queryBuilder, lang, new ExitService());
            var loginOrRegistration = new LoginOrRegistration(lang, queryBuilder, new ExitService());

            //menu login o registrazione 
            var choseCreateNewAccuoutOrLogin = menu.ShowMenuAuthentication();

            switch (choseCreateNewAccuoutOrLogin)
            {
                case "1":
                    user = loginOrRegistration.Login();
                    break;
                case "2":
                    user = loginOrRegistration.RegistrationNewAccount();
                    break;
            }
            var menuLang = user.Language;
            var measureUnit = user.UnitOfMeasure;
            var userRole = user.IdRole.ToString();
            var username = user.Username;
            
            var exit = true;
            if (userRole == "1")
            {
                var admin = new AdminActions(menuLang, queryBuilder, new ExitService());

                while (exit)
                {
                    var choiceSelect = menu.ShowFirtsMenuAdmin();
                    admin.AdminLogic(choiceSelect);
                    if (choiceSelect == "4")
                    {
                        exit = false;
                    }
                }
            }

            exit = true;
            while (exit)
            {
                var forecastManager = new ForecastAction(menuLang, new ExitService(), new PrintData(), new MeteoApi(), queryBuilder);
                forecastManager.Actions(user.Username, measureUnit);
            }
        }
    }
}