using System;
using System.Collections.Generic;
using Meteo.Services;


namespace Meteo.UI.AuthenticationUser
{
    public class AuthenticationUserInterface
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







        public AuthenticationUserInterface(string menuLang, Menu menu)
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
                Console.WriteLine(DataInterface.successIT);
            }
            else
            {
                Console.WriteLine(DataInterface.successEN);
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
            _menu.Chioce();

            var choiceSelected = Console.ReadLine();

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
            _menu.Chioce();

            var choiceSelected = Console.ReadLine();
            return choiceSelected;

        }
        public string InsertNameFile(string dataPrinted, string extension, string OneDayOr5Days)
        {
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.insertNameFileIT);
            }
            else
            {
                Console.WriteLine(DataInterface.insertNameFileEN);
            }
            if (extension == ".json")
            {
                
                var fileName = string.Concat(Console.ReadLine() + OneDayOr5Days + dataPrinted + extension );
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
            var choiceSelected = "";
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.choiceSendEmailIT);
            }
            else
            {
                Console.WriteLine(DataInterface.choiceSendEmailEN);
            }
            _menu.Chioce();
            choiceSelected = Console.ReadLine();

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
        public DateTimeUserInput ReadDataTime()
        {
            if (_menuLang == "it")
            {
                Console.WriteLine("Inserisci data con il seguente formato YYYY-mm-GG");
            }
            else
            {
                Console.WriteLine("Enter date with the following format YYYY-mm-GG");
            }
            var date = Console.ReadLine();
            var validateDateTime = new DateTimeUserInput(date);
            return validateDateTime;
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
       




















        public class Coordinate
        {
            public string Lat { get; set; }
            public string Lon { get; set; }
            public string _menuLang;

            public Coordinate ReadCoordinate()
            {
                var lat = "";
                var lon = "";
                if (_menuLang == "it")
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
                var c = new Coordinate();
                c.Lat = lat;
                c.Lon = lon;



                return c;

            }
        }








    }
}
