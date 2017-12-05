using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Meteo
{
    public class Config
    {
        public async Task ProcessMeteoByPlaceToday(string place)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureForToday>(jsonStr);

            Console.WriteLine(jsonObj.main.pressure);
            Console.WriteLine("Temperature");
            Console.WriteLine(jsonObj.main.temp);
            Console.WriteLine("Humidity");
            Console.WriteLine(jsonObj.main.humidity);
            Console.WriteLine("Temperature min");
            Console.WriteLine(jsonObj.main.temp_min);
            Console.WriteLine("Temperature max");
            Console.WriteLine(jsonObj.main.temp_max);
            
            System.IO.File.WriteAllText("Meteo odierno.json", jsonStr); // creazione file;



        }
        public async Task ProcessMeteoByCoordinatesToday(string lat, string lon)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<MeasureForToday>(jsonStr);

            Console.WriteLine(jsonObj.main.pressure);
            Console.WriteLine("Temperature");
            Console.WriteLine(jsonObj.main.temp);
            Console.WriteLine("Humidity");
            Console.WriteLine(jsonObj.main.humidity);
            Console.WriteLine("Temperature min");
            Console.WriteLine(jsonObj.main.temp_min);
            Console.WriteLine("Temperature max");
            Console.WriteLine(jsonObj.main.temp_max);
        }
        public async Task ProcessMeteoByPlaceLast5Day(string place)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://samples.openweathermap.org/data/2.5/forecast?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);



            foreach (var measure in jsonObj.list)
            {

                Console.WriteLine("Pressure");
                Console.WriteLine(measure.main.pressure);
                Console.WriteLine("Temperature");
                Console.WriteLine(measure.main.temp);
                Console.WriteLine("Humidity");
                Console.WriteLine(measure.main.humidity);
                Console.WriteLine("Temperature min");
                Console.WriteLine(measure.main.temp_min);
                Console.WriteLine("Temperature max");
                Console.WriteLine(measure.main.temp_max);
            }


        }




        public async Task ProcessMeteoByCoordinatesLast5Day(string lat, string lon)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://samples.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);

            foreach (var measure in jsonObj.list)
            {

                Console.WriteLine("Pressure");
                Console.WriteLine(measure.main.pressure);
                Console.WriteLine("Temperature");
                Console.WriteLine(measure.main.temp);
                Console.WriteLine("Humidity");
                Console.WriteLine(measure.main.humidity);
                Console.WriteLine("Temperature min");
                Console.WriteLine(measure.main.temp_min);
                Console.WriteLine("Temperature max");
                Console.WriteLine(measure.main.temp_max);

            }


        }
        public async Task FiltredMeteoByHumidityLast5Day(string place, string humidity)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://samples.openweathermap.org/data/2.5/forecast?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<Repo>(jsonStr);

            float humidityForFilter = float.Parse(humidity);
            var objFiltred = jsonObj.list.Where(x => x.main.humidity.Equals(humidityForFilter)).ToList();
            foreach (var item in objFiltred)
            {
                // float prova = ;
                Console.WriteLine(item.main.pressure);

            }
        }
        public async Task FiltredMeteoByDateTimeLast5Day(string place, string date, string time)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://samples.openweathermap.org/data/2.5/forecast?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<DataFiltredTime>(jsonStr);

            var dateTime = date + " " + time;
            var objFiltred = jsonObj.list.Where(x => x.dt_txt.Equals(dateTime)).ToList();
            foreach (var item in objFiltred)
            {
                //    Console.WriteLine(item.weather.main);
                foreach (var main in item.weather)
                {
                    Console.WriteLine(main.main);
                }


            }

        }
        public async Task FiltredMeteoByWeatherLast5Day(string place, string typeWeather)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"http://samples.openweathermap.org/data/2.5/forecast?q={place}&appid=0dc9854b15fa5612e84597073b150cd3";
            var jsonStr = await (client.GetStringAsync(url));
            var jsonObj = JsonConvert.DeserializeObject<DataFiltredTime>(jsonStr);
            foreach (var item in jsonObj.list)
            {
                var objFiltred = item.weather.Where(x => x.main.Equals(typeWeather)).ToList();

                foreach (var main in objFiltred)
                {
                    Console.WriteLine(main.id);
                }

            }
        }
    }
}