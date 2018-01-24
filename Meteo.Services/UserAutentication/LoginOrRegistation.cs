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
            var usernameAuthentication = "";
            var choseCreateNewAccuoutOrLogin = "";



           var queryBuilder = QueryBuilderServices.QueryBuilder();


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
            var loginServices = new LoginService(lang);

            var registrationServices = new RegistrationService(lang);



            var controlFirstChoiceLogin = true;
            while (controlFirstChoiceLogin)
            {
                choseCreateNewAccuoutOrLogin = Console.ReadLine();
                //Caso scelta Login con utente già registrato su DB
                if (choseCreateNewAccuoutOrLogin == "1")
                {

                    var accessOrNot = loginServices.LoginWithUserAndPsw();
                    if ( accessOrNot == false)
                    {
                        loginServices.GetQuestion();
                        loginServices.AutenticationWithAnswer();
                        usernameAuthentication = loginServices.InsertNewPsw();

                        
                    }



                }
                if (choseCreateNewAccuoutOrLogin == "2")
                {

                    usernameAuthentication = registrationServices.Registration();


                }


            }
                
            return usernameAuthentication;


            // Creazione nuovo utente 
        }
    }
}


