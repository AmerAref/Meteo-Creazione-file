using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Newtonsoft.Json;
using Meteo.ExcelManager;
using System.IO;
using System.Collections.Generic;

namespace Meteo.UI.AuthenticationUser
{
    public class AuthenticatedUser
    {

        public string _fileName;
        public MeteoApi meteoApi;
        public PrintData printData;
        public IQueryBuilder queryBuilder;
        public Menu menu;
        public EmailManager emailManager;
        public PrintData print;
        public FileMenager filemenager;
        public CreateXlsFile createXlsFile;
        public AuthenticationUserInterface aunthenticationUserInterface;
        public string _menuLang;
        public string _measureUnit;
        public int _idUserMaster;







        public void ForecastActions(string username, string choiceSelect, string menuLang, string measureUnit)

        {


            _measureUnit = measureUnit;
            _menuLang = menuLang;
            var OneDayOr5Days = "";
            var formatDateForFile = "";
            var createXlsFromFile = new CreateXlsFromFiles();
            var searchingFor = "";
            _idUserMaster = queryBuilder.GetUser(username).IdUser;

            switch (choiceSelect)
            {
                case "1":
                    menu.ShowSecondMenu();
                    var choseThisDay = Console.ReadLine();
                    switch (choseThisDay)
                    {
                        case "1":
                            var place = aunthenticationUserInterface.InsertNamePlace();
                            try
                            {
                                OneDayOr5Days = "1Day";
                                searchingFor = "city";
                                UserActions(place, null, null, searchingFor, OneDayOr5Days);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case "2":
                            OneDayOr5Days = "1Day";

                            string lon, lat;
                            searchingFor = "coordinates";
                            var coordinate = new AuthenticationUserInterface.Coordinate();
                            var readCoordiate = coordinate.ReadCoordinate();
                            lat = readCoordiate.Lat;
                            lon = readCoordiate.Lon;
                            try
                            {
                                UserActions(null, lat, lon, searchingFor, OneDayOr5Days);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "3":
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                    }
                    break;
                case "2":
                    menu.ShowSecondMenu();
                    var choseLast5Day = Console.ReadLine();
                    switch (choseLast5Day)
                    {
                        case "1":
                            var place = aunthenticationUserInterface.InsertNamePlace();
                            try
                            {

                                OneDayOr5Days = "5Days";
                                var extension = ".json";
                                searchingFor = "place";
                                UserActions(place, null, null, searchingFor, OneDayOr5Days);


                                break;
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);

                            }
                            break;
                        case "2":
                            OneDayOr5Days = "5Days";

                            var coordinate = new AuthenticationUserInterface.Coordinate();
                            var readCoordiate = coordinate.ReadCoordinate();
                            var lat = readCoordiate.Lat;
                            var lon = readCoordiate.Lon;
                            try
                            {
                                UserActions(null, lat, lon, searchingFor, OneDayOr5Days);


                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "3":
                            menu.ShowFiltredMenu();

                            var choseFilter = Console.ReadLine();
                            switch (choseFilter)
                            {
                                case "1":
                                    place = aunthenticationUserInterface.InsertNamePlace();
                                    var humidity = aunthenticationUserInterface.ReadHumidityValue();

                                    try
                                    {
                                        var objFilteredForHumidity = meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Result;
                                        print.PrintFilteredDataHumidity(objFilteredForHumidity, menuLang);

                                        aunthenticationUserInterface.RequestSucces();
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case "2":
                                    place = aunthenticationUserInterface.InsertNamePlace();

                                    place = Console.ReadLine();
                                    string time;
                                    DateTime printDate = DateTime.Now;

                                    var dataPrinted = printDate.Date.ToString(formatDateForFile);
                                    var date = Convert.ToString(aunthenticationUserInterface.ReadDataTime());

                                    time = aunthenticationUserInterface.ReadTime();
                                    var objFilteredForTimeDate = meteoApi.FiltredMeteoByDateTimeLast5Day(date, time, place).Result;
                                    print.PrintDataLast5Day(objFilteredForTimeDate, menuLang);
                                    aunthenticationUserInterface.RequestSucces();
                                    break;
                                case "3":
                                    place = aunthenticationUserInterface.InsertNamePlace();

                                    var typeWeather = aunthenticationUserInterface.ReadQualitySky();
                                    var jsonObj = meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place);
                                    aunthenticationUserInterface.RequestSucces();
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case "4":
                            Console.WriteLine("Con estensione");
                            _fileName = aunthenticationUserInterface.InsertNameFile(null, null, null);
                            filemenager.DeleteFile(_fileName);
                            break;
                        case "5":
                            _fileName = aunthenticationUserInterface.InsertNameFile(null, null, null);
                            emailManager.AttempsPasswordAndSendEmail(_fileName, aunthenticationUserInterface.senderValue, aunthenticationUserInterface.receiverValue, aunthenticationUserInterface.bodyValue, aunthenticationUserInterface.subjectValue, aunthenticationUserInterface.userValue, aunthenticationUserInterface.password);
                            break;
                        case "6":
                            menu.ShowMenuCreateXlsFile();
                            var choiceXls = aunthenticationUserInterface.ChoiceDoFileXls();
                            switch (choiceXls)
                            {
                                case "1":
                                    var todaySourceFile = aunthenticationUserInterface.InsertNameFile(null, null, null);
                                    var reciveDate = DateTime.Now;
                                    var dateTimeForFile = reciveDate.Date.ToString(formatDateForFile);
                                    var todayFilePath = Path.Combine(DataInterface.filePath, todaySourceFile);
                                    var todayXlsFile = aunthenticationUserInterface.InsertNameFile(null, null, null);
                                    createXlsFromFile.CreateXlsFromFileForToday(todayFilePath, todayXlsFile, dateTimeForFile);
                                    break;
                                case "2":
                                    var fiveDaysSourceFile = aunthenticationUserInterface.InsertNameFile(null, null, null);
                                    reciveDate = DateTime.Now;
                                    dateTimeForFile = reciveDate.Date.ToString(formatDateForFile);
                                    var fiveDaysFilePath = Path.Combine(DataInterface.filePath, fiveDaysSourceFile);
                                    var fiveDaysXlsFile = aunthenticationUserInterface.InsertNameFile(null, null, null);
                                    createXlsFromFile.CreateXlsFromFileFor5Days(fiveDaysFilePath, fiveDaysXlsFile, dateTimeForFile);
                                    break;
                                case "3":
                                    menu.ShowSecondMenu();
                                    break;
                                case "4":
                                    aunthenticationUserInterface.Exit();
                                    break;
                            }
                            break;
                        case "7":
                            aunthenticationUserInterface.Exit();

                            break;
                    }
                    break;
            }

        }
        public void UserActions(string place, string lat, string lon, string requestFor, string OneDayOr5DaysChoice)
        {
            var extension = ".json";
            var choiceSelected = "";
            var formatDateForFile = " ";

            DateTime dateForFile = DateTime.Now;
            var dataPrinted = dateForFile.Date.ToString(formatDateForFile);

            var jsonObj = ReciveJsonObj(lat, lon, place, OneDayOr5DaysChoice);
            print.PrintForData(jsonObj, _menuLang);
            queryBuilder.InsertOneDayForecast(jsonObj);
            var insertChoiceSelected = aunthenticationUserInterface.MeteoChoice1Day(requestFor);
            queryBuilder.InsertDataMaster(insertChoiceSelected, _idUserMaster);
            aunthenticationUserInterface.RequestSucces();
            choiceSelected = aunthenticationUserInterface.ChoiceDoFileJson();
            if (choiceSelected == "1")
            {
                _fileName = aunthenticationUserInterface.InsertNameFile(dataPrinted, extension, OneDayOr5DaysChoice);
                var jsonStr = JsonConvert.SerializeObject(jsonObj);
                var file = filemenager.CreateNewFile(_fileName, jsonStr);
                aunthenticationUserInterface.RequestSucces();
                choiceSelected = aunthenticationUserInterface.ChoceSendEmail();
                if (choiceSelected == "1")
                {
                    aunthenticationUserInterface = new AuthenticationUserInterface();
                    emailManager.AttempsPasswordAndSendEmail(_fileName, aunthenticationUserInterface.senderValue, aunthenticationUserInterface.receiverValue, aunthenticationUserInterface.bodyValue, aunthenticationUserInterface.subjectValue, aunthenticationUserInterface.userValue, aunthenticationUserInterface.passwordMaskered);
                }
                choiceSelected = aunthenticationUserInterface.ChoiceDoFileXls();
                if (choiceSelected == "1")
                {
                    extension = "xls";
                    var xlsFileName = aunthenticationUserInterface.InsertNameFile(dataPrinted, extension, OneDayOr5DaysChoice);
                    createXlsFile.CreateXlsFileForToday(jsonObj, place, xlsFileName, dataPrinted);
                }
                else
                {
                    aunthenticationUserInterface.RequestSucces();
                }
            }
            else
            {
                aunthenticationUserInterface.RequestSucces();
            }

        }


        private dynamic ReciveJsonObj(string lat, string lon, string place, string OneDayOr5Days)
        {

            if (lat != null && OneDayOr5Days == "1Day")
            {


                var obj = meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, _measureUnit).Result;
                return obj;
            }


            else if (place != null && OneDayOr5Days == "1Day")
            {
                var jsonObj = meteoApi.ProcessMeteoByPlaceToday(place, _measureUnit).Result;
                return jsonObj;


            }
            else if (place != null && OneDayOr5Days == "5Days")
            {
                var json = meteoApi.ProcessMeteoByPlaceLast5Day(place, _measureUnit);
                return json;
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                var json = meteoApi.ProcessMeteoByCoordinatesLast5Day(lat, lon, _measureUnit);

                return json;
            }
            aunthenticationUserInterface.Exit();
            return null;
        }


    }
}

