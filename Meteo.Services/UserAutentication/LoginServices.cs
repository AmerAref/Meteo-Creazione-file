using System;
using System.Reflection;
using Meteo.Services.Infrastructure;
using Meteo.UI;
using Ninject;

namespace Meteo.Services
{
    public class LoginServices
    {
        public string Login(string lang)
        {
            var attemptsAnswerForQuestion = 0;
            var newPswClear = "";
            var controlForUserIfExist = 0;
            var forAnswerInsertUsername = "";
            var controlWhilePsw = 0;
            var countAttempts = 2;
            var passwordLogin = "";
            var answerToLogin = "";
            var countAttemptsPswRegister = 0;
            var usernameAuthentication = "";


            var registration = new Register();

            var queryBuilderServices = new QueryBuilderServices();



            var queryBuilder = queryBuilderServices.QueryBuilder();

            var menu = new Menu(queryBuilder);

            while (controlWhilePsw < 5)
            {
                // Inserimento User
                if (lang == "1")
                {
                    Console.WriteLine(DataInterface.insertUserIT);
                    usernameAuthentication = Console.ReadLine();
                    Console.WriteLine(DataInterface.insertPswIT);
                }
                else
                {
                    Console.WriteLine(DataInterface.insertUserEN);
                    usernameAuthentication = Console.ReadLine();
                    Console.WriteLine(DataInterface.insertPswEN);
                }

                // Inserimento psw mascherata
                var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
                // Criptaggio Psw
                var authPwd = registration.EncryptPwd(passwordAuthentication);
                // confronto se esiste psw (Massimo 3 volte )
                var autentication = queryBuilder.GetUserIfExist(usernameAuthentication, authPwd);
                if (autentication != null)
                {
                    // Da il benvenuto e accede al menu Meteo
                    Console.WriteLine("\n");
                    if (lang == "1")
                    {
                        Console.WriteLine("Benvenuto" + " " + $"{usernameAuthentication}");
                    }
                    else
                    {
                        Console.WriteLine("Welcome" + " " + $"{usernameAuthentication}");
                    }
                    controlWhilePsw = 5;
                }
                else
                {
                    // Reinserimento Psw (massimo altri 2 tentativi)
                    if (controlWhilePsw < 3)
                    {
                        if (lang == "1")
                        {
                            Console.WriteLine($"\n{DataInterface.reinsertUserPswIT}");
                            Console.WriteLine($"{DataInterface.remainingAttemptsIT} {countAttempts}");
                        }
                        else
                        {
                            Console.WriteLine($"\n{DataInterface.reinsertUserPswEN}");
                            Console.WriteLine($"{DataInterface.remainingAttemptsEN} {countAttempts}");
                        }

                        controlWhilePsw++;
                    }




                    countAttempts--;

                    // al terzo tentativo da la possibilà di recuperare e modificare psw tramite domanda sicurezza precedentemente impostata
                    if (controlWhilePsw == 3)
                    {
                        // controllo while (esce qundo sbaglia 3 volte username e/o risposta sicurezza )



                        while (controlForUserIfExist < 4)
                        {

                            if (controlForUserIfExist == 3)
                            {
                                //termina sessione in caso in cui sbaglia tre volte a digitare psw e/o username
                                Environment.Exit(0);
                            }
                            if (lang == "1")
                            {
                                Console.WriteLine($"{DataInterface.secureQuestionIT}");
                            }
                            else
                            {

                                Console.WriteLine($"{DataInterface.secureQuestionEN}");
                            }

                            forAnswerInsertUsername = Console.ReadLine();
                            var userIfExist = queryBuilder.GetUser(forAnswerInsertUsername);
                            if (userIfExist != null)
                            {
                                var IdQuestionInTableUser = queryBuilder.GetUser(forAnswerInsertUsername).IdQuestion;
                                var question = queryBuilder.GetQuestion(IdQuestionInTableUser).DefaultQuestion;
                                if (question != null)
                                {
                                    controlForUserIfExist = 4;

                                    // inserimento Risposta mascherata 

                                    // Verifica se Risposta è corretta. Il risultato è dentro autentication 
                                    var answerWrong = true;
                                    while (answerWrong)
                                    {

                                        Console.WriteLine(question);

                                        var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                                        Console.WriteLine();
                                        // criptaggio della Risposta inserita 
                                        var insertAnswerForAccessEcrypted = registration.EncryptPwd(insertAnswerMaskered);
                                        autentication = queryBuilder.AutentiationWithAnswer(insertAnswerForAccessEcrypted, forAnswerInsertUsername);
                                        if (autentication != null)
                                        {


                                            if (lang == "1")
                                            {
                                                Console.WriteLine($"\n{DataInterface.newPswIT}");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"\n{DataInterface.newPswEN}");
                                            }
                                            var controlRequirementsNewPsw = true;

                                            while (controlRequirementsNewPsw)
                                            {
                                                // maschera nuova psw 
                                                var newPswMask = DataMaskManager.MaskData(newPswClear);
                                                // controlo su vincoli di sicurezza psw
                                                if (Helper.RegexForPsw(newPswMask) == false)
                                                {
                                                    if (lang == "1")
                                                    {
                                                        Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                                                        Console.WriteLine("\nReinserisci Password!");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
                                                        Console.WriteLine("\nReenter Password!");
                                                    }
                                                    countAttemptsPswRegister++;

                                                    // uscita in caso di 3 errori
                                                    if (countAttemptsPswRegister == 3)
                                                    {
                                                        if (lang == "1")
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
                                                else
                                                {
                                                    // criptaggio nuova psw 
                                                    var newPswEncrypted = registration.EncryptPwd(newPswMask);

                                                    // Aggiornamento psw in DB
                                                    queryBuilder.QueryForUpdatePsw(newPswEncrypted, forAnswerInsertUsername);
                                                    // Uscita dal while 
                                                    controlRequirementsNewPsw = false;
                                                    controlWhilePsw = 5;
                                                    usernameAuthentication = forAnswerInsertUsername;
                                                }



                                            }
                                            // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
                                            answerWrong = false;

                                        }
                                        attemptsAnswerForQuestion++;
                                        if (attemptsAnswerForQuestion == 3)
                                        {
                                            Environment.Exit(0);
                                        }
                                    }




                                }


                            }
                            else
                            {
                                controlForUserIfExist++;
                            }

                        }

                        // controllo se esiste domanda per user  
                    }



                }
            }
            // uscita Whie Primario
            controlWhilePsw++;
            return usernameAuthentication;

        }
    }
}

