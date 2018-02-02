using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using Meteo.Services.OpenWeatherMap.Models;
using System.Collections.Generic;
using System.IO;


namespace Meteo.Services
{
    public class MeteoApi
    {
        private HttpClient Client { get; }
        private readonly string _appUri;
        private readonly string _appId;

        public MeteoApi()
        {
            Client = CreateClient();
            _appId = "0dc9854b15fa5612e84597073b150cd3";
            _appUri = "http://api.openweathermap.org/data/2.5/";
        }

        public async Task<OneDayForecast> ProcessMeteoByPlaceToday(string place, string unitMeasure)
        {
            var url = $"{_appUri}weather?q={place}&units={unitMeasure}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<OneDayForecast>(jsonStr);
            return jsonObj;
        }

        public async Task<OneDayForecast> ProcessMeteoByCoordinatesToday(string lat, string lon, string unitMeasure)
        {
            var url = $"{_appUri}weather?lat={lat}&lon={lon}&units={unitMeasure}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<OneDayForecast>(jsonStr);
            return jsonObj;
        }

        public async Task<LastFiveDaysForecast> ProcessMeteoByPlaceLast5Day(string place, string unitMeasure)
        {
            var url = $"{_appUri}forecast?q={place}&units={unitMeasure}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(jsonStr);
            return jsonObj;
        }

        public async Task<LastFiveDaysForecast> ProcessMeteoByCoordinatesLast5Day(string lat, string lon, string unitMeasure)
        {
            var url = $"{_appUri}forecast?lat={lat}&lon={lon}&units={unitMeasure}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(jsonStr);
            return jsonObj;
        }

        public async Task<LastFiveDaysForecast> FiltredMeteoByHumidityLast5Day(string humidity, string place)
        {
            var url = $"{_appUri}forecast?q={place}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(jsonStr);
            var humidityForFilter = int.Parse(humidity);
            var objFiltred = jsonObj.List.Where(x => x.Parameters.Humidity.Equals(humidityForFilter));

            var objFiltredReady = new LastFiveDaysForecast()
            {
                List = objFiltred.ToList()
            };
            return objFiltredReady;
        }

        public async Task<LastFiveDaysForecast> FiltredMeteoByDateTimeLast5Day(string date, string time, string place)
        {
            var url = $"{_appUri}forecast?q={place}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(jsonStr);
            var dateTimeUserInput = new DateTimeUserInput(date);
            var dateTime = $"{date}  {time}";

            var authenticationData = dateTimeUserInput.GetResponse();
            var dataReady = true;
            while (dataReady)
            {
                if (authenticationData != null)
                {
                    var objFiltred = jsonObj.List.Where(x => x.TimeStamp
                                                    .Equals(dateTime));
                    var objFiltredReady = new LastFiveDaysForecast()
                    {
                        List = objFiltred.ToList()
                    };
                    return objFiltredReady;
                }
            }
            return null;
        }

        public async Task FiltredMeteoByWeatherLast5Day(string typeWeather, string place)
        {
            var url = $"{_appUri}forecast?q={place}&appid={_appId}";
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(jsonStr);

            foreach (var item in jsonObj.List)
            {
                var objFiltred = item.Weather.Where(x => x.Main.Equals(typeWeather)).ToList();

                foreach (var main in objFiltred)
                {
                    Console.WriteLine("Id");
                    Console.WriteLine(main.Id);
                    Console.WriteLine("Description");
                    Console.WriteLine(main.Description);
                }
            }
            return;
        }

        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}