using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;

namespace Meteo.UI
{
    public class Menu
    {
        private IService _exit;
        private IQueryBuilder _queryBuilder { get; set; }
        private int _lang;
        public Menu(IQueryBuilder queryBuilder, int lang, IService exit)

        {
            _queryBuilder = queryBuilder;
            _lang = lang;
            _exit = exit;
        }

        public void ChangeLanguage(int lang)
        {
            _lang = lang;
        }

        public int SelectLanguageStart()
        {
            var exit = true;
            while (exit)
            {
                Console.WriteLine("");
                Console.WriteLine("**************************************************************");
                Console.WriteLine("*****        Scegli la lingua/Select the language         ****");
                Console.WriteLine("****1 - Italiano/Italian (1)                                 *");
                Console.WriteLine("****2 - Inglese/English  (2)                                 *");
                Console.WriteLine("**************************************************************");

                var lang = Convert.ToInt32(Console.ReadLine());
                if (lang != 1 && lang != 2)
                {
                    Console.WriteLine("Lingua errata! Reinserisci la lingua! / Wrong language! Reenter the language!");
                }
                else
                {
                    return lang;
                }
            }
            return 0;
        }
        public string ShowMenuAuthentication()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Effettua Login                                    *");
                    Console.WriteLine("****2 - Crea nuovo utente                                 *");
                    Console.WriteLine("***********************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Login                                             *");
                    Console.WriteLine("****2 - Create new account                                *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
            var choiceSelected = Console.ReadLine();
            _exit.Exit(choiceSelected);
            return choiceSelected;
        }
        public int SelectQuestion()
        {
            switch (_lang)
            {
                case 1:
                    var allQuestionObj = _queryBuilder.AllQuestionsIT();
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine(" Digitare il valore del menu per scelta domanda di sicurezza");

                    foreach (var question in allQuestionObj)
                    {
                        Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
                    }
                    Console.WriteLine("************************************************************");
                    break;
                case 2:
                    allQuestionObj = _queryBuilder.AllQuestionsEN();
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine(" Digit value  ");
                    foreach (var question in allQuestionObj)
                    {
                        Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestion}");
                    }
                    Console.WriteLine("************************************************************");
                    break;
            }
            var idSelectedForQuestion = Convert.ToInt32(Console.ReadLine());

            return idSelectedForQuestion;
        }

        public int SelectLanguage()
        {
            var allLanguages = _queryBuilder.GetAllLanguages();
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("***************** Scegli la tua lingua *********************");
                    foreach (var language in allLanguages)
                    {
                        Console.WriteLine($"{language.IdLanguage} - {language.LanguageType}");
                    }
                    Console.WriteLine("************************************************************");
                    break;
                case 2:
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("***************** Choose your language *********************");
                    foreach (var language in allLanguages)
                    {
                        Console.WriteLine($"{language.IdLanguage} - {language.LanguageType}");
                    }
                    Console.WriteLine("************************************************************");
                    break;
            }

            var idSelectedLanguage = Convert.ToInt32(Console.ReadLine());

            return idSelectedLanguage;
        }


        public string Confirmation()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("******** Digitare il valore del menu per conferma risposta *");
                    Console.WriteLine("****1 - Conferma                                           *");
                    Console.WriteLine("****2 - Reinserisci                                        *");
                    Console.WriteLine("************************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("\n**********************************************************");
                    Console.WriteLine("*************            Digit value            ************");
                    Console.WriteLine("****1 - Confirm                                            *");
                    Console.WriteLine("****2 - Reenter                                            *");
                    Console.WriteLine("************************************************************");
                    break;
            }

            var confermation = Console.ReadLine();
            _exit.Exit(confermation);

            return confermation;
        }

        public string ShowFirtsMenuAdmin()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("*************       Digitare il valore del menu       ***********");
                    Console.WriteLine("****1 - Visualizza tutti gli utenti registrati                  *");
                    Console.WriteLine("****2 - Visualizza la tabella 'Master'                          *");
                    Console.WriteLine("****3 - Modifica la tabella 'User' (crea/elimina/modifica user) *");
                    Console.WriteLine("****4 - Accedi al menu previsioni                               *");
                    Console.WriteLine("****5 - Aggiorna la tabella 'City' del database                 *");
                    Console.WriteLine("*****************************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("*************       Digitare il valore del menu       ***********");
                    Console.WriteLine("****1 - View all the registrated users                          *");
                    Console.WriteLine("****2 - View the 'Master' table                                 *");
                    Console.WriteLine("****3 - Modify the 'User' table (create/delete/modify user)     *");
                    Console.WriteLine("****4 - Access to forecast menu                                 *");
                    Console.WriteLine("****5 - Update the 'City' table of the database                 *");
                    Console.WriteLine("*****************************************************************");
                    break;
            }
            var choiceSelected = Console.ReadLine();
            _exit.Exit(choiceSelected);

            return choiceSelected;
        }
        public string ShowSecondMenuAdmin()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("****1 - Vuoi eliminare un utente?                               *");
                    Console.WriteLine("****2 - Vuoi modificare la password di un utente?               *");
                    Console.WriteLine("****3 - Vuoi modificare il ruolo di un utente?                  *");
                    Console.WriteLine("****4 - Torna al menù principale                                *");
                    Console.WriteLine("*****************************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("*****************************************************************");
                    Console.WriteLine("****1 - Do you want to delete an user?                          *");
                    Console.WriteLine("****2 - Do you want to modify an users password?                *");
                    Console.WriteLine("****3 - Do you want to modify an users role?                    *");
                    Console.WriteLine("****4 - Go back to the main menu                                *");
                    Console.WriteLine("*****************************************************************");
                    break;
            }
            var choiceSelected = Console.ReadLine();
            _exit.Exit(choiceSelected);

            return choiceSelected;
        }
        public int SelectRole()
        {
            switch (_lang)
            {
                case 1:
                    var allRoles = _queryBuilder.AllRoles();
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("Digitare il valore del menu per scelta del ruolo           *");
                    foreach (var role in allRoles)
                    {
                        Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
                    }
                    Console.WriteLine("************************************************************");
                    break;
                case 2:
                    allRoles = _queryBuilder.AllRoles();
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("*************            Digit value            ************");
                    foreach (var role in allRoles)
                    {
                        Console.WriteLine($"{role.IdRole} - {role.RoleType} *");
                    }
                    Console.WriteLine("************************************************************");
                    break;
            }
            var roleSelected = Console.ReadLine();

            _exit.Exit(roleSelected);

            var roleFormatted = Convert.ToInt32(roleSelected);

            return roleFormatted;
        }

        public string ShowFirst()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Leggi dati relativi ad oggi                       *");
                    Console.WriteLine("****2 - Leggi dati riferiti ai prossimi 5 giorni          *");
                    Console.WriteLine("****3 - Accedi al menu filtraggio dati per città          *");
                    Console.WriteLine("****4 - Vuoi eliminare file nella cartella ?              *");
                    Console.WriteLine("****5 - Manda una email contenente un file a scelta       *");
                    Console.WriteLine("****6 - Elimina record delle ricerche affettuate          *");
                    Console.WriteLine("****7 - Esporta dati sulle ricerche effettuate            *");
                    Console.WriteLine("***********************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Read for data related to today                    *");
                    Console.WriteLine("****2 - Read for data related to the last 5 days          *");
                    Console.WriteLine("****3 - Access the data filtering menu by city            *");
                    Console.WriteLine("****4 - Do you want to delete files in the folder?        *");
                    Console.WriteLine("****5 - Send an email with a file in it                   *");
                    Console.WriteLine("****6 - Delete records from the researches                *");
                    Console.WriteLine("****7 - Export the research data                          *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
            var choiceSelect = Console.ReadLine();
            _exit.Exit(choiceSelect);

            return choiceSelect;
        }
        public string ShowSecondMenu()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("****       Inserisci parametri di ricerca                 *");
                    Console.WriteLine("***********************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************   Enter parameters to search     * **********");
                    Console.WriteLine("***********************************************************");
                    break;
            }
            var parameters = Console.ReadLine();
            _exit.Exit(parameters);
            return parameters;
        }
        public string ShowFiltredMenu()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Fitra ricerche per città                          *");
                    Console.WriteLine("****2 - Filtra ricerche per data                          *");
                    Console.WriteLine("****3 - Torna al menu principale                          *");
                    Console.WriteLine("***********************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - filter searches by city                           *");
                    Console.WriteLine("****2 - filter searches by data                           *");
                    Console.WriteLine("****3 - Go back to the main menu                          *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
            var choseSelected = Console.ReadLine();
            _exit.Exit(choseSelected);

            return choseSelected;
        }
        public string ShowExportMenu()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************    Digitare il valore del menu    ***********");
                    Console.WriteLine("****1 - Esporta dati relativi alle ricerce                *");
                    Console.WriteLine("****2 - Esporta dati passando un range di date            *");
                    Console.WriteLine("****3 - Torna al menu principale                          *");
                    Console.WriteLine("***********************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("*************            Digit value            ***********");
                    Console.WriteLine("****1 - Export forecast research                          *");
                    Console.WriteLine("****2 - Export data giving a date range                   *");
                    Console.WriteLine("****3 - Return to the main menu                           *");
                    Console.WriteLine("***********************************************************");
                    break;
            }
            var exportChoice = Console.ReadLine();
            _exit.Exit(exportChoice);

            return exportChoice;
        }

        public string Chioce()
        {
            switch (_lang)
            {
                case 1:
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("********       Digitare il valore del menu     *************");
                    Console.WriteLine("****1 - SI                                                 *");
                    Console.WriteLine("****2 - No                                                 *");
                    Console.WriteLine("************************************************************");
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("************************************************************");
                    Console.WriteLine("********               Digit value             *************");
                    Console.WriteLine("****1 - YES                                                *");
                    Console.WriteLine("****2 - No                                                 *");
                    Console.WriteLine("************************************************************");
                    break;
            }
            var choseSelected = Console.ReadLine();
            _exit.Exit(choseSelected);

            return choseSelected;
        }
    }
}