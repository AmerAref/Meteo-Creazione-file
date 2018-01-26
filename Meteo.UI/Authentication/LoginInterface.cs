﻿using System;
namespace Meteo.UI.Authentication
{

   public class LoginUserInterface
    {
        private string _lang;
        public LoginUserInterface(string lang)
        {
            _lang = lang;
        }
        public void InsertUsername()
        {
            if (_lang == "1")
            {
                Console.WriteLine(DataInterface.insertUserIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertUserEN);

            }
            return;
        }
        public void InsertPassword()
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


        public void WelcomeUser(string username)
        {
            Console.WriteLine("\n");
            if (_lang == "1")
            {
                Console.WriteLine("Benvenuto" + " " + $"{username}");
            }
            else
            {
                Console.WriteLine("Welcome" + " " + $"{username}");
            }
        }

        public void WrongPassword(int countAttempts)
        {
            if (_lang == "1")
            {
                Console.WriteLine($"\n{DataInterface.reinsertUserPswIT}");
                Console.WriteLine($"{DataInterface.remainingAttemptsIT} {countAttempts}");
            }
            else
            {
                Console.WriteLine($"\n{DataInterface.reinsertUserPswEN}");
                Console.WriteLine($"{DataInterface.remainingAttemptsEN} {countAttempts}");
            }
        }

        public string SecureQuestion()
        {
            if (_lang == "1")
            {
                Console.WriteLine($"\n{DataInterface.secureQuestionIT}");
            }
            else
            {
                Console.WriteLine($"\n{DataInterface.secureQuestionEN}");
            }
            var secureAnswer = Console.ReadLine();
            return secureAnswer;
        }

        public void InsertNewPassword()
        {
            if (_lang == "1")
            {
                Console.WriteLine($"\n{DataInterface.newPswIT}");
            }
            else
            {
                Console.WriteLine($"\n{DataInterface.newPswEN}");
            }
        }

        public void WrongRegexNewPassowrd()
        {
            if (_lang == "1")
            {
                Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                Console.WriteLine("\nReinserisci Password!");
            }
            else
            {
                Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
                Console.WriteLine("\nReenter Password!");
            }
        }

        public void FinishedAttempts()
        {
            if (_lang == "1")
            {
                Console.WriteLine("Mi dispiace, ma hai esaurito i tentativi!");
            }
            else
            {
                Console.WriteLine("I'm sorry, but you've exhausted the attempts!");
            }
        }
    }
}
