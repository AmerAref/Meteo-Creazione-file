using System;
using Meteo.Services;

namespace Meteo.UI.AdminActions
{
    public class AdminManagerUI
    {
        public string _lang;

        public AdminManagerUI(string lang)
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
        public string InsertNameUserToModfy ()
        {
            
            if (_lang == "it")
            { 
                Console.WriteLine("Inserisci l'username dell'utente da modificare");
            }
            else
            { 
                Console.WriteLine("Insert the username of the user to modify"); 
            }
            var usernameModify = Console.ReadLine();
            return usernameModify;
        }
        public string InsertFirstPsw()
        {
            var pswModify = "";
            if (_lang == "it")
            {
                Console.WriteLine("\nInserisci la nuova password dell'utente");
            }
            else
            {
                Console.WriteLine("\nInsert the new password of the user");
            }
            var pswModifyRegex = DataMaskManager.MaskData(pswModify);
            return pswModifyRegex;

            
        }
        public string InsertSecondPsw()
        {
            var pswModify = "";

            if (_lang == "it")
            {
                Console.WriteLine("\nReinserisci la nuova password dell'utente");
            }
            else
            {
                Console.WriteLine("\nReenter the new password of the users");
            }
            var pswModifyRegex = DataMaskManager.MaskData(pswModify);
            return pswModifyRegex;

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
        public void AttemtsRegexPsw()
        {
            if (_lang == "it")
            {
                Console.WriteLine($"\nLa psw non rispetta i criteri di sicurezza");
            }
            else
            {
                Console.WriteLine($"\nThe psw does not meet the security criteria");
            }
        }
    }
}
