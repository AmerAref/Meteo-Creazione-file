﻿using System;
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
        static string choseCreateNewAccuoutOrLogin = "";

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
            var filePath = "/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file";
            var exit = true;
            var choseConfigurationPc = new ChoseConfigurationPc();
            string insertNameFileIT = "Inserisci nome file da creare con tipo di estensione (nomefile.estensione)";
            string insertNameFileEN = "Enter the name of the file to be created and its extension (filename.extension)";
            var meteoApi = new MeteoApi();
            var selectQuestion = 0;
            var validationUsername = true;
            var controlWhileAnswer = true;
            var controlForUserIfExist = 0;
            var printQuestionForAccessAfterControl = "";
            var forAnswerInsertUsername = "";
            string successIT = "Richiesta elaborata con successo", successEN = "request processed successfully";
            string choiceDoFileIT = "Vuoi creare il file con i dati precedentemente richiesti? (S/n)";
            string choiceDoFileEN = "Do you want to create the file with the data previously requested? (S/n)";
            string choiceSendEmailIT = "Vuoi inviare tramite email il file appena creato? (S/n)";
            string choiceSendEmailEN = "Do you want to send an email with the newly created file? (S/n)";
            string insertNamePlaceIT = "Inserisci località richiesta", insertNamePlaceEN = "Enter requested location";
            string insertMeasureUnitIT = "Inserisci l'unità di misura: Celsius (metric) o Fahrenheit (imperial)";
            string insertMeasureUnitEN = "Insert the measure unit: Celsius (metric) or Fahrenheit (imperial)";
            string insertLonIT = "Insersci longitudine", insertLonEN = "Enter longitude";
            string insertLatIT = "Inserisci latitudine", insertLatEN = "Enter latitute";
            string successCreateFileIT = "File creato con successo", successCreateFileEN = "File successfully created";
            string choiceCreateXlsFileIT = "Vuoi creare un file XLS con i dati precedenti? (S/n)";
            string choiceCreateXlsFileEN = "Do you want to create an XLS file with the previous data? (S/n)";
            var password = "";
            string insertUserIT = "Inserisci Username", insertUserEN = "Enter Username";
            string insertPswIT = "Inserisci Password", insertPswEN = "Enter Password";
            string newPswIT = "Inserisci la nuova password", newPswEN = "Enter the new password";
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
            string reinsertUserPswIT = "Reinsersci Username e password", reinsertUserPswEN = "Reenter Username and Password";
            string remainingAttemptsIT = "Tentativi rimasi: ", remainingAttemptsEN = "Remaining attempts: ";
            string secureQuestionIT = "Inserisci Username per la domanda di sicurezza", secureQuestionEN = "Enter Username for the security question";
            var usernameNewAccount = "";
            var connection = new DbFactoryManager();
            var usernameAuthentication = "";
            var user = new User();




            Console.WriteLine("Scegli la lingua/ Choose the language (it/en)");
            lang = Console.ReadLine();
            var controlFirstChoiceLogin = true;
            while (controlFirstChoiceLogin)
            {
                if (lang == "it")
                {
                    menu.ShowMenuAuthenticationIT();
                }
                else
                {
                    menu.ShowMenuAuthenticationEN();
                }

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
                        if (lang == "it")
                        {
                            Console.WriteLine(insertUserIT);
                            usernameAuthentication = Console.ReadLine();
                            Console.WriteLine(insertPswIT);
                        }
                        else
                        {
                            Console.WriteLine(insertUserEN);
                            usernameAuthentication = Console.ReadLine();
                            Console.WriteLine(insertPswEN);
                        }

                        // Inserimento psw mascherata 
                        var passwordAuthentication = DataMaskManager.MaskData(passwordLogin);
                        // Criptaggio Psw
                        var authPwd = registration.EncryptPwd(passwordAuthentication);
                        // confronto se esiste psw (Massimo 3 volte )
                        var autentication = queryMng.GetUSerIfExist(usernameAuthentication, authPwd);
                        if (autentication.Any())
                        {
                            // Da il benvenuto e accede al menu Meteo
                            Console.WriteLine("\n");
                            if (lang == "it")
                            { Console.WriteLine("Benvenuto" + " " + $"{usernameAuthentication}"); }
                            else
                            { Console.WriteLine("Welcome" + " " + $"{usernameAuthentication}"); }
                            controlFirstChoiceLogin = false;
                            controlWhilePsw = 5;
                        }
                        else
                        {
                            // Reinserimento Psw (massimo altri 2 tentativi)
                            if (lang == "it")
                            {
                                Console.WriteLine($"\n{reinsertUserPswIT}");
                                Console.WriteLine($"{remainingAttemptsIT} {countAttempts}");
                            }
                            else
                            {
                                Console.WriteLine($"\n{reinsertUserPswEN}");
                                Console.WriteLine($"{remainingAttemptsEN} {countAttempts}");
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
                                    if (lang == "it")
                                    {
                                        Console.WriteLine($"{secureQuestionIT}");
                                    }
                                    else
                                    {

                                        Console.WriteLine($"{secureQuestionEN}");

                                    }


                                    forAnswerInsertUsername = Console.ReadLine();
                                    var userIfExist = queryMng.GetUser(forAnswerInsertUsername);

                                    var reciveIDQuestion = 0;
                                    var question = queryMng.GetQuestion(reciveIDQuestion, userIfExist[0]);

                                    // controllo se esiste user 
                                    if (userIfExist != null)
                                    {
                                        var printQuestionForAccessIfExist = question.DefaultQuestion;

                                        // controllo se esiste domanda per user 
                                        if (printQuestionForAccessIfExist != null)
                                        {
                                            // ricavo Domanda con stampa ed accesso a form per modifica psw 
                                            printQuestionForAccessAfterControl = question.DefaultQuestion;
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
                                // Verifica se Risposta è corretta. Il risultato è dentro autentication 
                                autentication = queryMng.AutentiationWithAnswer(insertAnswerForAccessEcrypted, forAnswerInsertUsername);
                                if (autentication.Any())
                                {

                                    var controlRequirementsNewPsw = true;
                                    if (lang == "it")
                                    {
                                        Console.WriteLine($"\n{newPswIT}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\n{newPswEN}");
                                    }
                                    // Dopo 3 volte che non vengono rispettati i criteri di sicurezza della psw termina la sessione
                                    while (controlRequirementsNewPsw)
                                    {
                                        var newPswClear = "";
                                        // maschera nuova psw 
                                        var newPswMask = DataMaskManager.MaskData(newPswClear);
                                        // controlo su vincoli di sicurezza psw
                                        if (Helper.RegexForPsw(pswNewAccount) == false)
                                        {
                                            if (lang == "it")
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
                                                if (lang == "en")
                                                { Console.WriteLine("Mi dispiace, ma hai esaurito i tentativi!"); }
                                                else
                                                { Console.WriteLine("I'm sorry, but you've exhausted the attempts!"); }
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
                        controlWhilePsw++;
                    }
                }

                // Creazione nuovo utente 
                if (choseCreateNewAccuoutOrLogin == "2")
                {
                    // Inserimento nome Registrazione
                    if (lang == "it")
                    {
                        Console.WriteLine("Inserisci Nome");
                    }
                    else
                    {
                        Console.WriteLine("Enter Name");
                    }
                    var nameNewAccuont = Console.ReadLine();

                    // Inserimento Cognome Registrazione
                    if (lang == "it")
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
                            if (lang == "it")
                            {
                                Console.WriteLine(insertUserIT);
                            }
                            else
                            {
                                Console.WriteLine(insertUserEN);
                            }

                            usernameNewAccount = Console.ReadLine();

                            var autentication = queryMng.GetUser(usernameNewAccount);
                            // autentication è vuota nel caso in cui non esiste un user con stesso username
                            if (autentication.Any())
                            {
                                // non ti fa uscire da while (reinserisci Username)
                                if (lang == "it")
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
                        if (lang == "it")
                        {
                            Console.WriteLine(insertPswIT);
                        }
                        else
                        {
                            Console.WriteLine(insertPswEN);
                        }
                        // Inserisci Psw mascherata

                        pswNewAccount = DataMaskManager.MaskData(passwordRegistration);
                        // Controlla se Accetta i criteri di sicurezza psw
                        if (Helper.RegexForPsw(pswNewAccount) == false)
                        {
                            if (lang == "it")
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
                                if (lang == "it")
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
                            if (lang == "it")
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
                                // contro su correttezza risposta (esce Quando è seleziona "S" per salvare la risposta alla domanda)
                                while (controlWhileAnswer)
                                {
                                    // menu per domande 
                                    menu.SelectQuestion();
                                    // stampa risposta inserita
                                    selectQuestion = Convert.ToInt32(Console.ReadLine());
                                    if (lang == "it")
                                    { Console.WriteLine("Inserisci risposta di sicurezza"); }
                                    else
                                    { Console.WriteLine("Insert security answer"); }

                                    // stampa risposta inserita 
                                    Console.WriteLine(selectQuestion);
                                    // conferma rispost inserita 
                                    insertAnswer = Console.ReadLine();
                                    if (lang == "it")
                                    { Console.WriteLine("La risposta richiesta è la seguente? "); }
                                    else
                                    { Console.WriteLine("Is the following the required answer?"); }
                                    Console.WriteLine(insertAnswer);
                                    Console.WriteLine("S/n");
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
                                var selectLanguage = "";
                                var languageNewAccount = "";
                                int roleNewAccount;
                                measureUnit = "";
                                if (lang == "it")
                                {
                                    Console.WriteLine("Inserisci la lingua (it/en)");
                                    menu.SelectLanguage();
                                    selectLanguage = Console.ReadLine();
                                    switch (selectLanguage)
                                    {
                                        case "1":
                                            languageNewAccount = "it";
                                            break;
                                        case "2":
                                            languageNewAccount = "en";
                                            break;
                                    }
                                    menu.SelectRole();
                                    Console.WriteLine("Inserisci il ruolo");
                                    roleNewAccount = Convert.ToInt32(Console.ReadLine());
                                    measureUnit = "metric";
                                }
                                else
                                {
                                    Console.WriteLine("Enter the language (it/en)");
                                    menu.SelectLanguage();
                                    selectLanguage = Console.ReadLine();
                                    switch (selectLanguage)
                                    {
                                        case "1":
                                            languageNewAccount = "it";
                                            break;
                                        case "2":
                                            languageNewAccount = "en";
                                            break;
                                    }
                                    menu.SelectRole();
                                    Console.WriteLine("Insert the role");
                                    roleNewAccount = Convert.ToInt32(Console.ReadLine());
                                    measureUnit = "imperial";
                                }

                                queryMng.InsertNewUser(encryptedAnswer, usernameNewAccount, surnameNewAccount, nameNewAccuont, selectQuestion, encryptedAnswer, languageNewAccount, measureUnit, roleNewAccount);
                                controlFirstChoiceLogin = false;
                                validationManagerPsw = false;
                                if (lang == "it")
                                { Console.WriteLine($"\nBenvenuto utente: {usernameNewAccount}"); }
                                else
                                { Console.WriteLine($"\nWelcome user: {usernameNewAccount}"); }
                            }
                            else
                            {
                                if (lang == "it")
                                { Console.WriteLine($"\nLe due password inserite non corrispondono! {reinsertUserPswIT}"); }
                                else
                                { Console.WriteLine($"\nThe two entered passwords don't match! {reinsertUserPswEN}"); }
                            }
                        }
                    }
                }
            }

            // salvataggio modifiche su DB 

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
                                if (menuLang == "it")
                                { Console.WriteLine(insertNamePlaceIT); }
                                else
                                { Console.WriteLine(insertNamePlaceEN); }
                                var place = Console.ReadLine();
                                if (menuLang == "it")
                                { Console.WriteLine(insertMeasureUnitIT); }
                                else
                                { Console.WriteLine(insertMeasureUnitEN); }
                                var measureUnitToday = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnitToday).Result;
                                    print.PrintForData(jsonObj);
                                    if (menuLang == "it")
                                    { Console.WriteLine(successIT); }
                                    else
                                    { Console.WriteLine(successEN); }
                                    var prop = "Pressure";
                                    var Propr = jsonObj.Parameters.GetType().GetProperty(prop).GetValue(jsonObj.Parameters, null);//GetType().GetProperty(prop).GetValue(jsonObj.Main, null);
                                    Console.WriteLine(Propr);

                                    if (menuLang == "it")
                                    { Console.WriteLine(choiceDoFileIT); }
                                    else
                                    { Console.WriteLine(choiceDoFileEN); }
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(insertNameFileIT); }
                                        else
                                        { Console.WriteLine(insertNameFileEN); }
                                        var fileName = Console.ReadLine();
                                        var jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        var file = filemenager.CreateNewFile(fileName, jsonStr);
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(choiceSendEmailIT);
                                            choiceSelected = Console.ReadLine();
                                            Console.WriteLine(successCreateFileIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(choiceSendEmailEN);
                                            choiceSelected = Console.ReadLine();
                                            Console.WriteLine(successCreateFileEN);
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
                                        { Console.WriteLine(choiceCreateXlsFileIT); }
                                        else
                                        { Console.WriteLine(choiceCreateXlsFileEN); }
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForToday(fileName, jsonObj, place);
                                        }

                                        else
                                        {
                                            if (menuLang == "it")
                                            { Console.WriteLine(successIT); }
                                            else
                                            { Console.WriteLine(successEN); }
                                        }
                                    }
                                    else
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(successIT); }
                                        else
                                        { Console.WriteLine(successEN); }
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
                                string lon, lat, measureUnitCoordinates;
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(insertLatIT);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(insertLonIT);
                                    lon = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitIT);
                                    measureUnitCoordinates = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine(insertLatEN);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(insertLonEN);
                                    lon = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitEN);
                                    measureUnitCoordinates = Console.ReadLine();
                                }
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnitCoordinates).Result;
                                    print.PrintForData(jsonObj);
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(successIT);
                                        Console.WriteLine(choiceDoFileIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(successEN);
                                        Console.WriteLine(choiceDoFileEN);
                                    }
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        string fileName, jsonStr, file;
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(insertNameFileIT);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(successCreateFileIT);
                                            Console.WriteLine(choiceSendEmailIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(insertNameFileEN);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(successCreateFileEN);
                                            Console.WriteLine(choiceSendEmailEN);
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
                                        { Console.WriteLine(choiceCreateXlsFileIT); }
                                        else
                                        { Console.WriteLine(choiceCreateXlsFileEN); }
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForTodayByCoordinates(fileName, jsonObj, lat, lon);
                                        }
                                        if (menuLang == "it")
                                        { Console.WriteLine(successIT); }
                                        else
                                        { Console.WriteLine(successEN); }
                                    }
                                    else
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(successIT); }
                                        else
                                        { Console.WriteLine(successEN); }
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
                                    Console.WriteLine(insertNamePlaceIT);
                                    place = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitIT);
                                }
                                else
                                {
                                    Console.WriteLine(insertNamePlaceEN);
                                    place = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitIT);
                                }
                                var measureUnitFiveDays = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnitFiveDays).Result;
                                    print.PrintDataLast5Day(jsonObj);
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(successIT);
                                        Console.WriteLine(choiceDoFileIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(successEN);
                                        Console.WriteLine(choiceDoFileEN);
                                    }
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        string fileName, jsonStr, file;
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(insertNameFileIT);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(choiceSendEmailIT);
                                            choiceSelected = Console.ReadLine();
                                            Console.WriteLine(successCreateFileIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(insertNameFileEN);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(choiceSendEmailEN);
                                            choiceSelected = Console.ReadLine();
                                            Console.WriteLine(successCreateFileEN);
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
                                        { Console.WriteLine(choiceCreateXlsFileIT); }
                                        else
                                        { Console.WriteLine(choiceCreateXlsFileEN); }
                                        choiceSelected = Console.ReadLine();

                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5Days(fileName, jsonObj, place);
                                        }
                                    }
                                    else
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(successIT); }
                                        else
                                        { Console.WriteLine(successEN); }
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
                                    Console.WriteLine(insertLatIT);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(insertLonIT);
                                    lon = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitIT);
                                }
                                else
                                {
                                    Console.WriteLine(insertLatEN);
                                    lat = Console.ReadLine();
                                    Console.WriteLine(insertLonEN);
                                    lon = Console.ReadLine();
                                    Console.WriteLine(insertMeasureUnitEN);
                                }
                                var measureUnitCoordinatesFiveDays = Console.ReadLine();
                                try
                                {
                                    var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnitCoordinatesFiveDays).Result;
                                    if (menuLang == "it")
                                    {
                                        Console.WriteLine(insertNameFileIT);
                                        print.PrintDataLast5Day(jsonObj);
                                        Console.WriteLine(successIT);
                                        Console.WriteLine(choiceDoFileIT);
                                    }
                                    else
                                    {
                                        Console.WriteLine(insertNameFileEN);
                                        print.PrintDataLast5Day(jsonObj);
                                        Console.WriteLine(successEN);
                                        Console.WriteLine(choiceDoFileEN);
                                    }
                                    var choiceSelected = Console.ReadLine();
                                    if (choiceSelected == "S")
                                    {
                                        string fileName, jsonStr, file;
                                        if (menuLang == "it")
                                        {
                                            Console.WriteLine(insertNameFileIT);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(choiceSendEmailIT);
                                        }
                                        else
                                        {
                                            Console.WriteLine(insertNameFileEN);
                                            fileName = Console.ReadLine();
                                            jsonStr = JsonConvert.SerializeObject(jsonObj);
                                            file = filemenager.CreateNewFile(fileName, jsonStr);
                                            Console.WriteLine(choiceSendEmailEN);
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
                                        { Console.WriteLine(choiceCreateXlsFileIT); }
                                        else
                                        { Console.WriteLine(choiceCreateXlsFileEN); }
                                        choiceSelected = Console.ReadLine();
                                        if (choiceSelected == "S")
                                        {
                                            createXlsFile.CreateXlsFileForLast5DaysByCoordinates(fileName, jsonObj, lat, lon);
                                        }
                                    }
                                    else
                                    {
                                        if (menuLang == "it")
                                        { Console.WriteLine(successIT); }
                                        else
                                        { Console.WriteLine(successEN); }
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
                                { Console.WriteLine(insertNamePlaceIT); }
                                else
                                { Console.WriteLine(insertNamePlaceEN); }
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
                                    { Console.WriteLine(successIT); }
                                    else
                                    { Console.WriteLine(successEN); }
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
                                { Console.WriteLine(insertNamePlaceIT); }
                                else
                                { Console.WriteLine(insertNamePlaceEN); }
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
                                { Console.WriteLine(successIT); }
                                else
                                { Console.WriteLine(successEN); }
                                break;
                            case "3":
                                if (menuLang == "it")
                                { Console.WriteLine(insertNamePlaceIT); }
                                else
                                { Console.WriteLine(insertNamePlaceEN); }
                                place = Console.ReadLine();
                                if (menuLang == "it")
                                { Console.WriteLine("Iserisci tipologia di tempo richiesta"); }
                                else
                                { Console.WriteLine("Enter the type of requested weather"); }
                                var typeWeather = Console.ReadLine();
                                meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place).Wait();
                                if (menuLang == "it")
                                { Console.WriteLine(successIT); }
                                else
                                { Console.WriteLine(successEN); }
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
}