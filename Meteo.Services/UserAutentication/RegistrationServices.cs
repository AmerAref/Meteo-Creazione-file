using System;
using System.Reflection;
using Meteo.Services.Infrastructure;
using Meteo.UI;
using Ninject;

namespace Meteo.Services
{
    public class RegistrationService
    {
        private Menu menu;
        public string _lang { get; set; }
        static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        private int _countAttemptsPswRegister = 0;

        public RegistrationService(string lang)
        {
            _lang = lang;
        }
        public RegistrationService()
        {
            menu = new Menu(queryBuilder);
           
        }
        public string Registration()
        {
            var IdSelectedForQuestion = 0;
            var passwordRegistration = "";
            var validationManagerPsw = true;
            var pswNewAccount = "";
            var insertAnswer = "";
            var usernameNewAccount = "";
            var measureUnit = "";

            // Inserimento nome Registrazione
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci Nome");
            }
            else
            {
                Console.WriteLine("Enter Name");
            }
            var nameNewAccuont = Console.ReadLine();

            // Inserimento Cognome Registrazione
            if (_lang == "1")
            {
                Console.WriteLine("Inserisci il Cognome");
            }
            else
            {
                Console.WriteLine("Enter Surname");
            }
            var surnameNewAccount = Console.ReadLine();
            // While Per validazione psw
            while (validationManagerPsw)
            {
                usernameNewAccount = ControlIfUserExists();
                if (_lang == "1")
                {
                    Console.WriteLine(DataInterface.insertPswIT);
                }
                else
                {
                    Console.WriteLine(DataInterface.insertPswEN);
                }
                // Inserisci Psw mascherata

                pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
                // Controlla se Accetta i criteri di sicurezza psw
                if (Helper.RegexForPsw(pswNewAccount) == false)
                {
                    ControlRegexPassword();
                }
                // se inserisce psw secondo i criteri di sicurezza deve riscrivere la psw da confrontare con la precedente 
                else
                {
                    if (_lang == "1")
                    {
                        Console.WriteLine("\nReinserisci Password.");
                    }
                    else
                    {
                        Console.WriteLine("\nReenter Password.");
                    }
                    // maschera reinserimento psw
                    var pswNewAccountComparison = DataMaskManager.MaskData(passwordRegistration);
                    // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                    if (pswNewAccount == pswNewAccountComparison)
                    {
                        insertAnswer = InsertAnswer(IdSelectedForQuestion, insertAnswer);

                        // criptaggio di Psw e Risposta 
                        var encryptedPwd = Register.EncryptPwd(pswNewAccount);
                        var encryptedAnswer = Register.EncryptPwd(insertAnswer);
                        var selectLanguage = "";
                        var languageNewAccount = "";
                        int roleNewAccount = 2;
                        if (_lang == "1")
                        {
                            menu.SelectLanguageIT();
                            selectLanguage = Console.ReadLine();
                            switch (selectLanguage)
                            {
                                case "1":
                                    languageNewAccount = "it";
                                    measureUnit = "metric";
                                    break;
                                case "2":
                                    languageNewAccount = "en";
                                    measureUnit = "imperial";
                                    break;
                            }
                        }
                        else
                        {
                            menu.SelectLanguageEN();
                            selectLanguage = Console.ReadLine();
                            switch (selectLanguage)
                            {
                                case "1":
                                    languageNewAccount = "it";
                                    measureUnit = "metric";
                                    break;
                                case "2":
                                    languageNewAccount = "en";
                                    measureUnit = "imperial";
                                    break;
                            }
                        }

                        queryBuilder.InsertNewUser(encryptedPwd, usernameNewAccount, surnameNewAccount, nameNewAccuont, IdSelectedForQuestion, encryptedAnswer, languageNewAccount, measureUnit, roleNewAccount);
                        validationManagerPsw = false;
                        if (_lang == "1")
                        { Console.WriteLine($"\nBenvenuto utente: {usernameNewAccount}"); }
                        else
                        { Console.WriteLine($"\nWelcome user: {usernameNewAccount}"); }
                    }
                    else
                    {
                        if (_lang == "1")
                        { Console.WriteLine($"\nLe due password inserite non corrispondono! {DataInterface.reinsertUserPswIT}"); }
                        else
                        { Console.WriteLine($"\nThe two entered passwords don't match! {DataInterface.reinsertUserPswEN}"); }
                    }
                }
            }
            return usernameNewAccount;
        }
        public string ControlIfUserExists()
        {
            var validationUsername = true;
            var insertUsername = "";
            // While per controllo username (deve essere univoco sul DB)
            while (validationUsername)
            {
                if (_lang == "1")
                {
                    Console.WriteLine(DataInterface.insertUserIT);
                }
                else
                {
                    Console.WriteLine(DataInterface.insertUserEN);
                }

                insertUsername = Console.ReadLine();

                var autentication = queryBuilder.GetUser(insertUsername);
                // autentication è vuota nel caso in cui non esiste un user con stesso username
                if (autentication != null)
                {
                    // non ti fa uscire da while (reinserisci Username)
                    if (_lang == "1")
                    {
                        Console.WriteLine("Username già esistente. Provare con uno diverso!");
                    }
                    else
                    {
                        Console.WriteLine("Username already exists. Try with a different one!");
                    }
                }
                else
                {
                    // esce dal while che gestisce inserimento univoco user
                    validationUsername = false;
                }
            }
            return insertUsername;
        }

        public void ControlRegexPassword()
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
            _countAttemptsPswRegister++;

            // se l'utente non soddisfa i criteri per 3 volte termina la sessione
            if (_countAttemptsPswRegister == 3)
            {
                if (_lang == "1")
                {
                    Console.WriteLine("Mi dispiace, ma hai esaurito i tentativi!");
                }
                else
                {
                    Console.WriteLine("I'm sorry, but you've exhausted the attempts!");
                }
                Environment.Exit(0);
            }
        }
        public string InsertAnswer(int selectedQuestionId, string insertAnswer)
        {
            var controlWhileAnswer = true;

            while (controlWhileAnswer)
            {
                // menu per domande 
                if (_lang == "1")
                {
                    menu.SelectQuestionIT();
                }
                else
                {
                    menu.SelectQuestionEN();
                }

                // stampa risposta inserita
                selectedQuestionId = Convert.ToInt32(Console.ReadLine());
                if (_lang == "1")
                {
                    Console.WriteLine("Inserisci risposta di sicurezza");
                }
                else
                {
                    Console.WriteLine("Insert security answer");
                }
                // stampa risposta inserita 
                var questionSelected = queryBuilder.GetQuestion(selectedQuestionId);
                Console.WriteLine(questionSelected.DefaultQuestion);
                // conferma rispost inserita 
                insertAnswer = Console.ReadLine();
                if (_lang == "1")
                {
                    Console.WriteLine("La risposta richiesta è la seguente? ");
                }
                else
                {
                    Console.WriteLine("Is the following the required answer?");
                }
                Console.WriteLine(insertAnswer);

                // controllo su correttezza risposta
                if (_lang == "1")
                {
                    menu.ConfirmationIT();
                }
                else
                {
                    menu.ConfirmationEN();
                }

                // esce dal while quando viene selezionata l'opzione "1" e salva la risposta data alla domanda
                // altrimenti scegliendo "2" ti permette di reinserire la risposta
                var controlAnswer = Console.ReadLine();

                if (controlAnswer == "1")
                {
                    controlWhileAnswer = false;
                }
            }
            return insertAnswer;
        }
    }
}