using System;
using Meteo.Services;
using Meteo.Services.Infrastructure;
using Newtonsoft.Json;
using Meteo.ExcelManager;
using System.IO;
using Meteo.UI.AuthenticationUser;

namespace Meteo.UI.ForecastManager
{
    public class ForecastAction
    {
        public string _fileName;
        public MeteoApi _meteoApi;
        public PrintData _printData;
        public static IQueryBuilder queryBuilder = QueryBuilderServices.QueryBuilder();
        public static Menu _menu;
        public EmailManager _emailManager;
        public FileMenager _filemenager;
        public CreateXlsFile _createXlsFile;
        public string _menuLang;
        public string _measureUnit;
        public int _idUserMaster;
        public ForecastManagerUI _aunthenticationUserInterface;
        public CreateXlsFromFiles _createXlsFromFile;
        public CoordinatesManager _coordinate;
        public static DateTime _reciveDate = DateTime.Now;
        public string _dateTimeForFile = _reciveDate.Date.ToString("yyyy-MM-dd");
        public string _extensionJson = ".json", _extensionXls = "xls";
        public ForecastAction(string lang)
        {
            _menuLang = lang;
            _meteoApi = new MeteoApi();
            _printData = new PrintData();
            _menu = new Menu(queryBuilder, _menuLang);
            _aunthenticationUserInterface = new ForecastManagerUI(_menuLang, _menu);
            _filemenager = new FileMenager();
            _emailManager = new EmailManager();
            _createXlsFile = new CreateXlsFile();
            _createXlsFromFile = new CreateXlsFromFiles();
            _coordinate = new CoordinatesManager();
        }
        public void Actions(string username, string measureUnit)
        {
            //_aunthenticationUserInterface.GetMenuLang(menuLang); dovrebbe funzionare senza dato che la passo come proprieta 
            var place = "";
            _measureUnit = measureUnit;
            var OneDayOr5Days = "";
            var fileName = "";
            var searchingFor = "";
            var lat = "";
            var lon = "";
            var choseThisDay = "";
            var choseLast5Day = "";
            _idUserMaster = queryBuilder.GetUser(username).IdUser;

            _menu.ShowFirst();
            var choiceSelect = Console.ReadLine();
            switch (choiceSelect)
            {
                case "1":
                    _menu.ShowSecondMenu();
                    choseThisDay = Console.ReadLine();
                    switch (choseThisDay)
                    {
                        case "1":
                            place = _aunthenticationUserInterface.InsertNamePlace();

                            OneDayOr5Days = "1Day";
                            searchingFor = "city";

                            ProcessRequestsForecasts(place, null, null, searchingFor, OneDayOr5Days);
                            break;

                        case "2":
                            OneDayOr5Days = "1Day";

                            searchingFor = "coordinates";
                            var readCoordiate = _coordinate.ReadCoordinate();
                            lat = readCoordiate.Lat;
                            lon = readCoordiate.Lon;
                            try
                            {
                                ProcessRequestsForecasts(null, lat, lon, searchingFor, OneDayOr5Days);
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
                    _menu.ShowSecondMenu();
                    choseLast5Day = Console.ReadLine();
                    switch (choseLast5Day)
                    {
                        case "1":
                            place = _aunthenticationUserInterface.InsertNamePlace();
                            try
                            {
                                OneDayOr5Days = "5Days";
                                searchingFor = "city";

                                ProcessRequestsForecasts(place, null, null, searchingFor, OneDayOr5Days);
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "2":
                            OneDayOr5Days = "5Days";

                            var readCoordiate = _coordinate.ReadCoordinate();
                            lat = readCoordiate.Lat;
                            lon = readCoordiate.Lon;
                            try
                            {
                                ProcessRequestsForecasts(null, lat, lon, searchingFor, OneDayOr5Days);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                    }
                    break;
                case "3":
                    _menu.ShowFiltredMenu();
                    var choseFilter = Console.ReadLine();
                    switch (choseFilter)
                    {
                        case "1":
                            place = _aunthenticationUserInterface.InsertNamePlace();
                            var humidity = _aunthenticationUserInterface.ReadHumidityValue();

                            try
                            {
                                var objFilteredForHumidity = _meteoApi.FiltredMeteoByHumidityLast5Day(humidity, place).Result;
                                _printData.PrintFilteredDataHumidity(objFilteredForHumidity, _menuLang);

                                _aunthenticationUserInterface.RequestSucces();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "2":
                            place = _aunthenticationUserInterface.InsertNamePlace();

                            var time = _aunthenticationUserInterface.ReadTime();
                            var readDate = _aunthenticationUserInterface.ReadDate();
                            var objFilteredForTimeDate = _meteoApi.FiltredMeteoByDateTimeLast5Day(readDate, time, place).Result;
                            _printData.PrintDataLast5Day(objFilteredForTimeDate, _menuLang);
                            _aunthenticationUserInterface.RequestSucces();
                            break;
                        case "3":
                            place = _aunthenticationUserInterface.InsertNamePlace();

                            var typeWeather = _aunthenticationUserInterface.ReadQualitySky();
                            var jsonObj = _meteoApi.FiltredMeteoByWeatherLast5Day(typeWeather, place);
                            _aunthenticationUserInterface.RequestSucces();
                            break;
                        case "4":
                            _menu.ShowFirst();
                            break;
                        case "5":
                            _aunthenticationUserInterface.Exit();
                            break;
                    }
                    break;
                case "4":
                    _fileName = _aunthenticationUserInterface.InsertNameFile(null, null, null);
                    _filemenager.DeleteFile(_fileName);
                    break;
                case "5":
                    _fileName = _aunthenticationUserInterface.InsertNameFile(null, null, null);
                    _emailManager.AttempsPasswordAndSendEmail(_fileName, _aunthenticationUserInterface.senderValue, _aunthenticationUserInterface.receiverValue, _aunthenticationUserInterface.bodyValue, _aunthenticationUserInterface.subjectValue, _aunthenticationUserInterface.userValue, _aunthenticationUserInterface.password);
                    break;
                case "6":
                    _menu.ShowMenuCreateXlsFile();
                    var choiceXls = _aunthenticationUserInterface.ChoiceDoFileXls();
                    switch (choiceXls)
                    {
                        case "1":
                            CreateXlsFromFileUserAction(choiceXls);
                            break;
                        case "2":
                            CreateXlsFromFileUserAction(choiceXls);
                            break;
                        case "3":
                            _menu.ShowSecondMenu();
                            break;
                        case "4":
                            _aunthenticationUserInterface.Exit();
                            break;
                    }
                    break;
                case "7":
                    fileName = "";
                    var forecastResearchOneDay = queryBuilder.GetForecastUserOneDayResearch(username);
                    var forecastResearchNext5Days = queryBuilder.GetForecastUserNext5DaysResearch(username);
                    var exportChoice = _menu.ShowExportMenu();
                    switch (exportChoice)
                    {
                        case "1":
                            var researchOneDay = queryBuilder.GetOneDayUserResearch(username);
                            fileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, null);
                            _createXlsFile.CreateXlsFileWithExportedOneDayData(researchOneDay, forecastResearchOneDay, fileName, _dateTimeForFile);
                            break;
                        case "2":
                            var researchNextFiveDays = queryBuilder.GetNextFiveDaysUserResearch(username);
                            fileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, null);
                            _createXlsFile.CreateXlsFileWithExportedNext5DaysData(researchNextFiveDays, forecastResearchNext5Days, fileName, _dateTimeForFile);
                            break;
                        case "3":
                            break;
                        case "4":
                            _menu.ShowFirst();
                            break;
                        case "5":
                            _aunthenticationUserInterface.Exit();
                            break;
                    }
                    break;
                case "8":
                    _aunthenticationUserInterface.Exit();
                    break;
            }
        }
        public void ProcessRequestsForecasts(string place, string lat, string lon, string requestFor, string OneDayOr5DaysChoice)
        {
            var choiceSelected = "";

            var dateOfRequist = _reciveDate.ToString("yyyy-MM-dd HH:mm:ss");

            var jsonObj = ReciveJsonObj(lat, lon, place, OneDayOr5DaysChoice);
            var idCity = queryBuilder.GetCityData(lat, lon, place).Id;
            var cityName = queryBuilder.GetCityData(lat, lon, place).Name;
            PrintData(jsonObj, OneDayOr5DaysChoice);

            var insertChoiceSelected = _aunthenticationUserInterface.MeteoChoice(requestFor, OneDayOr5DaysChoice);
            queryBuilder.InsertDataMaster(insertChoiceSelected, _idUserMaster, dateOfRequist, idCity);
            var masterData = queryBuilder.GetMasterData(_idUserMaster, dateOfRequist);
            queryBuilder.InsertDataIntoForecastTable(jsonObj, cityName, masterData.IdMaster, dateOfRequist, idCity);
            var idForecast = queryBuilder.GetForecastData(dateOfRequist).IdForecast;
            InsertData(jsonObj, OneDayOr5DaysChoice, idForecast);

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
                }
                choiceSelected = _aunthenticationUserInterface.ChoiceDoFileXls();
                if (choiceSelected == "1")
                {
                    var xlsFileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, OneDayOr5DaysChoice);
                    ChoiceCreateFileXlsOneDayOr5Days(lat, lon, place, OneDayOr5DaysChoice, xlsFileName, _dateTimeForFile, jsonObj);
                    _aunthenticationUserInterface.RequestSucces();
                }
                else
                {
                    _aunthenticationUserInterface.RequestSucces();
                }
            }
            else
            {
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
                var json = _meteoApi.ProcessMeteoByPlaceLast5Day(place, _measureUnit).Result;
                return json;
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                var json = _meteoApi.ProcessMeteoByCoordinatesLast5Day(lat, lon, _measureUnit).Result;
                return json;
            }
            _aunthenticationUserInterface.Exit();
            return null;
        }
        private void PrintData(dynamic jsonObj, string OneDayOr5Days)
        {
            if (OneDayOr5Days == "1Day")
            {
                _printData.PrintForData(jsonObj, _menuLang);
            }

            else if (OneDayOr5Days == "5Days")
            {
                _printData.PrintDataLast5Day(jsonObj, _menuLang);
            }
        }

        private void InsertData(dynamic jsonObj, string OneDayOr5Days, int idForecast)
        {
            if (OneDayOr5Days == "1Day")
            {
                queryBuilder.InsertOneDayForecast(jsonObj, idForecast);
            }
            else if (OneDayOr5Days == "5Days")
            {
                queryBuilder.Insert5DaysForecast(jsonObj, idForecast);
            }
        }
        private void ChoiceCreateFileXlsOneDayOr5Days(string lat, string lon, string place, string OneDayOr5Days, string xlsFile, string dateTime, dynamic jsonObj)
        {
            if (lat != null && OneDayOr5Days == "1Day")
            {
                _createXlsFile.CreateXlsFileForToday(jsonObj, place, xlsFile, dateTime);
            }
            else if (place != null && OneDayOr5Days == "1Day")
            {
                _createXlsFile.CreateXlsFileForTodayByCoordinates(jsonObj, lat, lon, xlsFile, dateTime);
            }
            else if (place != null && OneDayOr5Days == "5Days")
            {
                _createXlsFile.CreateXlsFileForLast5Days(jsonObj, place, xlsFile, dateTime);
            }
            else if (lat != null && OneDayOr5Days == "5Days")
            {
                _createXlsFile.CreateXlsFileForLast5DaysByCoordinates(jsonObj, lat, lon, xlsFile, dateTime);
            }
        }
        public void CreateXlsFromFileUserAction(string choiceXls)
        {
            var sourceFileName = _aunthenticationUserInterface.InsertNameFile(null, _extensionJson, null);
            var sourceFilePath = Path.Combine(DataInterface.filePath, sourceFileName);
            var xlsFileName = _aunthenticationUserInterface.InsertNameFile(_dateTimeForFile, _extensionXls, null);
            if (choiceXls == "1")
            {
                _createXlsFromFile.CreateXlsFromFileForToday(sourceFilePath, xlsFileName, _dateTimeForFile);
            }
            else
            {
                _createXlsFromFile.CreateXlsFromFileFor5Days(sourceFilePath, xlsFileName, _dateTimeForFile);
            }
        }
    }
}