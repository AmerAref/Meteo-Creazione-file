using System;
using Meteo.Services.Infrastructure;

namespace Meteo.UI
{
    public class Menu
    {
        public void ShowMenuAuthenticationIT()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Effettua Login                                    *");
            Console.WriteLine("****2 - Crea nuovo utente                                 *");
            Console.WriteLine("***********************************************************");
        }

        public void ShowMenuAuthenticationEN()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Login                                             *");
            Console.WriteLine("****2 - Create a new user                                 *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowFirst()
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
        public void ShowMenu()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Cerca dati relativi a Città                       *");
            Console.WriteLine("****2 - Cerca dati relativi a Coordinate                  *");
            Console.WriteLine("****3 - Torna al menu principale                          *");
            Console.WriteLine("****4 - Termina sessione                                  *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowFiltredMenu()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Filtra dati relativi a umidità                    *");
            Console.WriteLine("****2 - Filtra dati per data e/o ora                      *");
            Console.WriteLine("****3 - Filtra per Qualità cielo                          *");
            Console.WriteLine("****4 - Torna al menu                                     *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowMenuAuthentication()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Effettua Login                                    *");
            Console.WriteLine("****2 - Crea nuovo utente                                 *");
            Console.WriteLine("***********************************************************");
        }
        public void SelectQuestion(ApplicationDbContext context)
        {
            Console.WriteLine("\n*************************************************************************************");
            Console.WriteLine("************* Digitare il valore del menu per scelta domanda di sicurezza ***********");
            foreach (var question in context.Questions)
            {
                Console.WriteLine($"{question.IdQuestion} - {question.DefaultQuestions}");
            }
            Console.WriteLine("*************************************************************************************");
        }

        public void ShowMenuCreateXlsFile()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Crea XLS per dati relativi a oggi                 *");
            Console.WriteLine("****2 - Crea XLS per dati relativi a 5 giorni             *");
            Console.WriteLine("****3 - Torna al menu principale                          *");
            Console.WriteLine("****4 - Termina sessione                                  *");
            Console.WriteLine("***********************************************************");
        }
    }
}
