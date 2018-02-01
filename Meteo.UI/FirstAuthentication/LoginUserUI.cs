using System;
using Meteo.Services;

namespace Meteo.UI.Authentication
{

   public class LoginUserUI
    {
        private string _lang;
        public LoginUserUI(string lang)
        {
            _lang = lang;
        }
        public string ReadUsername()
        {
            if (_lang == "it")
            {
                Console.WriteLine(DataInterface.insertUserIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertUserEN);

            }
            var usernameAuthentication = Console.ReadLine();

            return usernameAuthentication;
        }
        public string ReadPassword()
        {
            if (_lang == "it")
            {
                Console.WriteLine(DataInterface.insertPswIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertPswEN);
            }
            var passwordLogin = "";
            var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
            var authPwd = Register.EncryptPwd(passwordAuthentication);
            return authPwd;
        }


        public void WelcomeUser(string username)
        {
            Console.WriteLine("\n");
            if (_lang == "it")
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
            if (_lang == "it")
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
            if (_lang == "it")
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

        public string ReadNewPassword()
        {
            if (_lang == "it")
            {
                Console.WriteLine($"\n{DataInterface.newPswIT}");
            }
            else
            {
                Console.WriteLine($"\n{DataInterface.newPswEN}");
            }

            var newPswMask = "";
            var pswMask = DataMaskManager.MaskData(newPswMask);
            return pswMask;
        }

        public void WrongRegexNewPassowrd()
        {
            if (_lang == "it")
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
            if (_lang == "it")
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

