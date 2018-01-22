using System;
using System.Collections.Generic;
using Meteo.Services;
using Meteo.ExcelManager;
using Meteo.Services.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Meteo.Services.Models;
using Ninject;

namespace Meteo.UI
{
    public static class Program
    {
        static string choseCreateNewAccuoutOrLogin = "";

        private static Dictionary<string, string> InsertDataForEmail(string lang)
        {
            string insertSender = "", insertReciver = "", insertBody = "", insertSubject = "";
            var dictionaryForEmail = new Dictionary<string, string>();
            if (lang == "it")
            {
                insertSender = "Inserisci email del mittente";
                insertReciver = "Inserisci email del destinatario";
                insertBody = "Iserisci il testo all'interno dell'email";
                insertSubject = "Inserisci l'oggetto";
            }
            else
            {
                insertSender = "Insert sender's email";
                insertReciver = "Insert recipient'semail";
                insertBody = "Isert the body text of the email";
                insertSubject = "Insert the object";
            }
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
            if (lang == "it")
            { Console.WriteLine("Inserisci password"); }
            else
            { Console.WriteLine("Insert password"); }

            return dictionaryForEmail;
        }

        static void Main(string[] args)
        {
            var newPswClear = "";
            var menuLang = "";
            var measureUnit = "";
            var registration = new Register();
            var emailManager = new EmailManager();
            var filemenager = new FileMenager();
            var createXlsFile = new CreateXlsFile();
            var createXlsFromFile = new CreateXlsFromFiles();
            var menu = new Menu();
            var print = new PrintData();
            var queryMng = new QueryManager();
            var exit = true;
            var meteoApi = new MeteoApi();
            var IdSelectedForQuestion = 0;
            var validationUsername = true;
            var controlWhileAnswer = true;
            var controlForUserIfExist = 0;
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
            var connection = new DbFactoryManager();
            var usernameAuthentication = "";
            var roleChoiceSelect = "";
            var meteoChoiceForDB = "";
            string userRole = "";
            var usernameForQuery = "";
            var user = new User();
            string formatForFile = "yyyy-MM-dd";
            string str = "";

            menu.SelectLanguageStart();
            lang = Console.ReadLine();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetAssembly(typeof(MySqlManager)));
            var queryBuilder = kernel.Get<IQueryBuilder>();
            var manager = new MySqlManager(queryBuilder);

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
                        var autentication = queryMng.GetUserIfExist(usernameAuthentication, authPwd);
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
                                    var userIfExist = queryMng.GetUser(forAnswerInsertUsername);
                                    if (userIfExist != null)
                                    {
                                        var IdQuestionInTableUser = queryMng.GetUser(forAnswerInsertUsername).IdQuestion;
                                        var question = queryMng.GetQuestion(IdQuestionInTableUser).DefaultQuestion;

                                        Console.WriteLine(question);
                                        controlForUserIfExist = 4;

                                    }
                                    // controllo se esiste domanda per user  
                                    controlForUserIfExist++;
                                }

                                // inserimento Risposta mascherata 
                                var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                                // criptaggio della Risposta inserita 
                                var insertAnswerForAccessEcrypted = registration.EncryptPwd(insertAnswerMaskered);
                                // Verifica se Risposta è corretta. Il risultato è dentro autentication 
                                autentication = queryMng.AutentiationWithAnswer(insertAnswerForAccessEcrypted, forAnswerInsertUsername);
                                if (autentication != null)
                                {

                                    var controlRequirementsNewPsw = true;
                                    if (lang == "1")
                                    {
                                        Console.WriteLine($"\n{DataInterface.newPswIT}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\n{DataInterface.newPswEN}");
                                    }
                                    // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
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
                                            queryMng.QueryForUpdatePsw(newPswEncrypted, forAnswerInsertUsername);
                                            // Uscita dal while 
                                            controlFirstChoiceLogin = false;
                                            controlRequirementsNewPsw = false;
                                        }
                                    }
                                }
                            }
                        }
                        // uscita Whie Primario
                        controlWhilePsw++; ;
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

                            var autentication = queryMng.GetUser(usernameNewAccount);
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
                                    var questionSelected = queryMng.GetQuestion(IdSelectedForQuestion);
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
                                    menu.SelectRoleEN();
                                    roleNewAccount = Convert.ToInt32(Console.ReadLine());
                                }

                                queryMng.InsertNewUser(encryptedPwd, usernameNewAccount, surnameNewAccount, nameNewAccuont, IdSelectedForQuestion, encryptedAnswer, languageNewAccount, measureUnit, roleNewAccount);
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

            while (exit)
            {
                if (choseCreateNewAccuoutOrLogin == "1")
                {
                    user = queryMng.GetUser(usernameAuthentication);
                    menuLang = user.Language;
                    measureUnit = user.UnitOfMeasure;
                    userRole = user.IdRole.ToString();
                    usernameForQuery = usernameAuthentication;
                }
                else
                {
                    user = queryMng.GetUser(usernameNewAccount);
                    menuLang = user.Language;
                    measureUnit = user.UnitOfMeasure;
                    userRole = user.IdRole.ToString(); ;
                    usernameForQuery = usernameNewAccount;
                }
                if (userRole == "1")
                {
                    if (menuLang == "it")
                    { menu.ShowFirtsMenuAdminIT(); }
                    else
                    { menu.ShowFirtsMenuAdminEN(); }
                    roleChoiceSelect = Console.ReadLine();
                    switch (roleChoiceSelect)
                    {
                        case "1":
                            var allUsers = queryMng.GetAllUsers();
                            print.PrintAllUsers(allUsers);
                            break;

                        case "2":
                            var allMasterRecords = queryMng.GetAllMasterRecords();
                            print.PrintAllMasterRecords(allMasterRecords);
                            break;

                        case "3":
                            if (menuLang == "it")
                            { menu.ShowSecondMenuAdminIT(); }
                            else
                            { menu.ShowSecondMenuAdminEN(); }
                            var secondAdminChoice = Console.ReadLine();
                            switch (secondAdminChoice)
                            {
                                case "1":
                                    string nameDelete = "", surnameDelete = "";
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine("Inserisci il nome dell'utente da eliminare");
                                        nameDelete = Console.ReadLine();
                                        Console.WriteLine("Inserisci il cognome dell'utente da elinimare");
                                        surnameDelete = Console.ReadLine();
                                        Console.WriteLine("Inserisci l'username dell'utente da eliminare");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Insert the name of the user to delete");
                                        nameDelete = Console.ReadLine();
                                        Console.WriteLine("Insert the surname of the user to delete");
                                        surnameDelete = Console.ReadLine();
                                        Console.WriteLine("Insert the username of the user to delete");
                                    }
                                    var usernameDelete = Console.ReadLine();
                                    queryMng.DeleteUser(nameDelete, surnameDelete, usernameDelete);
                                    break;
                                case "2":
                                    var pswModify = "";
                                    var pswModifyCompare = "";
                                    var pswModifyCount = 0;
                                    if (menuLang == "it")
                                    { Console.WriteLine("Inserisci l'username dell'utente da modificare"); }
                                    else
                                    { Console.WriteLine("Insert the username of the user to modify"); }
                                    var usernameModify = Console.ReadLine();
                                    while (pswModifyCount < 3)
                                    {
                                        var pswModifyRegex = "";
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine("Inserisci la nuova password dell'utente");
                                            pswModifyRegex = DataMaskManager.MaskData(pswModify);
                                            Console.WriteLine("\nReinserisci la nuova password dell'utente");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Insert the new password of the user");
                                            pswModifyRegex = DataMaskManager.MaskData(pswModify);
                                            Console.WriteLine("\nReenter the new password of the users");
                                        }
                                        var pswModifyCompareRegex = DataMaskManager.MaskData(pswModifyCompare);
                                        if (pswModifyRegex == pswModifyCompareRegex)
                                        {
                                            var pswModifyCrypto = registration.EncryptPwd(pswModifyRegex);
                                            queryMng.QueryForUpdatePsw(pswModifyCrypto, usernameModify);
                                            pswModifyCount = 3;
                                        }
                                        else
                                        {
                                            if (menuLang == "it")
                                            { Console.WriteLine($"\nLe due password non combaciano! Hai ancora {pswModifyCount} tentativi."); }
                                            else
                                            { Console.WriteLine($"\nThe two passwords do not match! You still have {pswModifyCount} attempts."); }
                                            pswModifyCount++;
                                            if (pswModifyCount == 3)
                                            {
                                                if (menuLang == "it")
                                                { Console.WriteLine("\nMi dispiace, ma hai esaurito i tentativi!\n"); }
                                                else
                                                { Console.WriteLine("\n I'm sorry, but you have exhausted the attempts!\n"); }
                                            }
                                        }
                                    }
                                    break;
                                case "3":
                                    if (menuLang == "it")
                                    { Console.WriteLine("Inserisci l'username dell'utente da modificare"); }
                                    else
                                    { Console.WriteLine("Insert the username of the user to modify"); }
                                    var usernameRoleModify = Console.ReadLine();
                                    menu.SelectRoleIT();
                                    var roleModify = Convert.ToInt32(Console.ReadLine());
                                    queryMng.QueryForUpdateRole(usernameRoleModify, roleModify);
                                    break;
                                case "4":
                                    break;
                                case "5":
                                    exit = false;
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine("Sessione terminata");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Session ended");
                                    }
                                    break;
                            }
                            break;
                        case "4":
                            exit = false;
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Sessione terminata");
                            }
                            else
                            {
                                Console.WriteLine("Session ended");
                            }
                            break;
                    }
                }
                else
                {
                    if (menuLang == "it")
                    {
                        menu.ShowFirstIT();
                    }
                    else
                    {
                        menu.ShowFirstEN();
                    }

                    var idUserMaster = queryMng.GetUser(usernameForQuery).IdUser;
                    var sceltaPrimaria = Console.ReadLine();
                    switch (sceltaPrimaria)
                    {
                        //case 1 del primo switch è relativo alle previsioni ad un giorno  
                        case "1":
                            if (menuLang == "it")
                            {
                                menu.ShowSecondMenuIT();
                            }
                            else
                            {
                                menu.ShowSecondMenuEN();
                            }
                            var choseThisDay = Console.ReadLine();
                            switch (choseThisDay)
                            {
                                case "1":
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertNamePlaceIT);
                                        meteoChoiceForDB = "Previsioni per un giorno (citta`)";
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNamePlaceEN);
                                        meteoChoiceForDB = "Forecast for one day (city)";
                                    }
                                    var place = Console.ReadLine();
                                    try
                                    {
                                        DateTime printDate = DateTime.Now;
                                        str = printDate.Date.ToString(formatForFile);
                                        var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnit).Result;
                                        print.PrintForData(jsonObj, menuLang);
                                        queryMng.InsertOneDayForecast(jsonObj);
                                        queryMng.InsertDataMaster(meteoChoiceForDB, idUserMaster);
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.successIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.successEN);
                                        }

                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.choiceDoFileIT);
                                            menu.ChioceIT();
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.choiceDoFileEN);
                                            menu.ChioceEN();
                                        }
                                        var choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "1")
                                        {
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileIT);
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileEN);
                                            }
                                            var fileName = string.Concat(Console.ReadLine() + "1Day" + str + ".json");
                                            var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            var file = filemenager.CreateNewFile(fileName, jsonStr);
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.successCreateFileIT);
                                                Console.WriteLine(DataInterface.choiceSendEmailIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.successCreateFileEN);
                                                Console.WriteLine(DataInterface.choiceSendEmailEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();


                                            if (choiceSelected == "1")
                                            {
                                                var dataForEmail = Program.InsertDataForEmail(menuLang);
                                                var senderValue = dataForEmail["senderKey"];
                                                var receiverValue = dataForEmail["receiverKey"];
                                                var bodyValue = dataForEmail["bodyKey"];
                                                var subjectValue = dataForEmail["subjectKey"];
                                                var userValue = dataForEmail["userKey"];
                                                emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                            }
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();

                                            if (choiceSelected == "1")
                                            {

                                                if (menuLang == "it")
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsIT);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsEN);
                                                }
                                                var xlsFileName = Console.ReadLine();


                                                createXlsFile.CreateXlsFileForToday(jsonObj, place, xlsFileName, str);
                                            }

                                            else
                                            {
                                                if (menuLang == "it")
                                                {
                                                    Console.WriteLine(DataInterface.successIT);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DataInterface.successEN);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.successIT);
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.successEN);
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;

                                //relativo ad un giorno per lat e lon
                                case "2":
                                    string lon, lat;
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertLatIT);
                                        lat = Console.ReadLine();
                                        Console.WriteLine(DataInterface.insertLonIT);
                                        lon = Console.ReadLine();
                                        meteoChoiceForDB = "Previsioni per un giorno (coordinate)";
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertLatEN);
                                        lat = Console.ReadLine();
                                        Console.WriteLine(DataInterface.insertLonEN);
                                        lon = Console.ReadLine();
                                        meteoChoiceForDB = "Forecast for one day (coordinates)";
                                    }
                                    try
                                    {
                                        DateTime printDate = DateTime.Now;
                                        str = printDate.Date.ToString(formatForFile);
                                        var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnit).Result;
                                        print.PrintForData(jsonObj, menuLang);
                                        queryMng.InsertOneDayForecast(jsonObj);
                                        queryMng.InsertDataMaster(meteoChoiceForDB, idUserMaster);
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.successIT);
                                            Console.WriteLine(DataInterface.choiceDoFileIT);
                                            menu.ChioceIT();
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.successEN);
                                            Console.WriteLine(DataInterface.choiceDoFileEN);
                                            menu.ChioceEN();
                                        }

                                        var choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "1")
                                        {
                                            string fileName, jsonStr, file;
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileIT);
                                                fileName = string.Concat(Console.ReadLine() + "1Day" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.successCreateFileIT);
                                                Console.WriteLine(DataInterface.choiceSendEmailIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileEN);
                                                fileName = string.Concat(Console.ReadLine() + "1Day" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.successCreateFileEN);
                                                Console.WriteLine(DataInterface.choiceSendEmailEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();

                                            if (choiceSelected == "1")
                                            {
                                                var dataForEmail = Program.InsertDataForEmail(menuLang);
                                                var senderValue = dataForEmail["senderKey"];
                                                var receiverValue = dataForEmail["receiverKey"];
                                                var bodyValue = dataForEmail["bodyKey"];
                                                var subjectValue = dataForEmail["subjectKey"];
                                                var userValue = dataForEmail["userKey"];
                                                emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                            }
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();

                                            if (choiceSelected == "1")
                                            {

                                                if (menuLang == "it")
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsIT);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsEN);
                                                }
                                                var xlsFileName = Console.ReadLine();



                                                createXlsFile.CreateXlsFileForTodayByCoordinates(jsonObj, lat, lon, xlsFileName, str);
                                            }
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.successIT);
                                            }
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
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case "3":
                                    break;
                                case "4":
                                    exit = false;
                                    break;
                            }
                            break;
                        case "2":
                            if (menuLang == "it")
                            {
                                menu.ShowSecondMenuIT();
                            }
                            else
                            {
                                menu.ShowSecondMenuEN();
                            }
                            // meteoChoiceForDB = "5 days";
                            var choseLast5Day = Console.ReadLine();
                            switch (choseLast5Day)
                            {
                                case "1":
                                    string place;
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
                                    try
                                    {
                                        DateTime printDate = DateTime.Now;
                                        str = printDate.Date.ToString(formatForFile);
                                        var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnit).Result;
                                        print.PrintDataLast5Day(jsonObj, menuLang);
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.successIT);
                                            Console.WriteLine(DataInterface.choiceDoFileIT);
                                            menu.ChioceIT();
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.successEN);
                                            Console.WriteLine(DataInterface.choiceDoFileEN);
                                            menu.ChioceEN();
                                        }
                                        var choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "1")
                                        {
                                            string fileName, jsonStr, file;
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileIT);
                                                fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.successCreateFileIT);
                                                Console.WriteLine(DataInterface.choiceSendEmailIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileEN);
                                                fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.successCreateFileEN);
                                                Console.WriteLine(DataInterface.choiceSendEmailEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();


                                            if (choiceSelected == "1")
                                            {
                                                var dataForEmail = Program.InsertDataForEmail(menuLang);
                                                var senderValue = dataForEmail["senderKey"];
                                                var receiverValue = dataForEmail["receiverKey"];
                                                var bodyValue = dataForEmail["bodyKey"];
                                                var subjectValue = dataForEmail["subjectKey"];
                                                var userValue = dataForEmail["userKey"];
                                                emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                            }
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();

                                            if (choiceSelected == "1")
                                            {
                                                if (menuLang == "it")
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsIT);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsEN);
                                                }
                                                var xlsFileName = Console.ReadLine();


                                                createXlsFile.CreateXlsFileForLast5Days(jsonObj, place, xlsFileName, str);
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
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
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
                                        DateTime printDate = DateTime.Now;
                                        str = printDate.Date.ToString(formatForFile);
                                        var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnit).Result;
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.insertNameFileIT);
                                            print.PrintDataLast5Day(jsonObj, menuLang);
                                            Console.WriteLine(DataInterface.successIT);
                                            Console.WriteLine(DataInterface.choiceDoFileIT);
                                            menu.ChioceIT();
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.insertNameFileEN);
                                            print.PrintDataLast5Day(jsonObj, menuLang);
                                            Console.WriteLine(DataInterface.successEN);
                                            Console.WriteLine(DataInterface.choiceDoFileEN);
                                            menu.ChioceEN();
                                        }
                                        var choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "1")
                                        {
                                            string fileName, jsonStr, file;
                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileIT);
                                                fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.choiceSendEmailIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.insertNameFileEN);
                                                fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                                jsonStr = JsonConvert.SerializeObject(jsonObj);
                                                file = filemenager.CreateNewFile(fileName, jsonStr);
                                                Console.WriteLine(DataInterface.choiceSendEmailEN);
                                                menu.ChioceEN();
                                            }
                                            choiceSelected = Console.ReadLine();

                                            if (choiceSelected == "1")
                                            {
                                                var dataForEmail = Program.InsertDataForEmail(menuLang);
                                                var senderValue = dataForEmail["senderKey"];
                                                var receiverValue = dataForEmail["receiverKey"];
                                                var bodyValue = dataForEmail["bodyKey"];
                                                var subjectValue = dataForEmail["subjectKey"];
                                                var userValue = dataForEmail["userKey"];
                                                emailManager.AttempsPasswordAndSendEmail(fileName, senderValue, receiverValue, bodyValue, subjectValue, userValue, password);
                                            }

                                            if (menuLang == "it")
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileIT);
                                                menu.ChioceIT();
                                            }
                                            else
                                            {
                                                Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                                menu.ChioceIT();
                                            }
                                            choiceSelected = Console.ReadLine();
                                            if (choiceSelected == "1")
                                            {
                                                if (menuLang == "it")
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsIT);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DataInterface.insertNameFileXlsEN);
                                                }
                                                var xlsFileName = Console.ReadLine();

                                                createXlsFile.CreateXlsFileForLast5DaysByCoordinates(jsonObj, lat, lon, xlsFileName, str);
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
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case "3":
                                    break;
                                case "4":
                                    return;
                            }
                            break;
                        case "3":
                            if (menuLang == "it")
                            {
                                menu.ShowFiltredMenuIT();
                            }
                            else
                            {
                                menu.ShowFiltredMenuEN();
                            }
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
                                        var objFilteredForHumidity = meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Result;
                                        print.PrintFilteredDataHumidity(objFilteredForHumidity, menuLang);

                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(DataInterface.successIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(DataInterface.successEN);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case "2":
                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.insertNamePlaceIT); }
                                    else
                                    { Console.WriteLine(DataInterface.insertNamePlaceEN); }
                                    place = Console.ReadLine();
                                    string date, time;
                                    DateTime printDate = DateTime.Now;
                                    str = printDate.Date.ToString(formatForFile);
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
                                    var objFilteredForTimeDate = meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Result;
                                    print.PrintDataLast5Day(objFilteredForTimeDate, menuLang);

                                    if (menuLang == "it")
                                    { Console.WriteLine(DataInterface.successIT); }
                                    else
                                    { Console.WriteLine(DataInterface.successEN); }
                                    break;
                                case "3":
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(DataInterface.insertNamePlaceIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNamePlaceEN);
                                    }
                                    place = Console.ReadLine();
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine("Iserisci tipologia di tempo richiesta");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter the type of requested weather");
                                    }
                                    var typeWeather = Console.ReadLine();
                                    var jsonObj = meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place);
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
                            var dataForEmailAnyFile = Program.InsertDataForEmail(menuLang);
                            var senderValueAnyFile = dataForEmailAnyFile["senderKey"];
                            var receiverValueAnyFile = dataForEmailAnyFile["receiverKey"];
                            var bodyValueAnyFile = dataForEmailAnyFile["bodyKey"];
                            var subjectValueAnyFile = dataForEmailAnyFile["subjectKey"];
                            var userValueAnyFile = dataForEmailAnyFile["userKey"];
                            emailManager.AttempsPasswordAndSendEmail(fileNameToSendAnyFile, senderValueAnyFile, receiverValueAnyFile, bodyValueAnyFile, subjectValueAnyFile, userValueAnyFile, password);
                            break;
                        case "6":
                            if (menuLang == "it")
                            {
                                menu.ShowMenuCreateXlsFileIT();
                            }
                            else
                            {
                                menu.ShowMenuCreateXlsFileEN();
                            }
                            var choiceXls = Console.ReadLine();
                            switch (choiceXls)
                            {
                                case "1":
                                    if (menuLang == "it")
                                    { Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati"); }
                                    else
                                    { Console.WriteLine("Enter the name of the file from which to get the data"); }
                                    var todaySourceFile = Console.ReadLine();
                                    var todayFilePath = Path.Combine(DataInterface.filePathAmer, todaySourceFile);
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
                                    var fiveDaysFilePath = Path.Combine(DataInterface.filePathGabriel, fiveDaysSourceFile);
                                    if (menuLang == "it")
                                    { Console.WriteLine("Inserisci il nome del file XLS"); }
                                    else
                                    { Console.WriteLine("Enter the name of the XLS file"); }
                                    var fiveDaysXlsFile = Console.ReadLine();
                                    createXlsFromFile.CreateXlsFromFileFor5Days(fiveDaysFilePath, fiveDaysXlsFile);
                                    break;
                                case "3":
                                    if (menuLang == "it")
                                    {
                                        menu.ShowSecondMenuIT();
                                    }
                                    else
                                    {
                                        menu.ShowSecondMenuEN();
                                    }
                                    break;
                                case "4":
                                    exit = false;
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine("Sessione terminata");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Session ended");
                                    }
                                    break;
                            }
                            break;
                        case "7":
                            exit = false;
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Sessione terminata");
                            }
                            else
                            {
                                Console.WriteLine("Session ended");
                            }
                            break;
                    }
                }
            }
        }
    }
}