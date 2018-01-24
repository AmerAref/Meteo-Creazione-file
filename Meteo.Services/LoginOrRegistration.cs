using System;
namespace Meteo.Services
{
    public class LoginOrRegistration
    {
        public LoginOrRegistration()
        {
            menu.SelectLanguageStart();
            lang = Console.ReadLine();

            switch (lang)
            {
                case "1":
                    menu.ShowMenuAuthenticationIT();
                    break;
                case "2":
                    menu.ShowMenuAuthenticationEN();
                    break;
            }

            var controlFirstChoiceLogin = true;
            while (controlFirstChoiceLogin)
            {
                choseCreateNewAccuoutOrLogin = Console.ReadLine();
                controlWhilePsw = 0;
                countAttempts = 2;
                //Caso scelta Login con utente già registrato su DB
                if (choseCreateNewAccuoutOrLogin == "1")
                {
                    //Controllo Uscita dal ciclo While
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
                            controlFirstChoiceLogin = false;
                            controlWhilePsw = 5;
                        }
                        else
                        {
                            // Reinserimento Psw (massimo altri 2 tentativi)
                            if (controlWhilePsw < 2)
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

                            }




                            countAttempts--;

                            // al terzo tentativo da la possibilà di recuperare e modificare psw tramite domanda sicurezza precedentemente impostata
                            if (controlWhilePsw == 2)
                            {
                                // controllo while (esce qundo sbaglia 3 volte username e/o risposta sicurezza )



                                while (controlForUserIfExist < 4)
                                {

                                    if (controlForUserIfExist == 3)
                                    {
                                        //termina sessione in caso in cui sbaglia tre volte a digitare psw e/o username
                                        return;
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
                                                    // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
                                                    answerWrong = false;

                                                }
                                                attemptsAnswerForQuestion++;
                                                if (attemptsAnswerForQuestion == 4)
                                                {
                                                    return;
                                                }
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
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    // criptaggio nuova psw 
                                                    var newPswEncrypted = registration.EncryptPwd(newPswMask);

                                                    // Aggiornamento psw in DB
                                                    queryBuilder.QueryForUpdatePsw(newPswEncrypted, forAnswerInsertUsername);
                                                    // Uscita dal while 
                                                    controlFirstChoiceLogin = false;
                                                    controlRequirementsNewPsw = false;
                                                    controlWhilePsw = 5;
                                                    usernameAuthentication = forAnswerInsertUsername;
                                                }



                                            }
                                        }




                                    }
                                    // controllo se esiste domanda per user  
                                }



                            }
                        }
                        // uscita Whie Primario
                        controlWhilePsw++;
                    }
                }

                // Creazione nuovo utente 
                if (choseCreateNewAccuoutOrLogin == "2")
                {
                    // Inserimento nome Registrazione
                    if (lang == "1")
                    {
                        Console.WriteLine("Inserisci Nome");
                    }
                    else
                    {
                        Console.WriteLine("Enter Name");
                    }
                    var nameNewAccuont = Console.ReadLine();

                    // Inserimento Cognome Registrazione
                    if (lang == "1")
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
                        // While per controllo username (deve essere univoco sul DB)
                        while (validationUsername)
                        {
                            if (lang == "1")
                            {
                                Console.WriteLine(DataInterface.insertUserIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertUserEN);
                            }

                            usernameNewAccount = Console.ReadLine();

                            var autentication = queryBuilder.GetUser(usernameNewAccount);
                            // autentication è vuota nel caso in cui non esiste un user con stesso username
                            if (autentication != null)
                            {
                                // non ti fa uscire da while (reinserisci Username)
                                if (lang == "1")
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
                        if (lang == "1")
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
                            if (lang == "1")
                            {
                                Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                                Console.WriteLine("\nReinserisci Password.");
                            }
                            else
                            {
                                Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
                                Console.WriteLine("\nReenter Password.");
                            }

                            countAttemptsPswRegister++;
                            // se l'utente non soddisfa i criteri per 3 volte termina la sessione
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
                                return;
                            }
                        }
                        // se inserisce psw secondo i criteri di sicurezza deve riscrivere la psw da confrontare con la precedente 
                        else
                        {
                            if (lang == "1")
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
                                // contro su correttezza risposta (esce Quando è seleziona "1" per salvare la risposta alla domanda)
                                while (controlWhileAnswer)
                                {
                                    if (lang == "1")
                                    {
                                        menu.SelectQuestionIT();
                                    }
                                    else
                                    {
                                        menu.SelectQuestionEN();
                                    }
                                    // menu per domande 

                                    // stampa risposta inserita
                                    IdSelectedForQuestion = Convert.ToInt32(Console.ReadLine());
                                    if (lang == "1")
                                    {
                                        Console.WriteLine("Inserisci risposta di sicurezza");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Insert security answer");
                                    }
                                    // stampa risposta inserita 
                                    var questionSelected = queryBuilder.GetQuestion(IdSelectedForQuestion);
                                    Console.WriteLine(questionSelected.DefaultQuestion);
                                    // conferma rispost inserita 
                                    insertAnswer = Console.ReadLine();
                                    if (lang == "1")
                                    {
                                        Console.WriteLine("La risposta richiesta è la seguente? ");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Is the following the required answer?");
                                    }
                                    Console.WriteLine(insertAnswer);

                                    if (lang == "1")
                                    {
                                        menu.ConfirmationIT();
                                    }
                                    else
                                    {
                                        menu.ConfirmationEN();
                                    }

                                    var controlAnswer = Console.ReadLine();

                                    // digita "1" Esce da While altrimenti ti fa reinserire
                                    if (controlAnswer == "1")
                                    {
                                        controlWhileAnswer = false;
                                    }
                                }
                                // criptaggio di Psw e Risposta 
                                var encryptedPwd = registration.EncryptPwd(pswNewAccount);
                                var encryptedAnswer = registration.EncryptPwd(insertAnswer);
                                var selectLanguage = "";
                                var languageNewAccount = "";
                                int roleNewAccount = 2;
                                if (lang == "1")
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
                                controlFirstChoiceLogin = false;
                                validationManagerPsw = false;
                                if (lang == "1")
                                { Console.WriteLine($"\nBenvenuto utente: {usernameNewAccount}"); }
                                else
                                { Console.WriteLine($"\nWelcome user: {usernameNewAccount}"); }
                            }
                            else
                            {
                                if (lang == "1")
                                { Console.WriteLine($"\nLe due password inserite non corrispondono! {DataInterface.reinsertUserPswIT}"); }
                                else
                                { Console.WriteLine($"\nThe two entered passwords don't match! {DataInterface.reinsertUserPswEN}"); }
                            }
                        }
                    }
                }
            }

            // salvataggio modifiche su DB 


        }
    }
}
