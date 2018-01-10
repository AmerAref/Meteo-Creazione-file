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
        private static Dictionary<string, string> InsertDataForEmail()
        {
            var dictionaryForEmail = new Dictionary<string, string>();
            var insertSender = "inserisci email mittente";
            var insertReciver = "Inserisci email destinatario";
            var insertBody = "Iserisci Testo all'interno dell'email";
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
            var login = new Login();
            var registration = new Register();
            var emailManager = new EmailManager();
            var filemenager = new FileMenager();
            var createXlsFile = new CreateXlsFile();
            var createXlsFromFile = new CreateXlsFromFiles();
            var menu = new Menu();
            var print = new PrintData();
            var queryMng = new QueryManager();
            var filePath = "/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file";
            var exit = true;
            var choseConfigurationPc = new ChoseConfigurationPc();
            string insertNameFileIT = "Inserisci nome file da creare con tipo di estensione (nomefile.estensione)";
            string insertNameFileEN = "Insert the name of the file to be created and its extension (filename.extension)";
            var meteoApi = new MeteoApi();
            var validationUsername = true;
            var controlWhileAnswer = true;
            var selectQuestion = "";
            var controlForUserIfExist = 0;
            var printQuestionForAccessAfterControl = "";
            var forAnswerInsertUsername = "";
            string successIT = "Richiesta elaborata con successo", successEN = "request processed successfully";
            string choiceDoFileIT = "Vuoi creare il file con i dati precedentemente richiesti? (S/n)";
            string choiceDoFileEN = "Do you want to create the file with the data previously requested? (S/n)";
            string choiceSendEmailIT = "Vuoi inviare tramite email il file appena creato? (S/n)";
            string choiceSendEmailEN = "Do you want to send an email with the newly created file? (S/n)";
            string insertNamePlaceIT = "Inserisci località richiesta", insertNamePlaceEN = "Insert requested location";
            string insertMeasureUnitIT = "Inserisci l'unità di misura: Celsius (metric) o Fahrenheit (imperial)";
            string insertMeasureUnitEN = "Insert the measure unit: Celsius (metric) or Fahrenheit (imperial)";
            string insertLonIT = "Insersci longitudine", insertLonEN = "Insert longitude";
            string insertLatIT = "Inserisci latitudine", insertLatEN = "Insert latitute";
            string successCreateFileIT = "File creato con successo", successCreateFileEN = "File successfully created";
            string nameFileDeleteIT = "Inserisci nome file da eliminare, con tipo di estensione (nomefile.estensione)";
            string nameFileDeleteEN = "Insert the name of the file to be deleted, with the extension (filename.extension)";
            string choiceCreateXlsFileIT = "Vuoi creare un file XLS con i dati precedenti? (S/n)";
            string choiceCreateXlsFileEN = "Do you want to create an XLS file with the previous data? (S/n)";
            var password = "";
            string insertUserIT = "Inserisci Username", insertUserEN = "Insert Username";
            string insertPswIT = "Inserisci Password", insertPswEN = "Insert Password";
            string insertNameIT = "Inserisci Nome", insertNameEN = "Insert Name";
            string insertSurnameIT = "Insersci Cognome", insertSurnameEN = "Insert Surname";
            string insertLangIT = "Inserisci Lingua", insertLangEN = "Insert Language";
            var controlWhilePsw = 0;
            var countAttempts = 2;
            var lang = "";
            var passwordLogin = "";
            var answerToLogin = "";
            var passwordRegistration = "";
            var validationManagerPsw = true;
            var pswNewAccount = "";
            var countAttemptsPswRegister = 0;
            string printInsertAnswerIT = "Inserisci risposta di sicurezza", printInsertAnswerEN = "Insert security answer";
            var insertAnswer = "";
            string reinsertUserPsw = "Reinsersci Username e password", reinsertUserPswEN = "Reenter USername and Password";
            var usernameNewAccount = "";
            var connection = new DbFactoryManager();
            connection.OpenConnection();

            var builder = new ConfigurationBuilder()
                .AddJsonFile(choseConfigurationPc.ConfigGabriel(), optional: false, reloadOnChange: true);
            var factory = new ApplicationDbContextFactory();
            var context = factory.CreateDbContext(new string[] { });

            context.SaveChanges();

            Console.WriteLine("Scegli la lingua/ Choose the language (it/en)");
            lang = Console.ReadLine();
            var controlFirstChoiceLogin = true;
            while (controlFirstChoiceLogin)
            {
                if (lang == "it")
                { menu.ShowMenuAuthenticationIT(); }
                else
                { menu.ShowMenuAuthenticationEN(); }

                var choseCreateNewAccuoutOrLogin = Console.ReadLine();
                controlWhilePsw = 0;
                countAttempts = 2;
                //Caso scelta Login con utente già registrato su DB
                if (choseCreateNewAccuoutOrLogin == "1")
                {
                    //Controllo Uscita dal ciclo While
                    while (controlWhilePsw < 5)
                    {
                        // Inserimento User
                        Console.WriteLine(insertUserIT);
                        var usernameAuthentication = Console.ReadLine();
                        Console.WriteLine(insertPswIT);
                        // Inserimento psw mascherata 
                        var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
                        // Criptaggio Psw
                        var authPwd = registration.EncryptPwd(passwordAuthentication);
                        // confronto se esiste psw (Massimo 3 volte )
                        var autentication = login.LoginAttempts(context, usernameAuthentication, authPwd);
                        if (autentication.Any())
                        {
                            // Da il benvenuto e accede al menu Meteo
                            Console.WriteLine("\n");
                            Console.WriteLine($"Benvenuto {usernameAuthentication}");
                            controlFirstChoiceLogin = false;
                            controlWhilePsw = 5;
                        }
                        else
                        {
                            // Reinserimento Psw (massimo altri 2 tentativi)
                            Console.WriteLine($"\n{reinsertUserPsw}");
                            Console.WriteLine($"Numero dei tenativi rimasti {countAttempts}");
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
                                    Console.WriteLine("Inserisci Username per accedere con domanda di sicurezza");
                                    forAnswerInsertUsername = Console.ReadLine();
                                    var userIfExist = queryMng.UserExistance(context, forAnswerInsertUsername);
                                    var reciveIDQuestion = 0;

                                    // controllo se esiste user 
                                    if (userIfExist != null)
                                    {
                                        var printQuestionForAccessIfExist = queryMng.QuestionExistance(context, reciveIDQuestion, userIfExist);

                                        // controllo se esiste domanda per user 
                                        if (printQuestionForAccessIfExist != null)
                                        {
                                            // ricavo Domanda con stampa ed accesso a form per modifica psw 
                                            printQuestionForAccessAfterControl = queryMng.QuestionControl(context, reciveIDQuestion);
                                            Console.WriteLine(printQuestionForAccessAfterControl);
                                            controlForUserIfExist = 4;
                                        }
                                    }
                                    controlForUserIfExist++;
                                }

                                // inserimento Risposta mascherata 
                                var insertAnswerMaskered = DataMaskManager.MaskData(answerToLogin);
                                // criptaggio della Risposta inserita 
                                var insertAnswerForAccessEcrypted = registration.EncryptPwd(insertAnswerMaskered);
                                autentication = login.ControlAnswer(insertAnswerForAccessEcrypted, context, forAnswerInsertUsername).ToList();
                                // Verifica se Risposta è corretta. Il risultato è dentro autentication 
                                if (autentication.Any())
                                {
                                    //forech effettuato su l'unico elemeto all'interno della lista 
                                    foreach (var tableUser in autentication)
                                    {
                                        var controlRequirementsNewPsw = true;
                                        Console.WriteLine("\nInserisci nuova password");

                                        // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
                                        while (controlRequirementsNewPsw)
                                        {
                                            var newPswClear = "";
                                            // maschera nuova psw 
                                            var newPswMask = DataMaskManager.MaskData(newPswClear);
                                            // controlo su vincoli di sicurezza psw
                                            if (Helper.RegexForPsw(pswNewAccount) == false)
                                            {
                                                Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                                                Console.WriteLine("\nReinserisci Password.");
                                                countAttemptsPswRegister++;

                                                // uscita in caso di 3 errori
                                                if (countAttemptsPswRegister == 3)
                                                {
                                                    Console.WriteLine("Mi Dispiace ma hai esaurito i tentativi");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                // criptaggio nuova psw 
                                                var newPswEncrypted = registration.EncryptPwd(newPswMask);

                                                // Aggiornamento psw in DB
                                                tableUser.Password = newPswEncrypted;
                                                // salvataggio aggiornamento nel DB 
                                                context.SaveChanges();
                                                controlWhilePsw = 5;
                                                // Uscita dal while 
                                                controlFirstChoiceLogin = false;
                                                controlRequirementsNewPsw = false;
                                            }
                                        }
                                    }
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
                    Console.WriteLine(insertNameIT);
                    var nameNewAccuont = Console.ReadLine();

                    // Inserimento Cognome Registrazione 
                    Console.WriteLine(insertSurnameIT);
                    var surnameNewAccount = Console.ReadLine();
                    // While Per validazione psw
                    while (validationManagerPsw)
                    {
                        // While per controllo username (deve essere univoco sul DB)
                        while (validationUsername)
                        {
                            Console.WriteLine(insertUserIT);

                            usernameNewAccount = Console.ReadLine();

                            var autentication = login.ControlUserIfExist(context, usernameNewAccount);
                            // autentication è vuota nel caso in cui non esiste un user con stesso username
                            if (autentication.Any())
                            {
                                // non ti fa uscire da while (reinserisci Username)
                                Console.WriteLine("Username già esistente. Provare con un altro nome.");
                            }
                            else
                            {
                                // esce dal while che gestisce inserimento univoco user
                                validationUsername = false;
                            }
                        }
                        Console.WriteLine(insertPswIT);
                        // Inserisci Psw mascherata

                        pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
                        // Controlla se Accetta i criteri di sicurezza psw
                        if (Helper.RegexForPsw(pswNewAccount) == false)
                        {
                            Console.WriteLine("\nI criteri di sicurezza non sono stati soddisfatti (Inserire 1 lettera maiuscola, 1 numero, 1 carattere speciale. La lunghezza deve essere maggiore o uguale ad 8)");
                            Console.WriteLine("\nReinserisci Password.");
                            countAttemptsPswRegister++;
                            // se l'utente non soddisfa i criteri per 3 volte termina la sessione
                            if (countAttemptsPswRegister == 3)
                            {
                                Console.WriteLine("Mi Dispiace ma hai esaurito i tentativi");
                                return;
                            }
                        }
                        // se inserisce psw secondo i criteri di sicurezza deve riscrivere la psw da confrontare con la precedente 
                        else
                        {
                            Console.WriteLine("\nReinserisci password");
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
                                    selectQuestion = Console.ReadLine();
                                    // stampa risposta inserita 
                                    Console.WriteLine(printInsertAnswerIT);
                                    // conferma rispost inserita 
                                    insertAnswer = Console.ReadLine();
                                    Console.WriteLine("La risposta richiesta è la seguente ? ");
                                    Console.WriteLine(insertAnswer);
                                    Console.WriteLine("S / n");
                                    var controlAnswer = Console.ReadLine();

                                    // digita "S" Esce da While altrimenti ti fa reinserire
                                    if (controlAnswer == "S")
                                    {
                                        controlWhileAnswer = false;
                                    }
                                }
                                // criptaggio di Psw e Risposta 
                                var encryptedPwd = registration.EncryptPwd(pswNewAccount);
                                var encryptedAnswer = registration.EncryptPwd(insertAnswer);
                                Console.WriteLine("Inserisci la lingua");
                                var languageNewAccount = Console.ReadLine();
                                context.Users.Add(
                                    new User
                                    {
                                        Password = encryptedPwd,
                                        Username = usernameNewAccount,
                                        Surname = surnameNewAccount,
                                        Name = nameNewAccuont,
                                        Question = selectQuestion,
                                        Answer = encryptedAnswer,
                                        Language = languageNewAccount
                                    }
                                );
                                controlFirstChoiceLogin = false;
                                validationManagerPsw = false;
                                Console.WriteLine($"\nBenvenuto untente {usernameNewAccount}");
                            }
                            else
                            {
                                Console.WriteLine($"\nLe due password inserite non corrispondono! {reinsertUserPsw}");
                            }
                        }
                    }
                }
            }

            // salvataggio modifiche su DB 
            context.SaveChanges();
            connection.CloseConnection();

            while (exit)
            {
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
                                Console.WriteLine(insertNamePlaceIT);
                                var place = Console.ReadLine();
                                Console.WriteLine(insertMeasureUnitIT);
                                var measureUnitToday = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnitToday).Result;
                                    print.PrintForData(jsonObj);
                                    Console.WriteLine(successIT);
                                    var prop = "Pressure";
                                    var Propr = jsonObj.Parameters.GetType().GetProperty(prop).GetValue(jsonObj.Parameters, null);//GetType().GetProperty(prop).GetValue(jsonObj.Main, null);
                                    Console.WriteLine(Propr);

                                    Console.WriteLine(choiceDoFileIT);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFileIT);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmailIT);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(successCreateFileIT);

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
                                        Console.WriteLine(choiceCreateXlsFileIT);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForToday(fileName, jsonObj, place);
                                        }

                                        else
                                        {
                                            Console.WriteLine(successIT);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(successIT);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                Console.WriteLine(insertLatIT);
                                var lat = Console.ReadLine();
                                Console.WriteLine(insertLonIT);
                                var lon = Console.ReadLine();
                                Console.WriteLine(insertMeasureUnitIT);
                                var measureUnitCoordinates = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnitCoordinates).Result;
                                    print.PrintForData(jsonObj);
                                    Console.WriteLine(successIT);
                                    Console.WriteLine(choiceDoFileIT);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFileIT);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(successCreateFileIT);
                                        Console.WriteLine(choiceSendEmailIT);
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
                                        Console.WriteLine(choiceCreateXlsFileIT);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForTodayByCoordinates(fileName, jsonObj, lat, lon);
                                        }
                                        Console.WriteLine(successIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(successIT);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
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
                                Console.WriteLine(insertNamePlaceIT);
                                var place = Console.ReadLine();
                                Console.WriteLine(insertMeasureUnitIT);
                                var measureUnitFiveDays = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnitFiveDays).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    Console.WriteLine(successIT);
                                    Console.WriteLine(choiceDoFileIT);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFileIT);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmailIT);
                                        choiceSelected = Console.ReadLine();
                                        Console.WriteLine(successCreateFileIT);

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
                                        Console.WriteLine(choiceCreateXlsFileIT);
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5Days(fileName, jsonObj, place);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(successIT);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                Console.WriteLine(insertLatIT);
                                var lat = Console.ReadLine();
                                Console.WriteLine(insertLonIT);
                                var lon = Console.ReadLine();
                                Console.WriteLine(insertMeasureUnitIT);
                                var measureUnitCoordinatesFiveDays = Console.ReadLine();
                                try
                                {
                                    Console.WriteLine(insertNameFileIT);
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnitCoordinatesFiveDays).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    Console.WriteLine(successIT);
                                    Console.WriteLine(choiceDoFileIT);
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        Console.WriteLine(insertNameFileIT);
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(choiceSendEmailIT);
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

                                        Console.WriteLine(choiceCreateXlsFileIT);
                                        choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5DaysByCoordinates(fileName, jsonObj, lat, lon);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(successIT);
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
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
                                Console.WriteLine(insertNamePlaceIT);
                                var place = Console.ReadLine();
                                Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni");
                                var humidity = Console.ReadLine();
                                try
                                {
                                    meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Wait();
                                    Console.WriteLine(successIT);
                                }
                                catch
                                {
                                    Console.WriteLine("Errore");
                                }
                                break;
                            case "2":
                                Console.WriteLine(insertNamePlaceIT);
                                place = Console.ReadLine();
                                Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
                                var date = Console.ReadLine();
                                Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
                                var time = Console.ReadLine();
                                meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Wait();
                                Console.WriteLine(successIT);
                                break;
                            case "3":
                                Console.WriteLine(insertNamePlaceIT);
                                place = Console.ReadLine();
                                Console.WriteLine("Iserisci tipologia di tempo richiesta");
                                var typeWeather = Console.ReadLine();
                                meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place).Wait();
                                Console.WriteLine(successIT);
                                break;
                            case "4":
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine(nameFileDeleteIT);
                        var fileNameDelete = Console.ReadLine();
                        filemenager.DeleteFile(fileNameDelete);
                        break;
                    case "5":
                        Console.WriteLine("Inserisci nome file da inviare tramite email");
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
                                Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati");
                                var todaySourceFile = Console.ReadLine();
                                var todayFilePath = Path.Combine(filePath, todaySourceFile);
                                Console.WriteLine("Inserisci il nome del file XLS");
                                var todayXlsFile = Console.ReadLine();

                                createXlsFromFile.CreateXlsFromFileForToday(todayFilePath, todayXlsFile);
                                break;
                            case "2":
                                Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati");
                                var fiveDaysSourceFile = Console.ReadLine();
                                var fiveDaysFilePath = Path.Combine(filePath, fiveDaysSourceFile);
                                Console.WriteLine("Inserisci il nome del file XLS");
                                var fiveDaysXlsFile = Console.ReadLine();
                                createXlsFromFile.CreateXlsFromFileFor5Days(fiveDaysFilePath, fiveDaysXlsFile);
                                break;
                            case "3":
                                menu.ShowFirst();
                                break;
                            case "4":
                                exit = false;
                                Console.WriteLine("Sessione terminata");
                                break;
                        }
                        break;
                    case "7":
                        exit = false;
                        Console.WriteLine("Sessione terminata");
                        break;
                }
            }
        }
    }
}