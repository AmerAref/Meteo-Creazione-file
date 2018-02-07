using System;
using System.Collections.Generic;
using Meteo.Services;


namespace Meteo.UI.ForecastManager
{
    public class ForecastManagerUI
    {
        public string _menuLang;
        public Menu _menu;
        public Dictionary<string, string> dataForEmail { get; set; }
        public string senderValue { get; set; }
        public string receiverValue { get; set; }
        public string bodyValue { get; set; }
        public string subjectValue { get; set; }
        public string userValue { get; set; }
        public string password { get; set; }
        public string passwordMaskered { get; set; }

        public ForecastManagerUI(string menuLang, Menu menu)
        {
            _menuLang = menuLang;
            _menu = menu;
        }
        public void GetMenuLang(string menuLang)
        {
            _menuLang = menuLang;
        }

        public string InsertNamePlace()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.insertNamePlaceIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertNamePlaceEN);
            }
            var place = Console.ReadLine();
            return place;
        }

        public string MeteoChoice(string searchingFor, string OneDayOr5DaysChoice)
        {
            var meteoChoiceForDB = "";

            if (_menuLang == "it")
            {
                meteoChoiceForDB = $"Previsioni {OneDayOr5DaysChoice} ({searchingFor})";
            }
            else
            {
                meteoChoiceForDB = $"Forecast {OneDayOr5DaysChoice} ({searchingFor})";
            }
            return meteoChoiceForDB;
        }

        public void RequestSucces()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.successIT + "\n");
            }
            else
            {
                Console.WriteLine(DataInterface.successEN + "\n");
            }
        }
        public string ChoiceDoFileJson()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.choiceDoFileIT);
            }
            else
            {
                Console.WriteLine(DataInterface.choiceDoFileEN);
            }
            var choiceSelected = _menu.Chioce();

            return choiceSelected;
        }
        public string ChoiceDoFileXls()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.choiceCreateXlsFileIT);
            }
            else
            {
                Console.WriteLine(DataInterface.choiceCreateXlsFileEN);
            }
            var choiceSelected = _menu.Chioce();
            return choiceSelected;
        }
        public string InsertNameFile(string dataPrinted, string extension, string OneDayOr5Days)
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.insertNameFileIT + $"({extension})");
            }
            else
            {
                Console.WriteLine(DataInterface.insertNameFileEN + $"({extension})");
            }
            if (extension == ".json")
            {

                var fileName = string.Concat(Console.ReadLine() + OneDayOr5Days + dataPrinted + extension);
                return fileName;
            }
            else if (extension == ".xls")
            {
                var fileName = Console.ReadLine();
                return fileName;
            }
            else
            {
                var fileName = Console.ReadLine();

                return fileName;
            }
        }
        public string ChoceSendEmail()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.choiceSendEmailIT);
            }
            else
            {
                Console.WriteLine(DataInterface.choiceSendEmailEN);
            }
            var choiceSelected = _menu.Chioce();
            return choiceSelected;
        }

        public void AuthenticationUserInterfaceSendEmail()
        {
            dataForEmail = GetDataForEmail.InsertDataForEmail(_menuLang);
            senderValue = dataForEmail["senderKey"];
            receiverValue = dataForEmail["receiverKey"];
            bodyValue = dataForEmail["bodyKey"];
            subjectValue = dataForEmail["subjectKey"];
            userValue = dataForEmail["userKey"];
            password = "";
            passwordMaskered = DataMaskManager.MaskData(password);
        }
        public string ReadHumidityValue()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Inserisci valore umidità richiesta riguardante gli ultimi 5 giorni");
            }
            else
            {
                Console.WriteLine("Enter value of required humidity for the last 5 days");
            }

            var humidity = Console.ReadLine();
            return humidity;
        }
        public string ReadTime()
        {
            var time = "00:00:00";
            if (_menuLang == "it")
            {
                Console.WriteLine("Inserisci orario con il seguente formato HH:MM:SS");
            }
            else
            {
                Console.WriteLine("Enter time with the following format HH:MM:SS");
            }
            time = Console.ReadLine();
            return time;
        }
        public string ReadQualitySky()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Iserisci tipologia di tempo richiesta");
            }
            else
            {
                Console.WriteLine("Enter the type of requested weather");
            }
            var typeWeather = Console.ReadLine();
            return typeWeather;
        }

        public string InsertStartDate()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Iserisci la data di inizio");
            }
            else
            {
                Console.WriteLine("Enter the start date");
            }
            var startDate = Console.ReadLine();
            return startDate;
        }
        public string InsertEndDate()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Iserisci la data di fine");
            }
            else
            {
                Console.WriteLine("Enter the end date");
            }
            var endDate = Console.ReadLine();
            return endDate;
        }

        public void Exit()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Sessione terminata");
            }
            else
            {
                Console.WriteLine("Session ended");
            }
            Environment.Exit(0);
        }
        public string ReadDate()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Inserisci Data");
            }
            else
            {
                Console.WriteLine("Read Data");
            }
            var readDate = Console.ReadLine();
            return readDate;
        }
    }
}