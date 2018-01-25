using System;
namespace Meteo.UI.AdminActions
{
    public class AdminInterface
    {
        public string _lang;

        public AdminInterface(string lang)
        {
            _lang = lang;
        }

        public void Exit()
        {
            
            if (_lang == "it")
            {
                Console.WriteLine("Sessione terminata");
            }
            else
            {
                Console.WriteLine("Session ended");
            }
        }
        public void InsertUsernameToDelete()
        {
            if (_lang == "it")
            {
               
                Console.WriteLine("Inserisci l'username dell'utente da eliminare");
            }
            else
            {
               
                Console.WriteLine("Insert the username of the user to delete");
            }


        }
        public void InsertNmeUserToModfy ()
        {
            if (_lang == "it")
            { 
                Console.WriteLine("Inserisci l'username dell'utente da modificare");
            }
            else
            { 
                Console.WriteLine("Insert the username of the user to modify"); 
            }
        }
        public void InsertFirstPsw()
        {
            if (_lang == "it")
            {
                Console.WriteLine("Inserisci la nuova password dell'utente");
            }
            else
            {
                Console.WriteLine("Insert the new password of the user");
            }

            
        }
        public void InsertSecondPsw()
        {
            if (_lang == "it")
            {
                Console.WriteLine("Inserisci la nuova password dell'utente");
                Console.WriteLine("\nReinserisci la nuova password dell'utente");
            }
            else
            {
                Console.WriteLine("Insert the new password of the user");
                Console.WriteLine("\nReenter the new password of the users");
            }
        }
        public void AttemptsPsw(int pswModifyCount)
        {
            if (_lang == "it")
            { 
                Console.WriteLine($"\nLe due password non combaciano! Hai ancora {pswModifyCount} tentativi."); 
            }
            else
            { 
                Console.WriteLine($"\nThe two passwords do not match! You still have {pswModifyCount} attempts.");
            }

        }
    }
}
