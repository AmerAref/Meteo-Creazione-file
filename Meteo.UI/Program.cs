using System;
using System.Collections.Generic;
using System.Linq;
using Meteo.Services;
using Meteo.ExcelManager;
using Meteo.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using Meteo.Services.Models;
namespace Meteo.UI
{
    public static class Program
    {
        //static string choseCreateNewAccuoutOrLogin = "";

        private static Dictionary<string, string> InsertDataForEmail()
        {
            var dictionaryForEmail = new Dictionary<string, string>();
            var insertSender = "Inserisci email mittente";
            var insertReciver = "Inserisci email destinatario";
            var insertBody = "Iserisci testo all'interno dell'email";
            var insertSubject = "Inserisci oggetto";
            Console.WriteLine(insertSender);
            var sender = Console.ReadLine();
            dictionaryForEmail.Add("senderKey", sender);

            Console.WriteLine();

            Console.WriteLine(insertReciver);
            var receiver = Console.ReadLine();
            Console.WriteLine(insertBody);
            var body = Console.ReadLine();
            Console.WriteLine(insertSubject);
            var subject = Console.ReadLine();

            var user = sender.Split('@')[0];

            dictionaryForEmail.Add("receiverKey", receiver);
            dictionaryForEmail.Add("bodyKey", body);
            dictionaryForEmail.Add("subjectKey", subject);
            dictionaryForEmail.Add("userKey", user);
            Console.WriteLine("Inserisci password");

            return dictionaryForEmail;
        }

        static void Main(string[] args)
        {
            var connection = new DbFactoryManager();
            var meteoApi = new MeteoApi();
            //var login = new Login();
            var registration = new Register();
            var emailManager = new EmailManager();
            var filemenager = new FileMenager();
            var createXlsFile = new CreateXlsFile();
            var createXlsFromFile = new CreateXlsFromFiles();
            var menu = new Menu();
            var print = new PrintData();
            var databaseManager = new DatabaseManager();
            //var queryMng = new QueryManager();
            var choseConfigurationPc = new ChoseConfigurationPc();
            var dbfm = new DbFactoryManager();
            var filePath = "/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file";
            var exit = true;
            var validationUsername = true;
            var controlWhileAnswer = true;
            var selectQuestion = 0;
            var controlForUserIfExist = 0;
            var printQuestionForAccessAfterControl = "";
            var forAnswerInsertUsername = "";
            var password = "";
            var controlWhilePsw = 0;
            var countAttempts = 2;
            var lang = "";
            var passwordLogin = "";
            var answerToLogin = "";
            var passwordRegistration = "";
            var validationManagerPsw = true;
            var pswNewAccount = "";
            var countAttemptsPswRegister = 0;
            var insertAnswer = "";
            var usernameNewAccount = "";
            var usernameAuthentication = "";
            var IdQuestionForTableUser = 0;
            menu.MenuDatabaseManager();
            User user = new User();
            connection.OpenConnection();
            var choiceCreateDatabase = Console.ReadLine();
            if (choiceCreateDatabase == "1")
            {
                try
                {
                    databaseManager.CreateDatabaseIfNotExist();
                    Console.WriteLine("Database creato con successo");

                }
                catch 
                {
                    Console.WriteLine("Database non creato");
                }

            }
            if (choiceCreateDatabase == "2")
            {
                try 
                {
                    databaseManager.DeleteDatabase();
                    Console.WriteLine("Database creato con successo");

                }
                catch 
                {
                    Console.WriteLine("Eliminazione Database non riuscita");    
                }
            }
           
           

            //Console.WriteLine("Scegli la lingua/ Choose the language (it/en)");
            //lang = Console.ReadLine();
            //var controlFirstChoiceLogin = true;
            //while (controlFirstChoiceLogin)
            //{
            //    if (lang == "it")
            //    { menu.ShowMenuAuthenticationIT(); }
            //    else
            //    { menu.ShowMenuAuthenticationEN(); }

            //    choseCreateNewAccuoutOrLogin = Console.ReadLine();
            //    controlWhilePsw = 0;
            //    countAttempts = 2;
            //    //Caso scelta Login con utente già registrato su DB
            //    if (choseCreateNewAccuoutOrLogin == "1")
            //    {
            //        //Controllo Uscita dal ciclo While
            //        while (controlWhilePsw < 5)
            //        {
            //            // Inserimento User
            //            if (lang == "it")
            //            {
            //                Console.WriteLine(DataInterface.insertUserIT);
            //                usernameAuthentication = Console.ReadLine();
            //                Console.WriteLine(DataInterface.insertPswIT);
            //            }
            //            else
            //            {
            //                Console.WriteLine(DataInterface.insertUserEN);
            //                usernameAuthentication = Console.ReadLine();
            //                Console.WriteLine(DataInterface.insertPswEN);
            //            }

            //            // Inserimento psw mascherata 
            //            var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
            //            // Criptaggio Psw
            //            var authPwd = registration.EncryptPwd(passwordAuthentication);
            //            // confronto se esiste psw (Massimo 3 volte )
            //            var autentication = login.LoginAttempts(context, usernameAuthentication, authPwd);
            //            if (autentication.Any())
            //            {
            //                // Da il benvenuto e accede al menu Meteo
            //                Console.WriteLine("\n");
            //                if (lang == "it")
            //                { Console.WriteLine("Benvenuto" + " " + $"{usernameAuthentication}"); }
            //                else
            //                { Console.WriteLine("Welcome" + " " + $"{usernameAuthentication}"); }
            //                controlFirstChoiceLogin = false;
            //                controlWhilePsw = 5;
            //            }
            //            else
            //            {
            //                // Reinserimento Psw (massimo altri 2 tentativi)
            //                if (lang == "it")
            //                {
            //                    Console.WriteLine($"\n{DataInterface.reinsertUserPswIT}");
            //                    Console.WriteLine($"{DataInterface.remainingAttemptsIT} {countAttempts}");
            //                }
            //                else
            //                {
            //                    Console.WriteLine($"\n{DataInterface.reinsertUserPswEN}");
            //                    Console.WriteLine($"{DataInterface.remainingAttemptsEN} {countAttempts}");
            //                }

                                    if (controlForUserIfExist == 3)
                                    {
                                        //termina sessione in caso in cui sbaglia tre volte a digitare psw e/o username
                                        return;
                                    }
                                    if (lang == "it")
                                    {
                                        Console.WriteLine($"{DataInterface.secureQuestionIT}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{DataInterface.secureQuestionEN}");
                                    }
                                    forAnswerInsertUsername = Console.ReadLine();
                                    var reciveIDQuestion = 0;
                                    user = queryMng.GetUser(forAnswerInsertUsername);
                                    var userIfExist = user.Username;

                                    // controllo se esiste user 
                                    if (userIfExist != null)
                                    {
                                        var printQuestionForAccessIfExist = queryMng.GetQuestion(reciveIDQuestion, user);

                                        // controllo se esiste domanda per user 
                                        if (printQuestionForAccessIfExist != null)
                                        {
                                            // ricavo Domanda con stampa ed accesso a form per modifica psw 
                                            var defaultQuestion = queryMng.GetQuestion(reciveIDQuestion, user).DefaultQuestions;
                                            Console.WriteLine(printQuestionForAccessAfterControl);
                                            controlForUserIfExist = 4;
                                        }
                                    }
                                    controlForUserIfExist++;
                                }

            //                        // controllo se esiste user 
            //                        if (userIfExist != null)
            //                        {
            //                            var printQuestionForAccessIfExist = queryMng.QuestionExistance(context, reciveIDQuestion, userIfExist);

            //                            // controllo se esiste domanda per user 
            //                            if (printQuestionForAccessIfExist != null)
            //                            {
            //                                // ricavo Domanda con stampa ed accesso a form per modifica psw 
            //                                printQuestionForAccessAfterControl = queryMng.QuestionControl(context, reciveIDQuestion);
            //                                Console.WriteLine(printQuestionForAccessAfterControl);
            //                                controlForUserIfExist = 4;
            //                            }
            //                        }
            //                        controlForUserIfExist++;
            //                    }

            //                    // inserimento Risposta mascherata 
            //                    var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
            //                    // criptaggio della Risposta inserita 
            //                    var insertAnswerForAccessEcrypted = registration.EncryptPwd(insertAnswerMaskered);
            //                    autentication = login.ControlAnswer(insertAnswerForAccessEcrypted, context, forAnswerInsertUsername).ToList();
            //                    // Verifica se Risposta è corretta. Il risultato è dentro autentication 
            //                    if (autentication.Any())
            //                    {
            //                        //forech effettuato su l'unico elemeto all'interno della lista 
            //                        foreach (var tableUser in autentication)
            //                        {
            //                            var controlRequirementsNewPsw = true;
            //                            if (lang == "it")
            //                            { Console.WriteLine($"\n{DataInterface.newPswIT}"); }
            //                            else
            //                            { Console.WriteLine($"\n{DataInterface.newPswEN}"); }
            //                            // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
            //                            while (controlRequirementsNewPsw)
            //                            {
            //                                var newPswClear = "";
            //                                // maschera nuova psw 
            //                                var newPswMask = DataMaskManager.MaskData(newPswClear);
            //                                // controlo su vincoli di sicurezza psw
            //                                if (Helper.RegexForPsw(pswNewAccount) == false)
            //                                {
            //                                    if (lang == "it")
            //                                    {
            //                                        Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
            //                                        Console.WriteLine("\nReinserisci Password!");
            //                                    }
            //                                    else
            //                                    {
            //                                        Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
            //                                        Console.WriteLine("\nReenter Password!");
            //                                    }

            //                                    countAttemptsPswRegister++;

            //                                    // uscita in caso di 3 errori
            //                                    if (countAttemptsPswRegister == 3)
            //                                    {
            //                                        if (lang == "en")
            //                                        { Console.WriteLine("Mi dispiace, ma hai esaurito i tentativi!"); }
            //                                        else
            //                                        { Console.WriteLine("I'm sorry, but you've exhausted the attempts!"); }
            //                                        return;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    // criptaggio nuova psw 
            //                                    var newPswEncrypted = registration.EncryptPwd(newPswMask);

            //                                    // Aggiornamento psw in DB
            //                                    tableUser.Password = newPswEncrypted;
            //                                    // salvataggio aggiornamento nel DB 
            //                                    context.SaveChanges();
            //                                    controlWhilePsw = 5;
            //                                    // Uscita dal while 
            //                                    controlFirstChoiceLogin = false;
            //                                    controlRequirementsNewPsw = false;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            // uscita Whie Primario
            //            controlWhilePsw++;
            //        }
            //    }

            //    // Creazione nuovo utente 
            //    if (choseCreateNewAccuoutOrLogin == "2")
            //    {
            //        // Inserimento nome Registrazione
            //        if (lang == "it")
            //        { Console.WriteLine("Inserisci Nome"); }
            //        else
            //        { Console.WriteLine("Enter Name"); }
            //        var nameNewAccuont = Console.ReadLine();

            //        // Inserimento Cognome Registrazione
            //        if (lang == "it")
            //        { Console.WriteLine("Inserisci il Cognome"); }
            //        else
            //        { Console.WriteLine("Enter Surname"); }
            //        var surnameNewAccount = Console.ReadLine();
            //        // While Per validazione psw
            //        while (validationManagerPsw)
            //        {
            //            // While per controllo username (deve essere univoco sul DB)
            //            while (validationUsername)
            //            {
            //                if (lang == "it")
            //                { Console.WriteLine(DataInterface.insertUserIT); }
            //                else
            //                { Console.WriteLine(DataInterface.insertUserEN); }

            //                usernameNewAccount = Console.ReadLine();

                            countAttemptsPswRegister++;
                            // se l'utente non soddisfa i criteri per 3 volte termina la sessione
                            if (countAttemptsPswRegister == 3)
                            {
                                if (lang == "it")
                                { Console.WriteLine("Mi dispiace, ma hai esaurito i tentativi!"); }
                                else
                                { Console.WriteLine("I'm sorry, but you've exhausted the attempts!"); }
                                return;
                            }
                        }
                        // se inserisce psw secondo i criteri di sicurezza deve riscrivere la psw da confrontare con la precedente 
                        else
                        {
                            if (lang == "it")
                            { Console.WriteLine("\nReinserisci Password."); }
                            else
                            { Console.WriteLine("\nReenter Password."); }
                            // maschera reinserimento psw
                            var pswNewAccountComparison = DataMaskManager.MaskData(passwordRegistration);
                            // confronto fra le due psw scritte. Se corrispondo l'utente deve selezionare una domanda di sicurezza per recupero psw 
                            if (pswNewAccount == pswNewAccountComparison)
                            {
                                // contro su correttezza risposta (esce Quando è seleziona "S" per salvare la risposta alla domanda)
                                while (controlWhileAnswer)
                                {
                                    // menu per domande 
                                    menu.SelectQuestion(context);
                                    selectQuestion = Convert.ToInt32(Console.ReadLine());
                                    var questionSelected = context.Questions.ToList();
                                    var questionAutentication = context.Questions.Where(x => x.IdQuestion.Equals(selectQuestion)).ToList();
                                    foreach (var questionForUser in questionAutentication)
                                    {
                                        var questionPrint = questionForUser.DefaultQuestions;
                                        Console.WriteLine(questionPrint);
                                        IdQuestionForTableUser = questionForUser.IdQuestion;
                                    }
                                    // stampa risposta inserita
                                    if (lang == "it")
                                    {
                                        Console.WriteLine("Inserisci risposta di sicurezza");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Insert security answer");
                                    }

            //            pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
            //            // Controlla se Accetta i criteri di sicurezza psw
            //            if (Helper.RegexForPsw(pswNewAccount) == false)
            //            {
            //                if (lang == "it")
            //                {
            //                    Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire almeno 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
            //                    Console.WriteLine("\nReinserisci Password.");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("\nThe security criteria are not met (Enter at least 1 capital letter, 1 number, 1 special character. The length must be greater than or equal to 8)");
            //                    Console.WriteLine("\nReenter Password.");
            //                }

                                    // digita "S" Esce da While altrimenti ti fa reinserire
                                    if (controlAnswer == "S")
                                    {
                                        controlWhileAnswer = false;
                                    }
                                }
                                // criptaggio di Psw e Risposta 
                                var encryptedPwd = registration.EncryptPwd(pswNewAccount);
                                var encryptedAnswer = registration.EncryptPwd(insertAnswer);
                                var unitOfMeasureNewAccount = "";
                                if (lang == "it")
                                {
                                    Console.WriteLine("Inserisci la lingua");
                                    unitOfMeasureNewAccount = "metric";
                                }
                                else
                                {
                                    Console.WriteLine("Enter the language");
                                    unitOfMeasureNewAccount = "imperial";
                                }
                                var languageNewAccount = Console.ReadLine();

                                context.Users.Add(
                                    new User
                                    {
                                        Password = encryptedPwd,
                                        Username = usernameNewAccount,
                                        Surname = surnameNewAccount,
                                        Name = nameNewAccuont,
                                        IdQuestion = IdQuestionForTableUser,
                                        Answer = encryptedAnswer,
                                        Language = languageNewAccount,
                                        UnitOfMeasure = unitOfMeasureNewAccount
                                    }
                                );
                                controlFirstChoiceLogin = false;
                                validationManagerPsw = false;
                                if (lang == "it")
                                {
                                    Console.WriteLine($"\nBenvenuto utente: {usernameNewAccount}");
                                }
                                else
                                {
                                    Console.WriteLine($"\nWelcome user: {usernameNewAccount}");
                                }
                            }
                            else
                            {
                                if (lang == "it")
                                {
                                    Console.WriteLine($"\nLe due password inserite non corrispondono! {DataInterface.reinsertUserPswIT}");
                                }
                                else
                                {
                                    Console.WriteLine($"\nThe two entered passwords don't match! {DataInterface.reinsertUserPswIT}");
                                }
                            }
                        }
                    }
                }
            }

            //                        // digita "S" Esce da While altrimenti ti fa reinserire
            //                        if (controlAnswer == "S")
            //                        {
            //                            controlWhileAnswer = false;
            //                        }
            //                    }
            //                    // criptaggio di Psw e Risposta 
            //                    var encryptedPwd = registration.EncryptPwd(pswNewAccount);
            //                    var encryptedAnswer = registration.EncryptPwd(insertAnswer);
            //                    if (lang == "it")
            //                    {
            //                        Console.WriteLine("Inserisci la lingua");
            //                    }
            //                    else
            //                    {
            //                        Console.WriteLine("Enter the language");
            //                    }
            //                    var languageNewAccount = Console.ReadLine();

            //                    context.Users.Add(
            //                        new User
            //                        {
            //                            Password = encryptedPwd,
            //                            Username = usernameNewAccount,
            //                            Surname = surnameNewAccount,
            //                            Name = nameNewAccuont,
            //                            IdQuestion = IdQuestionForTableUser,
            //                            Answer = encryptedAnswer,
            //                            Language = languageNewAccount,
            //                            IdRole = roleSelected
            //                        }
            //                    );
            //                    controlFirstChoiceLogin = false;
            //                    validationManagerPsw = false;
            //                    if (lang == "it")
            //                    { 
            //                        Console.WriteLine($"\nBenvenuto utente: {usernameNewAccount}"); 
            //                    }
            //                    else
            //                    { 
            //                        Console.WriteLine($"\nWelcome user: {usernameNewAccount}"); 
            //                    }
            //                }
            //                else
            //                {
            //                    if (lang == "it")
            //                    { 
            //                        Console.WriteLine($"\nLe due password inserite non corrispondono! {DataInterface.reinsertUserPswIT}");
            //                    }
            //                    else
            //                    { 
            //                        Console.WriteLine($"\nThe two entered passwords don't match! {DataInterface.reinsertUserPswIT}"); 
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //// salvataggio modifiche su DB 
            //context.SaveChanges();
            //connection.CloseConnection();

            //while (exit)
            //{
            //var menuLang = "";
            //connection.OpenConnection();
            //if (choseCreateNewAccuoutOrLogin == "1")
            //{
            //    menuLang = context.Users.SingleOrDefault(x => x.Username.Equals(usernameAuthentication)).Language;
            //    Console.WriteLine(menuLang);
            //}
            //else
            //{
            //    menuLang = context.Users.SingleOrDefault(x => x.Username.Equals(usernameNewAccount)).Language;
            //    Console.WriteLine(menuLang);
            //}

            //connection.CloseConnection();
            var menuLang = "it";
            menu.ShowFirst();
            var sceltaPrimaria = Console.ReadLine();
            switch (sceltaPrimaria)
            {
                var menuLang = "";
                var measureUnit = "";
                connection.OpenConnection();
                if (choseCreateNewAccuoutOrLogin == "1")
                {
                    user = queryMng.GetUser(usernameAuthentication);
                    menuLang = user.Language;
                    measureUnit = user.UnitOfMeasure;
                }
                else
                {
                    user = queryMng.GetUser(usernameNewAccount);
                    menuLang = user.Language;
                    measureUnit = user.UnitOfMeasure;
                }

                connection.CloseConnection();
                menu.ShowFirst();
                var sceltaPrimaria = Console.ReadLine();
                switch (sceltaPrimaria)
                {
                    case "1":
                        menu.ShowMenu();
                        var choseThisDay = Console.ReadLine();
                        switch (choseThisDay)
                        {
                            case "1":
                                if (menuLang == "it")
                                { Console.WriteLine(DataInterface.successIT); }
                                else
                                { Console.WriteLine(DataInterface.insertNamePlaceEN); }
                                var place = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnit).Result;
                                    print.PrintForData(jsonObj);
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.insertNameFileIT); }
                                    else
                                    { Console.WriteLine(DataInterface.insertNameFileEN); }
                                    var fileName = Console.ReadLine();
                                    var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                    var file = filemenager.CreateNewFile(fileName, jsonStr);
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.choiceSendEmailIT);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(DataInterface.successCreateFileIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                    }


                                    if (choiceSelected == "S")
                                    {
                                        var dataForEmail = Program.InsertDataForEmail();
                                        var senderValue = dataForEmail["senderKey"];
                                        var receiverValue = dataForEmail["receiverKey"];
                                        var bodyValue = dataForEmail["bodyKey"];
                                        var subjectValue = dataForEmail["subjectKey"];
                                        var userValue = dataForEmail["userKey"];
                                        emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                    }
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileIT); }
                                    else
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileEN); }
                                    choiceSelected = Console.ReadLine();

                                    if (choiceSelected == "S")
                                    {
                                        createXlsFile.CreateXlsFileForToday(fileName, jsonObj, place);
                                    }

                                    else
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(DataInterface.successIT); }
                                        else
                                        { Console.WriteLine(DataInterface.successEN); }
                                    }
                                }
                                else
                                {
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                }
                                break;
                            case "2":
                                string lon, lat;
                                if (menuLang == "it")
                                { Console.WriteLine("ERRORE!"); }
                                else
                                { Console.WriteLine("ERROR!"); }
                            }
                            break;
                        case "2":
                            string lon, lat, measureUnitCoordinates;
                            if (menuLang == "it")
                            {
                                Console.WriteLine(DataInterface.insertLatIT);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonIT);
                                lon = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitIT);
                                measureUnitCoordinates = Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertLatEN);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonEN);
                                lon = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitEN);
                                measureUnitCoordinates = Console.ReadLine();
                            }
                            try
                            {
                                var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnitCoordinates).Result;
                                print.PrintForData(jsonObj);
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.insertLatIT);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(DataInterface.insertLonIT);
                                    lon = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.insertLatEN);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(DataInterface.insertLonEN);
                                    lon = Console.ReadLine();
                                }
                                var choiceSelected = Console.ReadLine();
                                if (choiceSelected == "S")
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnit).Result;
                                    print.PrintForData(jsonObj);
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileIT);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.successCreateFileIT);
                                        Console.WriteLine(DataInterface.choiceSendEmailIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    choiceSelected = Console.ReadLine();

                                    if (choiceSelected == "S")
                                    {
                                        var dataForEmail = Program.InsertDataForEmail();
                                        var senderValue = dataForEmail["senderKey"];
                                        var receiverValue = dataForEmail["receiverKey"];
                                        var bodyValue = dataForEmail["bodyKey"];
                                        var subjectValue = dataForEmail["subjectKey"];
                                        var userValue = dataForEmail["userKey"];
                                        emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                    }
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileIT); }
                                    else
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileEN); }
                                    choiceSelected = Console.ReadLine();

                                    if (choiceSelected == "S")
                                    {
                                        createXlsFile.CreateXlsFileForTodayByCoordinates(fileName, jsonObj, lat, lon);
                                    }
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                }
                                else
                                {
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                }
                            }
                            catch
                            {
                                if (menuLang == "it")
                                { Console.WriteLine("ERRORE!"); }
                                else
                                { Console.WriteLine("ERROR!"); }
                            }
                            break;
                        case "3":
                            break;
                    }
                    break;
                case "2":
                    menu.ShowMenu();
                    var choseLast5Day = Console.ReadLine();
                    switch (choseLast5Day)
                    {
                        case "1":
                            string place;
                            if (menuLang == "it")
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceIT);
                                place = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceEN);
                                place = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitIT);
                            }
                            var measureUnitFiveDays = Console.ReadLine();
                            try
                            {
                                var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnitFiveDays).Result;
                                print.PrintDataLast5Day(jsonObj);
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.insertNamePlaceIT);
                                    place = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.insertNamePlaceEN);
                                    place = Console.ReadLine();
                                }
                                var measureUnitFiveDays = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnit).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileIT);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.choiceSendEmailIT);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(DataInterface.successCreateFileIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                    }

                                    if (choiceSelected == "S")
                                    {
                                        var dataForEmail = Program.InsertDataForEmail();
                                        var senderValue = dataForEmail["senderKey"];
                                        var receiverValue = dataForEmail["receiverKey"];
                                        var bodyValue = dataForEmail["bodyKey"];
                                        var subjectValue = dataForEmail["subjectKey"];
                                        var userValue = dataForEmail["userKey"];
                                        emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                    }
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileIT); }
                                    else
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileEN); }
                                    choiceSelected = Console.ReadLine();

                                    if (choiceSelected == "S")
                                    {
                                        createXlsFile.CreateXlsFileForLast5Days(fileName, jsonObj, place);
                                    }
                                }
                                else
                                {
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                }
                            }
                            catch
                            {
                                if (menuLang == "it")
                                { Console.WriteLine("ERRORE!"); }
                                else
                                { Console.WriteLine("ERROR!"); }
                            }
                            break;
                        case "2":
                            string lat, lon;
                            if (menuLang == "it")
                            {
                                Console.WriteLine(DataInterface.insertLatIT);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonIT);
                                lon = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertLatEN);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonEN);
                                lon = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertMeasureUnitEN);
                            }
                            var measureUnitCoordinatesFiveDays = Console.ReadLine();
                            try
                            {
                                var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnitCoordinatesFiveDays).Result;
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.insertLatIT);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(DataInterface.insertLonIT);
                                    lon = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.insertLatEN);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(DataInterface.insertLonEN);
                                    lon = Console.ReadLine();
                                }
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnit).Result;
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileIT);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.choiceSendEmailIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = Console.ReadLine();
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    choiceSelected = Console.ReadLine();

                                    if (choiceSelected == "S")
                                    {
                                        var dataForEmail = Program.InsertDataForEmail();
                                        var senderValue = dataForEmail["senderKey"];
                                        var receiverValue = dataForEmail["receiverKey"];
                                        var bodyValue = dataForEmail["bodyKey"];
                                        var subjectValue = dataForEmail["subjectKey"];
                                        var userValue = dataForEmail["userKey"];
                                        emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                    }

                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileIT); }
                                    else
                                    { Console.WriteLine(DataInterface.choiceCreateXlsFileEN); }
                                    choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        createXlsFile.CreateXlsFileForLast5DaysByCoordinates(fileName, jsonObj, lat, lon);
                                    }
                                }
                                else
                                {
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                }
                            }
                            catch
                            {
                                if (menuLang == "it")
                                { Console.WriteLine("ERRORE!"); }
                                else
                                { Console.WriteLine("ERROR!"); }
                            }
                            break;
                        case "3":
                            break;
                        case "4":
                            return;
                    }
                    break;
                case "3":
                    menu.ShowFiltredMenu();
                    var choseFilter = Console.ReadLine();
                    switch (choseFilter)
                    {
                        case "1":
                            if (menuLang == "it")
                            { Console.WriteLine(DataInterface.insertNamePlaceIT); }
                            else
                            { Console.WriteLine(DataInterface.insertNamePlaceEN); }
                            var place = Console.ReadLine();
                            if (menuLang == "it")
                            { Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni"); }
                            else
                            { Console.WriteLine("Enter value of required humidity for the last 5 days"); }
                            var humidity = Console.ReadLine();
                            try
                            {
                                meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Wait();
                                if (menuLang == "it")
                                { Console.WriteLine(DataInterface.successIT); }
                                else
                                { Console.WriteLine(DataInterface.successEN); }
                            }
                            catch
                            {
                                if (menuLang == "it")
                                { Console.WriteLine("ERRORE!"); }
                                else
                                { Console.WriteLine("ERROR!"); }
                            }
                            break;
                        case "2":
                            if (menuLang == "it")
                            { Console.WriteLine(DataInterface.insertNamePlaceIT); }
                            else
                            { Console.WriteLine(DataInterface.insertNamePlaceEN); }
                            place = Console.ReadLine();
                            string date, time;
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
                                date = Console.ReadLine();
                                Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
                            }
                            else
                            {
                                Console.WriteLine("Enter date with the following format YYYY-mm-GG");
                                date = Console.ReadLine();
                                Console.WriteLine("Enter time with the following format HH:MM:SS");
                            }
                            time = Console.ReadLine();
                            meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Wait();
                            if (menuLang == "it")
                            { Console.WriteLine(DataInterface.successIT); }
                            else
                            { Console.WriteLine(DataInterface.successEN); }
                            break;
                        case "3":
                            if (menuLang == "it")
                            { Console.WriteLine(DataInterface.insertNamePlaceIT); }
                            else
                            { Console.WriteLine(DataInterface.insertNamePlaceEN); }
                            place = Console.ReadLine();
                            if (menuLang == "it")
                            { Console.WriteLine("Iserisci tipologia di tempo richiesta"); }
                            else
                            { Console.WriteLine("Enter the type of requested weather"); }
                            var typeWeather = Console.ReadLine();
                            meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place).Wait();
                            if (menuLang == "it")
                            { Console.WriteLine(DataInterface.successIT); }
                            else
                            { Console.WriteLine(DataInterface.successEN); }
                            break;
                        case "4":
                            break;
                    }
                    break;
                case "4":
                    if (menuLang == "it")
                    { Console.WriteLine("Inserisci nome file da eliminare, con tipo di estensione (nomefile.estensione)"); }
                    else
                    { Console.WriteLine("Enter the name of the file to be deleted, with the extension (filename.extension)"); }
                    var fileNameDelete = Console.ReadLine();
                    filemenager.DeleteFile(fileNameDelete);
                    break;
                case "5":
                    if (menuLang == "it")
                    { Console.WriteLine("Inserisci il nome file da inviare tramite email"); }
                    else
                    { Console.WriteLine("Enter the file name to be sent via email"); }
                    var fileNameToSendAnyFile = Console.ReadLine();
                    var dataForEmailAnyFile = Program.InsertDataForEmail();
                    var senderValueAnyFile = dataForEmailAnyFile["senderKey"];
                    var receiverValueAnyFile = dataForEmailAnyFile["receiverKey"];
                    var bodyValueAnyFile = dataForEmailAnyFile["bodyKey"];
                    var subjectValueAnyFile = dataForEmailAnyFile["subjectKey"];
                    var userValueAnyFile = dataForEmailAnyFile["userKey"];
                    emailManager.AttempsPasswordAndSendEmail(fileNameToSendAnyFile, senderValueAnyFile, receiverValueAnyFile, bodyValueAnyFile, subjectValueAnyFile, userValueAnyFile, password);
                    break;
                case "6":
                    menu.ShowMenuCreateXlsFile();
                    var choiceXls = Console.ReadLine();
                    switch (choiceXls)
                    {
                        case "1":
                            if (menuLang == "it")
                            { Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati"); }
                            else
                            { Console.WriteLine("Enter the name of the file from which to get the data"); }
                            var todaySourceFile = Console.ReadLine();
                            var todayFilePath = Path.Combine(filePath, todaySourceFile);
                            if (menuLang == "it")
                            { Console.WriteLine("Inserisci il nome del file XLS"); }
                            else
                            { Console.WriteLine("Enter the name of the XLS file"); }
                            var todayXlsFile = Console.ReadLine();

                            createXlsFromFile.CreateXlsFromFileForToday(todayFilePath, todayXlsFile);
                            break;
                        case "2":
                            if (menuLang == "it")
                            { Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati"); }
                            else
                            { Console.WriteLine("Enter the name of the file from which to get the data"); }
                            var fiveDaysSourceFile = Console.ReadLine();
                            var fiveDaysFilePath = Path.Combine(filePath, fiveDaysSourceFile);
                            if (menuLang == "it")
                            { Console.WriteLine("Inserisci il nome del file XLS"); }
                            else
                            { Console.WriteLine("Enter the name of the XLS file"); }
                            var fiveDaysXlsFile = Console.ReadLine();
                            createXlsFromFile.CreateXlsFromFileFor5Days(fiveDaysFilePath, fiveDaysXlsFile);
                            break;
                        case "3":
                            menu.ShowFirst();
                            break;
                        case "4":
                            exit = false;
                            if (menuLang == "it")
                            { Console.WriteLine("Sessione terminata"); }
                            else
                            { Console.WriteLine("Session ended"); }
                            break;
                    }
                    break;
                case "7":
                    exit = false;
                    if (menuLang == "it")
                    { Console.WriteLine("Sessione terminata"); }
                    else
                    { Console.WriteLine("Session ended"); }
                    break;
            }
        }
    }
}
