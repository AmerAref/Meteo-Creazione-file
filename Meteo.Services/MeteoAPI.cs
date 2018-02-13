using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using Meteo.Services.OpenWeatherMap.Models;

namespace Meteo.Services
{
    public interface IMeteoApiService
    {
        Task<OneDayForecast> ProcessMeteoForToday(string place, string lat, string lon, string unitMeasure, string country);
        Task<FiveDaysForecast> ProcessMeteoForFiveDays(string place, string lat, string lon, string unitMeasure, string country);
    }
    public class MeteoApi : IMeteoApiService
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

        public async Task<OneDayForecast> ProcessMeteoForToday(string place, string lat, string lon, string unitMeasure, string country)
        {
            var url = "";
            if (!string.IsNullOrEmpty(place))
            {
                url = $"{_appUri}weather?q={place}{country}&units={unitMeasure}&appid={_appId}";
            }
            else if (!string.IsNullOrEmpty(lat + lon))
            {
                url = $"{_appUri}weather?lat={lat}&lon={lon}&units={unitMeasure}&appid={_appId}";
            }
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<OneDayForecast>(jsonStr);
            return jsonObj;
        }

        public async Task<FiveDaysForecast> ProcessMeteoForFiveDays(string place, string lat, string lon, string unitMeasure, string country)
        {
            var url = "";
            if (place != null)
            {
                url = $"{_appUri}forecast?q={place}{country}&units={unitMeasure}&appid={_appId}";
            }
            else if (lat != null)
            {
                url = $"{_appUri}forecast?lat={lat}&lon={lon}&units={unitMeasure}&appid={_appId}";
            }
            var jsonStr = await Client.GetStringAsync(url);
            var jsonObj = JsonConvert.DeserializeObject<FiveDaysForecast>(jsonStr);
            return jsonObj;
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