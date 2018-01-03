using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Meteo.Services
{
    public class MeteoAPI
    {
        private HttpClient Client { get; set; }
        private string appUri;
        private string appId;

        public MeteoAPI()
        {
            Client = CreateClient();
            appId = "0dc9854b15fa5612e84597073b150cd3";
            appUri = "http://api.openweathermap.org/data/2.5/";
        }
        public async Task<MeasureToday> ProcessMeteoByPlaceToday(string place, string unitMeasure)
        {

            var url = $"{appUri}weather?q={place}&units={unitMeasure}&appid={appId}";
            string jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureToday>(jsonStr);
            return jsonObj;
        }
        public async Task<MeasureToday> ProcessMeteoByCoordinatesToday(string lat, string lon, string unitMeasure)
        {
            var url = $"{appUri}weather?lat={lat}&lon={lon}&units={unitMeasure}&appid={appId}";

            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureToday>(jsonStr);
            return jsonObj;
        }
        public async Task<ListMeasureLast5Day> ProcessMeteoByPlaceLast5Day(string place, string unitMeasure)
        {
            var url = $"{appUri}forecast?q={place}&units={unitMeasure}&appid={appId}";
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<ListMeasureLast5Day>(jsonStr);
            return jsonObj;
        }
        public async Task<ListMeasureLast5Day> ProcessMeteoByCoordinatesLast5Day(string lat, string lon, string unitMeasure)
        {
            var url = $"{appUri}forecast?lat={lat}&lon={lon}&units={unitMeasure}&appid={appId}";

            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<ListMeasureLast5Day>(jsonStr);
            return jsonObj;
        }
        public async Task FiltredMeteoByHumidityLast5Day(string humidity, string place)
        {
            var url = $"{appUri}forecast?q={place}&appid={appId}";
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<ListMeasureLast5Day>(jsonStr);
            float humidityForFilter = float.Parse(humidity);
            var objFiltred = jsonObj.List.Where(x => x.Main.Humidity.Equals(humidityForFilter)).ToList();
            foreach (var item in objFiltred)
            {
                Console.WriteLine(item.Main.Pressure);
            }
        }
        public async Task FiltredMeteoByDateTimeLast5Day(string place, string date, string time)
        {
            var url = $"{appUri}forecast?q={place}&appid={appId}";
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<ListDataFiltred>(jsonStr);
            var dateTime = date + " " + time;
            var objFiltred = jsonObj.ListForFilter.Where(x => x.Date.Equals(dateTime)).ToList();
            foreach (var item in objFiltred)
            {
                foreach (var main in item.Weather)
                {
                    Console.WriteLine(main.Main);
                }
            }
        }
        public async Task FiltredMeteoByWeatherLast5Day(string typeWeather, string place)
        {
            var url = $"{appUri}forecast?q={place}&appid={appId}";
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<ListDataFiltred>(jsonStr);
            foreach (var item in jsonObj.ListForFilter)
            {
                var objFiltred = item.Weather.Where(x => x.Main.Equals(typeWeather)).ToList();
                foreach (var main in objFiltred)
                {
                    if (item.Weather.Where(x => x.Id == main.Id).ToList().Any())
                    {
                        Console.WriteLine(item.Date);
                    }
                }
            }
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