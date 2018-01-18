using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI
{
    public class Menu
    {
        QueryManager queryManager = new QueryManager();

        public void ShowFirtsMenuAdminIT()
        {
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("*************       Digitare il valore del menu       ***********");
            Console.WriteLine("****1 - Visualizza tutti gli utenti registrati                  *");
            Console.WriteLine("****2 - Visualizza la tabella 'Master'                          *");
            Console.WriteLine("****3 - Modifica la tabella 'User' (crea/elimina/modifica user) *");
            Console.WriteLine("****4 - Termina sessione                                        *");
            Console.WriteLine("*****************************************************************");
        }

        public void ShowSecondMenuAdminIT()
        {
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("****1 - Vuoi creare un utente?                                  *");
            Console.WriteLine("****2 - Vuoi eliminare un utente?                               *");
            Console.WriteLine("****3 - Vuoi modificare la password di un utente?               *");
            Console.WriteLine("****4 - Termina sessione                                        *");
            Console.WriteLine("*****************************************************************");
        }
        public void ShowMenuAuthenticationIT()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Effettua Login                                    *");
            Console.WriteLine("****2 - Crea nuovo utente                                 *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowFirstIT()
        {
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
        }
        public void ShowSecondMenuIT()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Cerca dati relativi a Città                       *");
            Console.WriteLine("****2 - Cerca dati relativi a Coordinate                  *");
            Console.WriteLine("****3 - Torna al menu pricipale                          *");
            Console.WriteLine("****4 - Termina sessione                                  *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowFiltredMenuIT()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Filtra dati relativi a umidità                    *");
            Console.WriteLine("****2 - Filtra dati per data e/o ora                      *");
            Console.WriteLine("****3 - Filtra per Qualità cielo                          *");
            Console.WriteLine("****4 - Torna al menu principale                          *");
            Console.WriteLine("***********************************************************");
        }
        public void SelectQuestionIT()
        {
            var allQuestionObj = queryManager.AllQuestionsIT();
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine(" Digitare il valore del menu per scelta domanda di sicurezza");

            foreach (var question in allQuestionObj)
            {
                Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
            }
            Console.WriteLine("************************************************************");
        }

        public void SelectRoleIT()
        {
            var allRoles = queryManager.AllRoles();
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("Digitare il valore del menu per scelta del ruolo           *");
            foreach (var role in allRoles)
            {
                Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
            }
            Console.WriteLine("************************************************************");
        }



        public void SelectLanguage()
        {
            Console.WriteLine("\n***********************************************************");
            Console.WriteLine("********* Digitare il valore del menu per scelta della lingua");
            Console.WriteLine("****1 - Italiano                                             *");
            Console.WriteLine("****2 - Inglese                                              *");
            Console.WriteLine("**************************************************************");
        }

        public void ShowMenuCreateXlsFileIT()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Crea XLS per dati relativi a oggi                 *");
            Console.WriteLine("****2 - Crea XLS per dati relativi a 5 giorni             *");
            Console.WriteLine("****3 - Torna al menu principale                          *");
            Console.WriteLine("****4 - Termina sessione                                  *");
            Console.WriteLine("***********************************************************");
        }
        public void MenuDatabaseManager()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Crea Database 'MeteoDatabase'                     *");
            Console.WriteLine("****2 - Elimina Database 'MeteoDatabase'                  *");
            Console.WriteLine("***********************************************************");
        }
        public void ConfirmationIT()
        {
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("******** Digitare il valore del menu per conferma risposta *");
            Console.WriteLine("****1 - Conferma                                           *");
            Console.WriteLine("****2 - Reinserisci                                        *");
            Console.WriteLine("*********************************************************** ");
        }
        public void ChioceIT()
        {
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("********       Digitare il valore del menu     *************");
            Console.WriteLine("****1 - SI                                                 *");
            Console.WriteLine("****2 - No                                                 *");
            Console.WriteLine("*********************************************************** ");

        }

        //inizio menu in inglese 
        public void ChioceEN()
        {
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("********       Digitare il valore del menu     *************");
            Console.WriteLine("****1 - YES                                                *");
            Console.WriteLine("****2 - No                                                 *");
            Console.WriteLine("*********************************************************** ");

        }


        public void ShowFirstEN()
        {
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
        }
        public void ShowSecondMenuEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Search for data about city                        *");
            Console.WriteLine("****2 - Search for data about coordinates                 *");
            Console.WriteLine("****3 - Go back to the main menu                          *");
            Console.WriteLine("****4 - End session                                       *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowFiltredMenuEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Filter humidity data                              *");
            Console.WriteLine("****2 - Filter data by date and / or time                 *");
            Console.WriteLine("****3 - Filter by sky quality                             *");
            Console.WriteLine("****4 - Go back to the main menu                          *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowMenuAuthenticationEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Login                                             *");
            Console.WriteLine("****2 - Create new account                                *");
            Console.WriteLine("***********************************************************");
        }
        public void SelectQuestionEN()
        {
            var allQuestionObj = queryManager.AllQuestionsEN();
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine(" Digit value  ");
            foreach (var question in allQuestionObj)
            {
                Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
            }
            Console.WriteLine("************************************************************");
        }

        public void SelectLanguageEN()
        {
            Console.WriteLine("\n*********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Italian                                           *");
            Console.WriteLine("****2 - English                                           *");
            Console.WriteLine("***********************************************************");
        }

        public void ShowMenuCreateXlsFileEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Create XLS for today's data                       *");
            Console.WriteLine("****2 - Create XLS for data for 5 days                    *");
            Console.WriteLine("****3 - Return to the main menu                           *");
            Console.WriteLine("****4 - End session                                       *");
            Console.WriteLine("***********************************************************");
        }
        public void MenuDatabaseManagerEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************            Digit value            ***********");
            Console.WriteLine("****1 - Create Database 'MeteoDatabase'                   *");
            Console.WriteLine("****2 - Delete Database 'MeteoDatabase'                   *");
            Console.WriteLine("***********************************************************");
        }
        public void ConfirmationEN()
        {
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("*************            Digit value            ************");
            Console.WriteLine("****1 - Confirm                                            *");
            Console.WriteLine("****2 - Reenter                                            *");
            Console.WriteLine("*********************************************************** ");
        }
        public void SelectRoleEN()
        {
            var allRoles = queryManager.AllRoles();
            Console.WriteLine("\n**********************************************************");
            Console.WriteLine("*************            Digit value            ************");
            foreach (var role in allRoles)
            {
                Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
            }
            Console.WriteLine("************************************************************");
        }
    }
}