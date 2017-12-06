using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Meteo
{
    public class Config
    {

        HttpClient Client { get; set; }
        string Url { get; set; }
        Menu PrintDataForToday { get; set; }
        Menu PrintAllDataDay { get; set; }

        public Config(string url)
        {
            PrintDataForToday = new Menu();
            PrintAllDataDay = new Menu();
            Client = CreateClient();
            Url = url;
        }
        public async Task ProcessMeteoByPlaceToday(string url, string fileName)
        {
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureForToday>(jsonStr);
            PrintDataForToday.PrintForData(jsonObj);
            var firstValueHeader = jsonObj.main.GetType().GetProperties();
            //Console.WriteLine(t);
            for (var i = 0; i < firstValueHeader.Length; i++)
            {
                var valueWithoutSplit = firstValueHeader.GetValue(i);
                var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];// Nome campi Header
            }
            System.IO.File.WriteAllText(fileName, jsonStr); // creazione file;
        }
        public async Task ProcessMeteoByCoordinatesToday(string url, string fileName)
        {

            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureForToday>(jsonStr);
            PrintDataForToday.PrintForData(jsonObj);
            System.IO.File.WriteAllText(fileName, jsonStr);

        }
        public async Task ProcessMeteoByPlaceLast5Day(string url, string fileName)
        {
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);
            PrintAllDataDay.PrintDataLast5Day(jsonObj);
            System.IO.File.WriteAllText(fileName, jsonStr);
        }
        public async Task ProcessMeteoByCoordinatesLast5Day(string url, string fileName)
        {


            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);
            PrintAllDataDay.PrintDataLast5Day(jsonObj);
            System.IO.File.WriteAllText(fileName, jsonStr);


        }
        public async Task FiltredMeteoByHumidityLast5Day(string url, string humidity)
        {
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);

            float humidityForFilter = float.Parse(humidity);
            var objFiltred = jsonObj.list.Where(x => x.main.humidity.Equals(humidityForFilter)).ToList();
            foreach (var item in objFiltred)
            {
                Console.WriteLine(item.main.pressure);
            }
        }
        public async Task FiltredMeteoByDateTimeLast5Day(string url, string date, string time)
        {

            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<DataFiltredTime>(jsonStr);

            var dateTime = date + " " + time;
            var objFiltred = jsonObj.list.Where(x => x.dt_txt.Equals(dateTime)).ToList();
            foreach (var item in objFiltred)
            {
                foreach (var main in item.weather)
                {
                    Console.WriteLine(main.main);
                }
            }
        }
        public async Task FiltredMeteoByWeatherLast5Day(string url, string typeWeather)
        {
            var jsonStr = await (Client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<DataFiltredTime>(jsonStr);
            foreach (var item in jsonObj.list)
            {
                var objFiltred = item.weather.Where(x => x.main.Equals(typeWeather)).ToList();

                foreach (var main in objFiltred)
                {

                    if (item.weather.Where(x => x.id == main.id).ToList().Any())
                    {
                        Console.WriteLine(item.dt_txt);
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