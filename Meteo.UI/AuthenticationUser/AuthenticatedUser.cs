using System;
namespace Meteo.UI.AuthenticationUser
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser()
        {
            var idUserMaster = queryBuilder.GetUser(username).IdUser;
            var sceltaPrimaria = Console.ReadLine();
            switch (sceltaPrimaria)
            {
                //case 1 del primo switch è relativo alle previsioni ad un giorno  
                case "1":

                    menu.ShowSecondMenu();

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
                                str = printDate.Date.ToString(formatDateForFile);
                                var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, measureUnit).Result;
                                print.PrintForData(jsonObj, menuLang);
                                queryBuilder.InsertOneDayForecast(jsonObj);
                                queryBuilder.InsertDataMaster(meteoChoiceForDB, idUserMaster);
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
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.choiceDoFileEN);
                                }
                                menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                    }
                                    menu.Chioce();

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
                                str = printDate.Date.ToString(formatDateForFile);
                                var jsonObj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, measureUnit).Result;
                                print.PrintForData(jsonObj, menuLang);
                                queryBuilder.InsertOneDayForecast(jsonObj);
                                queryBuilder.InsertDataMaster(meteoChoiceForDB, idUserMaster);
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.successIT);
                                    Console.WriteLine(DataInterface.choiceDoFileIT);
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.successEN);
                                    Console.WriteLine(DataInterface.choiceDoFileEN);
                                }
                                menu.Chioce();


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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = string.Concat(Console.ReadLine() + "1Day" + str + ".json");
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                    }
                                    menu.Chioce();

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
                                    {
                                        Console.WriteLine(DataInterface.successEN);
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
                        case "3":
                            break;
                        case "4":
                            exit = false;
                            break;
                    }
                    break;
                case "2":

                    menu.ShowSecondMenu();

                    if (menuLang == "it")
                    {
                        meteoChoiceForDB = "Previsioni per 5 giorni (citta)";
                    }
                    else
                    {
                        meteoChoiceForDB = "Forecast for 5 days (city)";
                    }

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
                                str = printDate.Date.ToString(formatDateForFile);
                                var jsonObj = meteoApi.ProcessMeteoByPlaceLast5Day(place, measureUnit).Result;
                                print.PrintDataLast5Day(jsonObj, menuLang);
                                queryBuilder.Insert5DaysForecast(jsonObj);
                                queryBuilder.InsertDataMaster(meteoChoiceForDB, idUserMaster);
                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.successIT);
                                    Console.WriteLine(DataInterface.choiceDoFileIT);
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.successEN);
                                    Console.WriteLine(DataInterface.choiceDoFileEN);
                                }
                                menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.successCreateFileEN);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                    }
                                    menu.Chioce();

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
                        case "2":
                            string lat, lon;
                            if (menuLang == "it")
                            {
                                meteoChoiceForDB = "Previsioni per 5 giorni (coordinate)";
                                Console.WriteLine(DataInterface.insertLatIT);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonIT);
                                lon = Console.ReadLine();
                            }
                            else
                            {
                                meteoChoiceForDB = "Forecast for 5 days (coordinates)";
                                Console.WriteLine(DataInterface.insertLatEN);
                                lat = Console.ReadLine();
                                Console.WriteLine(DataInterface.insertLonEN);
                                lon = Console.ReadLine();
                            }
                            try
                            {
                                DateTime printDate = DateTime.Now;
                                str = printDate.Date.ToString(formatDateForFile);
                                var jsonObj = meteoApi.ProcessMeteoByCoordinatesLast5Day(lon, lat, measureUnit).Result;
                                print.PrintDataLast5Day(jsonObj, menuLang);
                                queryBuilder.Insert5DaysForecast(jsonObj);
                                queryBuilder.InsertDataMaster(meteoChoiceForDB, idUserMaster);


                                if (menuLang == "it")
                                {
                                    Console.WriteLine(DataInterface.insertNameFileIT);
                                    Console.WriteLine(DataInterface.successIT);
                                    Console.WriteLine(DataInterface.choiceDoFileIT);
                                }
                                else
                                {
                                    Console.WriteLine(DataInterface.insertNameFileEN);
                                    Console.WriteLine(DataInterface.successEN);
                                    Console.WriteLine(DataInterface.choiceDoFileEN);
                                }
                                menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.insertNameFileEN);
                                        fileName = string.Concat(Console.ReadLine() + "5Days" + str + ".json");
                                        jsonStr = JsonConvert.SerializeObject(jsonObj);
                                        file = filemenager.CreateNewFile(fileName, jsonStr);
                                        Console.WriteLine(DataInterface.choiceSendEmailEN);
                                    }
                                    menu.Chioce();

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
                                    }
                                    else
                                    {
                                        Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
                                    }
                                    menu.Chioce();

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
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceEN);
                            }
                            var place = Console.ReadLine();
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni");
                            }
                            else
                            {
                                Console.WriteLine("Enter value of required humidity for the last 5 days");
                            }
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
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.insertNamePlaceEN);
                            }
                            place = Console.ReadLine();
                            string date, time;
                            DateTime printDate = DateTime.Now;
                            str = printDate.Date.ToString(formatDateForFile);
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
                            {
                                Console.WriteLine(DataInterface.successIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.successEN);
                            }
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
                            {
                                Console.WriteLine(DataInterface.successIT);
                            }
                            else
                            {
                                Console.WriteLine(DataInterface.successEN);
                            }
                            break;
                        case "4":
                            break;
                    }
                    break;
                case "4":
                    if (menuLang == "it")
                    {
                        Console.WriteLine("Inserisci nome file da eliminare, con tipo di estensione (nomefile.estensione)");
                    }
                    else
                    {
                        Console.WriteLine("Enter the name of the file to be deleted, with the extension (filename.extension)");
                    }
                    var fileNameDelete = Console.ReadLine();
                    filemenager.DeleteFile(fileNameDelete);
                    break;
                case "5":
                    if (menuLang == "it")
                    {
                        Console.WriteLine("Inserisci il nome file da inviare tramite email");
                    }
                    else
                    {
                        Console.WriteLine("Enter the file name to be sent via email");
                    }
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
                    menu.ShowMenuCreateXlsFile();

                    var choiceXls = Console.ReadLine();
                    switch (choiceXls)
                    {
                        case "1":
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati");
                            }
                            else
                            {
                                Console.WriteLine("Enter the name of the file from which to get the data");
                            }
                            var todaySourceFile = Console.ReadLine();
                            var reciveDate = DateTime.Now;
                            var dateTimeForFile = reciveDate.Date.ToString(formatDateForFile);
                            var todayFilePath = Path.Combine(DataInterface.filePath, todaySourceFile);
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci il nome del file XLS");
                            }
                            else
                            {
                                Console.WriteLine("Enter the name of the XLS file");
                            }
                            var todayXlsFile = Console.ReadLine();

                            createXlsFromFile.CreateXlsFromFileForToday(todayFilePath, todayXlsFile, dateTimeForFile);
                            break;
                        case "2":
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci il nome del file dal quale ricavare i dati");
                            }
                            else
                            {
                                Console.WriteLine("Enter the name of the file from which to get the data");
                            }
                            var fiveDaysSourceFile = Console.ReadLine();
                            reciveDate = DateTime.Now;
                            dateTimeForFile = reciveDate.Date.ToString(formatDateForFile);
                            var fiveDaysFilePath = Path.Combine(DataInterface.filePath, fiveDaysSourceFile);
                            if (menuLang == "it")
                            {
                                Console.WriteLine("Inserisci il nome del file XLS");
                            }
                            else
                            {
                                Console.WriteLine("Enter the name of the XLS file");
                            }
                            var fiveDaysXlsFile = Console.ReadLine();
                            createXlsFromFile.CreateXlsFromFileFor5Days(fiveDaysFilePath, fiveDaysXlsFile, dateTimeForFile);
                            break;
                        case "3":
                            menu.ShowSecondMenu();

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
