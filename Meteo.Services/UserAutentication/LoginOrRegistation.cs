using System;
using System.Reflection;
using Meteo.Services.Infrastructure;
using Meteo.UI;
using Ninject;

namespace Meteo.Services
{
    public class LoginOrRegistration
    {
        public string Login()
        {
           
            var lang = "";
            var registrationServices = new RegistrationServices();
            var usernameAuthentication = "";
            var registration = new Register();
            var choseCreateNewAccuoutOrLogin = "";
            var loginServices = new LoginServices();
            var queryBuilderServices = new QueryBuilderServices();



           var queryBuilder = queryBuilderServices.QueryBuilder();


            var menu = new Menu(queryBuilder);

            menu.SelectLanguageStart();
            lang = Console.ReadLine();

            switch (lang)
            {
                case "1":
                    menu.ShowMenuAuthenticationIT();
                    break;
                case "2":
                    menu.ShowMenuAuthenticationEN();
                    break;
            }

            var controlFirstChoiceLogin = true;
            while (controlFirstChoiceLogin)
            {
                choseCreateNewAccuoutOrLogin = Console.ReadLine();
                //Caso scelta Login con utente già registrato su DB
                if (choseCreateNewAccuoutOrLogin == "1")
                {


                    usernameAuthentication = loginServices.Login(lang);
                    controlFirstChoiceLogin = false;


                }
                if (choseCreateNewAccuoutOrLogin == "2")
                {

                    usernameAuthentication = registrationServices.Registration(lang);
                    controlFirstChoiceLogin = false;

                }


            }
                
            return usernameAuthentication;


            // Creazione nuovo utente 
        }
    }
}


