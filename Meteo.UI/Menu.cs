using System;
using Meteo.Services.Infrastructure;

namespace Meteo.UI
{
    public class Menu
    {
        private IQueryBuilder _queryBuilder { get; set; }
        private string _lang;
        public Menu(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }
        public void ChangeLangages(string lang)
        {
            _lang = lang;

        }

        public void SelectLanguageStart()
        {
            Console.WriteLine("\n************************************************************");
            Console.WriteLine("*****        Scegli la lingua/Select the language         ****");
            Console.WriteLine("****1 - Italiano/Italian                                     *");
            Console.WriteLine("****2 - Inglese/English                                      *");
            Console.WriteLine("**************************************************************");
        }

        public void ShowFirtsMenuAdmin()
        {
            switch (_lang)
            {
                case "1":


                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("*************       Digitare il valore del menu       ***********");
                    Console.WriteLine("****1 - Visualizza tutti gli utenti registrati                  *");
                    Console.WriteLine("****2 - Visualizza la tabella 'Master'                          *");
                    Console.WriteLine("****3 - Modifica la tabella 'User' (crea/elimina/modifica user) *");
                    Console.WriteLine("****4 - Accedi al menu previsioni                               *");
                    Console.WriteLine("****5 - Termina sessione                                        *");
                    Console.WriteLine("*****************************************************************");

                    break;

                case "2":

                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("*************       Digitare il valore del menu       ***********");
                    Console.WriteLine("****1 - View all the registrated users                          *");
                    Console.WriteLine("****2 - View the 'Master' table                                 *");
                    Console.WriteLine("****3 - Modify the 'User' table (create/delete/modify user)     *");
                    Console.WriteLine("****4 - Access to forecast menu                             *");
                    Console.WriteLine("****5 - End session                                             *");
                    Console.WriteLine("*****************************************************************");
                    break;
            }

        }

        public void ShowSecondMenuAdmin()
        {
            switch (_lang)
            {
                case "1":
                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("****1 - Vuoi eliminare un utente?                               *");
                    Console.WriteLine("****2 - Vuoi modificare la password di un utente?               *");
                    Console.WriteLine("****3 - Vuoi modificare il ruolo di un utente?                  *");
                    Console.WriteLine("****4 - Torna al menù principale                                *");
                    Console.WriteLine("****5 - Termina sessione                                        *");
                    Console.WriteLine("*****************************************************************");
                    break;

                case "2":

                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("****1 - Do you want to delete an user?                          *");
                    Console.WriteLine("****2 - Do you want to modify an users password?                *");
                    Console.WriteLine("****3 - Do you want to modify an users role?                    *");
                    Console.WriteLine("****4 - Go back to the main menu                                *");
                    Console.WriteLine("****5 - End session                                             *");
                    Console.WriteLine("*****************************************************************");

                    break;
            }


        }
        public void ShowMenuAuthentication()
        {
            switch (_lang)
            {
                case "1":
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Effettua Login                                    *");
                    Console.WriteLine("****2 - Crea nuovo utente                                 *");
                    Console.WriteLine("****3 - Termina sessione                                  *");
                    Console.WriteLine("***********************************************************");
                    break;

                case "2":


                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Login                                             *");
                    Console.WriteLine("****2 - Create new account                                *");
                    Console.WriteLine("****3 - End session                                       *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
        }




        public void ShowFirst()
        {
            switch (_lang)
            {
                case "1":
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Cerca dati relativi ad oggi                       *");
                    Console.WriteLine("****2 - Cerca dati riferiti agli ultimi 5 giorni          *");
                    Console.WriteLine("****3 - Accedi al menu filtraggio dati per città          *");
                    Console.WriteLine("****4 - Vuoi eliminare file nella cartella ?              *");
                    Console.WriteLine("****5 - Invia file tramite email                          *");
                    Console.WriteLine("****6 - Creare un file XLS                                *");
                    Console.WriteLine("****7 - Termina sessione                                  *");
                    Console.WriteLine("***********************************************************");

                    break;

                case "2":



                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Search for data related to today                  *");
                    Console.WriteLine("****2 - Search for data related to the last 5 days        *");
                    Console.WriteLine("****3 - Access the data filtering menu by city            *");
                    Console.WriteLine("****4 - Do you want to delete files in the folder?        *");
                    Console.WriteLine("****5 - Send files via email                              *");
                    Console.WriteLine("****6 - Create an XLS file                                *");
                    Console.WriteLine("****7 - End session                                       *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
        }
        public void ShowSecondMenu()
        {
            switch (_lang)
            {
                case "1":

                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Cerca dati relativi a Città                       *");
                    Console.WriteLine("****2 - Cerca dati relativi a Coordinate                  *");
                    Console.WriteLine("****3 - Torna al menu pricipale                           *");
                    Console.WriteLine("****4 - Termina sessione                                  *");
                    Console.WriteLine("***********************************************************");
                    break;

                case "2":
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Search for data about city                        *");
                    Console.WriteLine("****2 - Search for data about coordinates                 *");
                    Console.WriteLine("****3 - Go back to the main menu                          *");
                    Console.WriteLine("****4 - End session                                       *");
                    Console.WriteLine("***********************************************************");
                    break;



            }
        }

        public void SelectLanguage()
        {
            switch (_lang)
            {
                case "1":
                    Console.WriteLine("\n************************************************************");
                    Console.WriteLine("***** Digitare il valore del menu per scelta della lingua ****");
                    Console.WriteLine("****1 - Italiano                                             *");
                    Console.WriteLine("****2 - Inglese                                              *");
                    Console.WriteLine("**************************************************************");
                    break;
                case "2":
                    Console.WriteLine("\n*********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Italian                                           *");
                    Console.WriteLine("****2 - English                                           *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
        }
        public void ShowFiltredMenu()
        {
            switch (_lang)
            {
                case "1":
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Filtra dati relativi a umidità                    *");
                    Console.WriteLine("****2 - Filtra dati per data e/o ora                      *");
                    Console.WriteLine("****3 - Filtra per Qualità cielo                          *");
                    Console.WriteLine("****4 - Torna al menu principale                          *");
                    Console.WriteLine("***********************************************************");
                    break;

                case "2":
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Filter humidity data                              *");
                    Console.WriteLine("****2 - Filter data by date and / or time                 *");
                    Console.WriteLine("****3 - Filter by sky quality                             *");
                    Console.WriteLine("****4 - Go back to the main menu                          *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
        }


        public void SelectQuestion()
        {
            switch (_lang)
            {
                case "1":

                    var allQuestionObj = _queryBuilder.AllQuestionsIT();
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine(" Digitare il valore del menu per scelta domanda di sicurezza");

                    foreach (var question in allQuestionObj)
                    {
                        Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
                    }
                    Console.WriteLine("************************************************************");
                    break;

                case "2":
                    allQuestionObj = _queryBuilder.AllQuestionsEN();
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine(" Digit value  ");
                    foreach (var question in allQuestionObj)
                    {
                        Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
                    }
                    Console.WriteLine("************************************************************");
                    break;
            }
        }

        public void SelectRole()
        {

            switch (_lang)
            {
                case "1":


                    var allRoles = _queryBuilder.AllRoles();
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("Digitare il valore del menu per scelta del ruolo           *");
                    foreach (var role in allRoles)
                    {
                        Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
                    }
                    Console.WriteLine("************************************************************");
                    break;

                case "2":




                    allRoles = _queryBuilder.AllRoles();
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("*************            Digit value            ************");
                    foreach (var role in allRoles)
                    {
                        Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
                    }
                    Console.WriteLine("************************************************************");
                    break;
            }
        }










        public void ShowMenuCreateXlsFile()
        {
            switch (_lang)
            {
                case "1":


                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Crea XLS per dati relativi a oggi                 *");
                    Console.WriteLine("****2 - Crea XLS per dati relativi a 5 giorni             *");
                    Console.WriteLine("****3 - Torna al menu principale                          *");
                    Console.WriteLine("****4 - Termina sessione                                  *");
                    Console.WriteLine("***********************************************************");

                    break;

                case "2":


                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Create XLS for today's data                       *");
                    Console.WriteLine("****2 - Create XLS for data for 5 days                    *");
                    Console.WriteLine("****3 - Return to the main menu                           *");
                    Console.WriteLine("****4 - End session                                       *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
        }

        public void Confirmation()
        {
            switch (_lang)
            {
                case "1":

                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("******** Digitare il valore del menu per conferma risposta *");
                    Console.WriteLine("****1 - Conferma                                           *");
                    Console.WriteLine("****2 - Reinserisci                                        *");
                    Console.WriteLine("************************************************************");

                    break;

                case "2":

                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("*************            Digit value            ************");
                    Console.WriteLine("****1 - Confirm                                            *");
                    Console.WriteLine("****2 - Reenter                                            *");
                    Console.WriteLine("************************************************************");
                    break;
            }
        }
        public void Chioce()
        {
            switch (_lang)
            {
                case "1":


                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("********       Digitare il valore del menu     *************");
                    Console.WriteLine("****1 - SI                                                 *");
                    Console.WriteLine("****2 - No                                                 *");
                    Console.WriteLine("************************************************************");

                    break;

                case "2":




                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("********               Digit value             *************");
                    Console.WriteLine("****1 - YES                                                *");
                    Console.WriteLine("****2 - No                                                 *");
                    Console.WriteLine("************************************************************");
                    break;

            }
        }
    }
}


