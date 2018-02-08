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
        public MeteoApi _meteoApi;
        public static IQueryBuilder _queryBuilder;
        public static Menu _menu;
        public EmailManager _emailManager;
        public FileMenager _filemenager;
        public CreateXlsFile _createXlsFile;
        public string _fileName, _menuLang, _measureUnit, _extensionJson = ".json", _extensionXls = ".xls", _lat, _lon, _place;
        public int _idUserMaster;
        public ForecastInteractions _aunthenticationUserInterface;
        public static DateTime _reciveDate = DateTime.Now;
        private IService _exit;
        private IPrintingService _printService;
        public string _dateTimeForFile = _reciveDate.Date.ToString("yyyy-MM-dd");

        public ForecastAction(string lang, IService exit, IPrintingService printService, IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
            _exit = exit;
            _menuLang = lang;
            _meteoApi = new MeteoApi();
            _printService = printService;
            _menu = new Menu(queryBuilder, _menuLang, _exit);
            _aunthenticationUserInterface = new ForecastInteractions(_menuLang, _menu);
            _filemenager = new FileMenager();
            _emailManager = new EmailManager();
            _createXlsFile = new CreateXlsFile();
        }
        public void Actions(string username, string measureUnit)
        {
            //_aunthenticationUserInterface.GetMenuLang(menuLang); dovrebbe funzionare senza dato che la passo come proprieta 
            _measureUnit = measureUnit;
            var OneDayOr5Days = "";
            var searchingFor = "";
            var choseThisDay = "";
            var choseLast5Day = "";
            _idUserMaster = _queryBuilder.GetUser(username).IdUser;

            var choiceSelect = _menu.ShowFirst();
            switch (choiceSelect)
            {
                case "1":
                    choseThisDay = GetDecisionAndParam();

                    switch (choseThisDay)
                    {
                        case "1":
                            OneDayOr5Days = "1Day";
                            searchingFor = "city";

                            ProcessRequestsForecasts(_place, null, null, searchingFor, OneDayOr5Days, username);
                            break;
                        case "2":
                            OneDayOr5Days = "1Day";
                            searchingFor = "coordinates";

                            try
                            {
                                ProcessRequestsForecasts(null, _lat, _lon, searchingFor, OneDayOr5Days, username);
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
                    choseLast5Day = GetDecisionAndParam();

                    switch (choseLast5Day)
                    {
                        case "1":
                            try
                            {
                                OneDayOr5Days = "5Days";
                                searchingFor = "city";
                                ProcessRequestsForecasts(_place, null, null, searchingFor, OneDayOr5Days, username);
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
                            try
                            {
                                ProcessRequestsForecasts(null, _lat, _lon, searchingFor, OneDayOr5Days, username);
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
                            var humidity = _aunthenticationUserInterface.ReadHumidityValue();

                            try
                            {
                                var objFilteredForHumidity = _meteoApi.FilteredMeteoByHumidityNext5Day(humidity, _place).Result;
                                _printService.PrintFilteredDataHumidity(objFilteredForHumidity, _menuLang);

                                _aunthenticationUserInterface.RequestSucces();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "2":
                            _place = _aunthenticationUserInterface.InsertNamePlace();
                            var time = _aunthenticationUserInterface.ReadTime();
                            var readDate = _aunthenticationUserInterface.ReadDate();
                            var objFilteredForTimeDate = _meteoApi.FilteredMeteoByDateTimeNext5Day(readDate, time, _place).Result;
                            _printService.PrintDataFor5Days(objFilteredForTimeDate, _menuLang);
                            _aunthenticationUserInterface.RequestSucces();
                            break;
                        case "3":
                            _place = _aunthenticationUserInterface.InsertNamePlace();

                            var typeWeather = _aunthenticationUserInterface.ReadQualitySky();
                            var jsonObj = _meteoApi.FilteredMeteoByWeatherNext5Day(typeWeather, _place);
                            _aunthenticationUserInterface.RequestSucces();
                            break;
                        case "4":
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
                case "6":
                    var exportChoice = _menu.ShowExportMenu();
                    switch (exportChoice)
                    {
                        case "1":
                            CreateXlsWithExportedData(exportChoice, username);
                            break;
                        case "2":
                            CreateXlsWithExportedData(exportChoice, username);
                            break;
                        case "3":
                            _menu.ShowFirst();
                            break;
                    }
                    break;
            }
        }

        private string GetDecisionAndParam()
        {
            var ChoseSelected = "";
            var parameters = _menu.ShowSecondMenu();
            string[] substrings = parameters.Split('=');

            if (substrings[0] == "place")
            {
                _place = substrings[1];
                ChoseSelected = "1";
            }

            if (substrings[0] == "lat" || substrings[0] == "lon")
            {
                _lon = substrings[2];
                var latt = substrings[1].Split("&");
                _lat = latt[0];

                ChoseSelected = "2";
            }
            return ChoseSelected;
        }

        public void ProcessRequestsForecasts(string place, string lat, string lon, string requestFor, string OneDayOr5DaysChoice, string username)
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
                var obj = _meteoApi.ProcessMeteoByCoordinatesToday(lon, lat, _measureUnit).Result;
                return obj;
            }
            else if (place != null && OneDayOr5Days == "1Day")
            {
                var jsonObj = _meteoApi.ProcessMeteoByPlaceToday(place, _measureUnit).Result;
                return jsonObj;
            }
            else if (place != null && OneDayOr5Days == "5Days")
            {
                var json = _meteoApi.ProcessMeteoNextFiveDays(place, _measureUnit).Result;
                return json;
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                var json = _meteoApi.ProcessMeteoByCoordinatesNextFiveDays(lat, lon, _measureUnit).Result;
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
            lastInsertedMasterId = _queryBuilder.InsertDataMaster(insertChoiceSelected, _idUserMaster, dateOfRequist, idCity);

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
                forecastResearch = _queryBuilder.GetUserForecastResearch(_idUserMaster);
                _fileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, null);
                _createXlsFile.CreateXlsFileWithExportedData(forecastResearch, _fileName, _dateTimeForFile, exportChoice);
            }
            else if (exportChoice == "2")
            {
                var startDate = _aunthenticationUserInterface.InsertStartDate();
                var endDate = _aunthenticationUserInterface.InsertEndDate();
                forecastResearch = _queryBuilder.GetForecastFilteredByDate(startDate, endDate, _idUserMaster);
                _createXlsFile.CreateXlsFileWithExportedData(forecastResearch, _fileName, _dateTimeForFile, exportChoice);
            }
        }
        public void ControlCoordinates(string lat, string lon)
        {
            var latUserInput = new CoordinatesUserInput(lat);
            var lonUserInput = new CoordinatesUserInput(lon);
            var authenticateLat = latUserInput.GetResponse();
            var authenticateLon = lonUserInput.GetResponse();
            var coordsReady = true;
            while (coordsReady)
            {
                try
                {
                    if (authenticateLat != null && authenticateLon != null)
                    {
                        coordsReady = false;
                    }
                    else
                    {
                        Console.WriteLine("Coordinate sbagliate");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    coordsReady = false;
                }
            }
        }
    }
}