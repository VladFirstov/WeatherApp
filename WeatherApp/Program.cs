﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Coord
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
    }

    public class City
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public Coord? coord { get; set; }
        public string? country { get; set; }
        public int? population { get; set; }
        public int? timezone { get; set; }
        public int? sunrise { get; set; }
        public int? sunset { get; set; }
    }

    public class WeatherInfoMain
    {
        public double? temp { get; set; }
        public double? feels_like { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public int? pressure { get; set; }
        public int? sea_level { get; set; }
        public int? grnd_level { get; set; }
        public int? humidity { get; set; }
        public double? temp_kf { get; set; }
    }

    public class WeatherInfo
    {
        public int? dt { get; set; }
        public WeatherInfoMain? main { get; set; }
        public string? dt_txt { get; set; }
    }

    public class WeatherForecast
    {
        public string? cod { get; set; }
        public int? message { get; set; }
        public int? cnt { get; set; }
        public City? city { get; set; }
        public IList<WeatherInfo>? list { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "https://api.openweathermap.org/data/2.5/forecast?appid=1a378d6b4d0969a0d1ffbe734285436b&q=Cherkasy&cnt=5";

        async static Task Main(string[] args)
        {
            await getWeather();
            Console.WriteLine("Hello, World!");

            async Task getWeather()
            {
                Console.WriteLine("Getting JSON...");
                var responseString = await client.GetStringAsync(url);
                Console.WriteLine("Parsing JSON...");
                WeatherForecast? weatherForecast =
                   JsonSerializer.Deserialize<WeatherForecast>(responseString);
                Console.WriteLine($"cod: {weatherForecast?.cod}");
                Console.WriteLine($"City: {weatherForecast?.city?.name}");
                Console.WriteLine($"list count: {weatherForecast?.list?.Count}");
                foreach (var weather in weatherForecast?.list)
                {
                    Console.WriteLine($"weather temp : {weather?.main?.temp}");
                }
            }
        }
    }
}
