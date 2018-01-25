using System;
using Meteo.UI;

namespace Meteo.Services
{
    public class RegistrationUserInterface
    {
        public string _lang;

        public RegistrationUserInterface(string lang)
        {
            _lang = lang;
        }

        public void InsertName()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci Nome");
            }
            else
            {
                Console.WriteLine("Enter Name");
            }
            return;
        }
        public void InsertSurname()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci il Cognome");
            }
            else
            {
                Console.WriteLine("Enter Surname");
            }
            return;
        }
        public void InsertUser()
        {
            if (_lang == "1")
            {
                Console.WriteLine(DataInterface.insertUserIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertUserEN);
            }
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

        public void InserPsw()
        {
            if (_lang == "1")
            {
                Console.WriteLine(DataInterface.insertPswIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertPswEN);
            }
            return;

        }
        public void ReinsertPsw()
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
        public void ComparisonReinsertPsw()
        {
            if (_lang == "1")
            {
                Console.WriteLine("\nReinserisci Password.");
            }
            else
            {
                Console.WriteLine("\nReenter Password.");
            }
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