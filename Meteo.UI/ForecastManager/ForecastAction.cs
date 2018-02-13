using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Meteo.Services.Models;
using Newtonsoft.Json;
using Meteo.ExcelManager;
using System.Collections.Generic;

namespace Meteo.UI.ForecastManager
{
    public class ForecastAction
    {
        public IMeteoApiService _meteoApiService;
        public static IQueryBuilder _queryBuilder;
        public static Menu _menu;
        public EmailManager _emailManager;
        public FileMenager _filemenager;
        public CreateXlsFile _createXlsFile;
        public string _fileName, _menuLang, _measureUnit, _extensionJson = ".json", _extensionXls = ".xls", _lat, _lon, _place;
        public int _idUser;
        public ForecastInteractions _aunthenticationUserInterface;
        public static DateTime _reciveDate = DateTime.Now;
        private IService _exit;
        private IPrintingService _printService;
        public string _dateTimeForFile = _reciveDate.Date.ToString("yyyy-MM-dd");
        public string _country;

        public ForecastAction(string lang, IService exit, IPrintingService printService, IMeteoApiService meteoApiService, IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
            _exit = exit;
            _menuLang = lang;
            _meteoApiService = meteoApiService;
            _printService = printService;
            _menu = new Menu(queryBuilder, _menuLang, _exit);
            _aunthenticationUserInterface = new ForecastInteractions(_menuLang, _menu);
            _filemenager = new FileMenager();
            _emailManager = new EmailManager();
            _createXlsFile = new CreateXlsFile();
        }
        public void Actions(string username, string measureUnit)
        {
            _measureUnit = measureUnit;
            var OneDayOr5Days = "";
            var searchingFor = "";
            var choseThisDay = "";
            var choseLast5Day = "";
            _idUser = _queryBuilder.GetUser(username).IdUser;

            var choiceSelect = _menu.ShowFirst();
            switch (choiceSelect)
            {
                case "1":
                    var parametersNotSplitted = _menu.ShowSecondMenu();
                    choseThisDay = ChoiceSelected(parametersNotSplitted);

                    var readDataAndValidate1Day = new UserSearchInput(parametersNotSplitted);


                    switch (choseThisDay)
                    {
                        case "1":
                            OneDayOr5Days = "1Day";
                            searchingFor = "city";

                            GetPlace(readDataAndValidate1Day);




                            ProcessRequestsForecasts(_place, null, null, searchingFor, OneDayOr5Days, username, _country);
                            break;
                        case "2":
                            OneDayOr5Days = "1Day";
                            searchingFor = "coordinates";
                            GetCoordinates(readDataAndValidate1Day);

                            try
                            {
                                ProcessRequestsForecasts(null, _lat, _lon, searchingFor, OneDayOr5Days, username, null);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "3":
                            break;
                    }
                    break;
                case "2":
                    parametersNotSplitted = _menu.ShowSecondMenu();
                    choseLast5Day = ChoiceSelected(parametersNotSplitted);
                    var readDataAndValidate5Day = new UserSearchInput(parametersNotSplitted);



                    switch (choseLast5Day)
                    {


                        case "1":
                            try
                            {
                                OneDayOr5Days = "5Days";
                                searchingFor = "city";
                                GetPlace(readDataAndValidate5Day);

                                ProcessRequestsForecasts(_place, null, null, searchingFor, OneDayOr5Days, username, _country);
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "2":
                            OneDayOr5Days = "5Days";
                            searchingFor = "coordinates";
                            GetCoordinates(readDataAndValidate5Day);

                            try
                            {
                                ProcessRequestsForecasts(null, _lat, _lon, searchingFor, OneDayOr5Days, username, null);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                    }
                    break;
                case "3":
                    var choseFilter = _menu.ShowFiltredMenu();
                    switch (choseFilter)
                    {
                        case "1":
                            _place = _aunthenticationUserInterface.InsertNamePlace();

                            try
                            {
                                var dataFiltredByCity = _queryBuilder.FilterSearcheByCity(_place, _idUser);
                                _printService.PrintDataFiltred(dataFiltredByCity);
                                _aunthenticationUserInterface.RequestSucces();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "2":
                            _place = _aunthenticationUserInterface.InsertNamePlace();
                            var dateFrom = _aunthenticationUserInterface.ReadDate();
                            var dateTo = _aunthenticationUserInterface.ReadDate();
                            var dataFiltredByDate = _queryBuilder.GetForecastFilteredByDate(dateFrom, dateTo, _idUser);
                            _printService.PrintDataFiltred(dataFiltredByDate);
                            _aunthenticationUserInterface.RequestSucces();
                            break;
                        case "3":
                            _menu.ShowFirst();
                            break;
                    }
                    break;
                case "4":
                    _fileName = _aunthenticationUserInterface.InsertNameFile(null, null, null);
                    _filemenager.DeleteFile(_fileName);
                    break;
                case "5":
                    _fileName = _aunthenticationUserInterface.InsertNameFile(null, null, null);
                    _aunthenticationUserInterface.AuthenticationUserInterfaceSendEmail();
                    _emailManager.AttempsPasswordAndSendEmail(_fileName, _aunthenticationUserInterface.senderValue, _aunthenticationUserInterface.receiverValue, _aunthenticationUserInterface.bodyValue, _aunthenticationUserInterface.subjectValue, _aunthenticationUserInterface.userValue, _aunthenticationUserInterface.password);
                    break;
            }
        }



        public void ProcessRequestsForecasts(string place, string lat, string lon, string requestFor, string OneDayOr5DaysChoice, string username, string country)
        {
            var choiceSelected = "";
            var jsonObj = ReciveJsonObj(lat, lon, place, OneDayOr5DaysChoice);
            var insertChoiceSelected = _aunthenticationUserInterface.MeteoChoice(requestFor, OneDayOr5DaysChoice);

            PrintData(jsonObj, OneDayOr5DaysChoice);

            var lastInsertedMasterId = InsertData(jsonObj, OneDayOr5DaysChoice, place, lat, lon, insertChoiceSelected, username);
            _aunthenticationUserInterface.RequestSucces();

            choiceSelected = _aunthenticationUserInterface.ChoiceDoFileJson();
            if (choiceSelected == "1")
            {
                _fileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionJson, OneDayOr5DaysChoice);
                var jsonStr = JsonConvert.SerializeObject(jsonObj);
                var file = _filemenager.CreateNewFile(_fileName, jsonStr);
                _aunthenticationUserInterface.RequestSucces();

                choiceSelected = _aunthenticationUserInterface.ChoceSendEmail();
                if (choiceSelected == "1")
                {
                    _aunthenticationUserInterface.AuthenticationUserInterfaceSendEmail();
                    _emailManager.AttempsPasswordAndSendEmail(_fileName, _aunthenticationUserInterface.senderValue, _aunthenticationUserInterface.receiverValue, _aunthenticationUserInterface.bodyValue, _aunthenticationUserInterface.subjectValue, _aunthenticationUserInterface.userValue, _aunthenticationUserInterface.passwordMaskered);
                    _aunthenticationUserInterface.RequestSucces();
                }
            }

            choiceSelected = _aunthenticationUserInterface.ChoiceDoFileXls();
            if (choiceSelected == "1")
            {
                var xlsFileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, OneDayOr5DaysChoice);
                ChoiceCreateFileXlsOneDayOr5Days(lat, lon, place, OneDayOr5DaysChoice, xlsFileName, _dateTimeForFile, lastInsertedMasterId);
                _aunthenticationUserInterface.RequestSucces();
            }
        }

        private dynamic ReciveJsonObj(string lat, string lon, string place, string OneDayOr5Days)
        {
            if (lat != null && OneDayOr5Days == "1Day")
            {
                var obj = _meteoApiService.ProcessMeteoForToday(null, lon, lat, _measureUnit).Result;
                return obj;
            }
            else if (place != null && OneDayOr5Days == "1Day")
            {
                var jsonObj = _meteoApiService.ProcessMeteoForToday(place, null, null, _measureUnit).Result;
                return jsonObj;
            }
            else if (place != null && OneDayOr5Days == "5Days")
            {
                var json = _meteoApiService.ProcessMeteoForFiveDays(place, null, null, _measureUnit).Result;
                return json;
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                var json = _meteoApiService.ProcessMeteoForFiveDays(null, lat, lon, _measureUnit).Result;
                return json;
            }
            _aunthenticationUserInterface.Exit();
            return null;
        }
        private void PrintData(dynamic jsonObj, string OneDayOr5Days)
        {
            if (OneDayOr5Days == "1Day")
            {
                _printService.PrintForData(jsonObj, _menuLang);
            }

            else if (OneDayOr5Days == "5Days")
            {
                _printService.PrintDataFor5Days(jsonObj, _menuLang);
            }
        }
        private long InsertData(dynamic jsonObj, string OneDayOr5Days, string place, string lat, string lon, string insertChoiceSelected, string username)
        {
            long lastInsertedMasterId = 0L;
            var dateOfRequist = _reciveDate.ToString("yyyy-MM-dd HH:mm:ss");
            var cityName = _queryBuilder.GetCityData(lat, lon, place).Name;
            var idCity = _queryBuilder.GetCityData(lat, lon, place).Id;
            lastInsertedMasterId = _queryBuilder.InsertDataMaster(insertChoiceSelected, _idUser, dateOfRequist, idCity);

            if (OneDayOr5Days == "1Day")
            {
                _queryBuilder.InsertDataIntoForecastTable(jsonObj, cityName, lastInsertedMasterId, idCity, OneDayOr5Days, dateOfRequist);
            }
            else if (OneDayOr5Days == "5Days")
            {
                _queryBuilder.InsertDataIntoForecastTable(jsonObj, cityName, lastInsertedMasterId, idCity, OneDayOr5Days, dateOfRequist);
            }
            return lastInsertedMasterId;
        }
        private void ChoiceCreateFileXlsOneDayOr5Days(string lat, string lon, string place, string OneDayOr5Days, string xlsFile, string dateTime, long lastInsertedMasterId)
        {
            List<Forecast> forecastData = _queryBuilder.GetForecastDataByLastInsertedId(lastInsertedMasterId);
            if (lat != null && OneDayOr5Days == "1Day")
            {
                _createXlsFile.CreateXlsFileWithForecastData(forecastData, place, null, null, xlsFile, dateTime, OneDayOr5Days);
            }
            else if (place != null && OneDayOr5Days == "1Day")
            {
                _createXlsFile.CreateXlsFileWithForecastData(forecastData, null, lat, lon, xlsFile, dateTime, OneDayOr5Days);
            }
            else if (place != null && OneDayOr5Days == "5Days")
            {
                _createXlsFile.CreateXlsFileWithForecastData(forecastData, place, null, null, xlsFile, dateTime, OneDayOr5Days);
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                _createXlsFile.CreateXlsFileWithForecastData(forecastData, null, lat, lon, xlsFile, dateTime, OneDayOr5Days);
            }
        }
        public void CreateXlsWithExportedData(string exportChoice, string username)
        {
            dynamic forecastResearch;
            if (exportChoice == "1")
            {
                forecastResearch = _queryBuilder.GetUserForecastResearch(_idUser);
                _fileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, null);
                _createXlsFile.CreateXlsFileWithExportedData(forecastResearch, _fileName, _dateTimeForFile, exportChoice);
            }
            else if (exportChoice == "2")
            {
                var startDate = _aunthenticationUserInterface.InsertStartDate();
                var endDate = _aunthenticationUserInterface.InsertEndDate();
                forecastResearch = _queryBuilder.GetForecastFilteredByDate(startDate, endDate, _idUser);
                _createXlsFile.CreateXlsFileWithExportedData(forecastResearch, _fileName, _dateTimeForFile, exportChoice);
            }
        }
        public string ChoiceSelected(string parametersNotSplitted)
        {
            var choseSelected = "";
            var paramSplitted = parametersNotSplitted.Split("=");
            if (paramSplitted[0] == "place")
            {
                choseSelected = "1";
            }
            else
                choseSelected = "2";
            return choseSelected;

        }
        public void GetPlace(UserSearchInput readDataAndValidate)
        {

            readDataAndValidate.GetResponse();

            _place = readDataAndValidate.city.Name;
            _country = readDataAndValidate.city.Country;
            if (!string.IsNullOrEmpty(_country))
            {
                var app = $",{_country}";
                _country = app; 

            }

        }


        public void GetCoordinates(UserSearchInput readDataAndValidate)
        {
            readDataAndValidate.GetResponse();
            _lat = Convert.ToString(readDataAndValidate.coordate.Latitude);
            _lon = Convert.ToString(readDataAndValidate.coordate.Longitude);

        }

    }
}
