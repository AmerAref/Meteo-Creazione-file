using System;
using System.Reflection;
using Meteo.Services.Infrastructure;
using Meteo.UI;
using Ninject;

namespace Meteo.Services
{
    public class LoginService
    {

        private IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        private Menu Menu;
        private string _lang { get; set; }
        private string _usernameAuthentication;
        private string _questionPrintedForAccess;


        public LoginService(string lang)

        {
            _lang = lang;
        }

        public LoginService()
        {
            Menu = new Menu(queryBuilder);

        }

        public bool LoginWithUserAndPsw()
        {

            var countAttempts = 2;
            var passwordLogin = "";

            for (var m = 0; m < 2; m++)
            {

                if (_lang == "1")
                {
                    Console.WriteLine(DataInterface.insertUserIT);
                    _usernameAuthentication = Console.ReadLine();
                    Console.WriteLine(DataInterface.insertPswIT);
                }
                else
                {
                    Console.WriteLine(DataInterface.insertUserEN);
                    _usernameAuthentication = Console.ReadLine();
                    Console.WriteLine(DataInterface.insertPswEN);
                }

                // Inserimento psw mascherata
                var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
                // Criptaggio Psw
                var authPwd = Register.EncryptPwd(passwordAuthentication);
                // confronto se esiste psw (Massimo 3 volte )
                var autentication = queryBuilder.GetUserIfExist(_usernameAuthentication, authPwd);
                if (autentication != null)
                {
                    // Da il benvenuto e accede al menu Meteo
                    Console.WriteLine("\n");
                    if (_lang == "1")
                    {
                        Console.WriteLine("Benvenuto" + " " + $"{_usernameAuthentication}");
                    }
                    else
                    {
                        Console.WriteLine("Welcome" + " " + $"{_usernameAuthentication}");
                    }
                    return true ;


                }
                else
                {
                    // Reinserimento Psw (massimo altri 2 tentativi)

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


                }


                countAttempts--;

            }
            return false;
        }


        public void GetQuestion()
        {
            for (var m = 0; m < 2; m++)
            {


                if (_lang == "1")
                {
                    Console.WriteLine($"{DataInterface.secureQuestionIT}");

                }
                else
                {

                    Console.WriteLine($"{DataInterface.secureQuestionEN}");
                }

                _usernameAuthentication = Console.ReadLine();
                var userIfExist = queryBuilder.GetUser(_usernameAuthentication);
                if (userIfExist != null)
                {
                    var IdQuestionInTableUser = queryBuilder.GetUser(_usernameAuthentication).IdQuestion;
                     _questionPrintedForAccess = queryBuilder.GetQuestion(IdQuestionInTableUser).DefaultQuestion;
                    return ;
                }

            }
            Environment.Exit(0);
            return ;

        }



        public void AutenticationWithAnswer()
        {
            for (var m = 0; m < 2; m++)
            {

                Console.WriteLine(_questionPrintedForAccess);
                var answerToLogin = "";
                var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                Console.WriteLine();
                // criptaggio della Risposta inserita 
                var insertAnswerForAccessEcrypted = Register.EncryptPwd(insertAnswerMaskered);
                var autentication = queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, _usernameAuthentication);
                if (autentication != null)
                {
                    return ;
                }


            }
            Environment.Exit(0);
            return ;

        }


     public string InsertNewPsw()
        {
            for (var m = 0; m < 2; m++)
            {
                if (_lang == "1")
                {
                    Console.WriteLine($"\n{DataInterface.newPswIT}");
                }
                else
                {
                    Console.WriteLine($"\n{DataInterface.newPswEN}");
                }

                var newPswClear = "";
                // maschera nuova psw 
                var newPswMask = DataMaskManager.MaskData(newPswClear);
                // controlo su vincoli di sicurezza psw
                if (Helper.RegexForPsw(newPswMask) == false)
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
                else
                {
                    // criptaggio nuova psw 
                    var newPswEncrypted = Register.EncryptPwd(newPswMask);

                    // Aggiornamento psw in DB
                    queryBuilder.QueryForUpdatePsw(newPswEncrypted, _usernameAuthentication);
                    // Uscita dal while 
                    return _usernameAuthentication;

                }
            }
            Environment.Exit(0);
            return null;



        }

    }
}


