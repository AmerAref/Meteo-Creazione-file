using System;
using Meteo.UI;

namespace Meteo.Services
{
    public class RegistrationUserUI
    {
        public string _lang;

        public RegistrationUserUI(string lang)
        {
            _lang = lang;
        }

        public string ReadName()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci Nome");
            }
            else
            {
                Console.WriteLine("Enter Name");
            }
            var nameNewAccount = Console.ReadLine();

            return nameNewAccount;
        }
        public string ReadSurname()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci il Cognome");
            }
            else
            {
                Console.WriteLine("Enter Surname");
            }
            var surnameNewAccount = Console.ReadLine();

            return surnameNewAccount;
        }
        public string ReadUser()
        {
            if (_lang == "1")
            {
                Console.WriteLine(DataInterface.insertUserIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertUserEN);
            }
            var newUsername = Console.ReadLine();
            return newUsername;


        }


        public void IfUsernameExist()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Username già esistente. Provare con uno diverso!");
            }
            else
            {
                Console.WriteLine("Username already exists. Try with a different one!");
            }
        }

        public string ReadPsw()
        {
            var passwordRegistration = "";
            if (_lang == "1")
            {
                Console.WriteLine(DataInterface.insertPswIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertPswEN);
            }
            var newpsw = DataMaskManager.MaskData(passwordRegistration);
            return newpsw;

        }
        public void ReadPswSecondTime()
        {
            if (_lang == "1")
            {
                Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                Console.WriteLine("\nReinserisci Password.");
            }
            else
            {
                Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
                Console.WriteLine("\nReenter Password.");
            }
        }
        public string ComparisonPsw()
        {
            var passwordComparisonNotEcrypted = "";
            if (_lang == "1")
            {
                Console.WriteLine("\nReinserisci Password.");
            }
            else
            {
                Console.WriteLine("\nReenter Password.");
            }
            var compPsw = DataMaskManager.MaskData(passwordComparisonNotEcrypted);
            return compPsw;

        }

        public void PswNotEquals()
        {
            if (_lang == "1")
            {
                Console.WriteLine($"\nLe due password inserite non corrispondono! {DataInterface.reinsertUserPswIT}");
            }
            else
            {
                Console.WriteLine($"\nThe two entered passwords don't match! {DataInterface.reinsertUserPswEN}");
            }

        }

        public void InsertAnswer()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci risposta di sicurezza");
            }
            else
            {
                Console.WriteLine("Insert security answer");
            }

        }
        public void ConfirmationAnswer(string insertAnswer)
        {
            if (_lang == "1")
            {
                Console.WriteLine("La risposta richiesta è la seguente? ");
                Console.WriteLine(insertAnswer);

            }
            else
            {
                Console.WriteLine("Is the following the required answer?");
                Console.WriteLine(insertAnswer);

            }
        }



    }
}