using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Security.Principal;

namespace WeatherTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string APIKey = "";
            Console.WriteLine("Enter latitude: ");
            double lat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter longitude: ");
            double lon = Convert.ToDouble(Console.ReadLine());
            await HTTPSCall(lat, lon, APIKey);
        }

        static async Task HTTPSCall(double lat, double lon, string apikey)
        {
            HttpClient client = new HttpClient();
            try
            {
                string url = string.Format($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apikey}&units=metric");

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responses = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a WeatherApiResponse object
                var weatherresponse = JsonSerializer.Deserialize<response>(responses);
                  
                Console.WriteLine($"Temp: {weatherresponse.main.temp}");
                Console.WriteLine($"Weather: {weatherresponse.weather[0].main}");
                Console.WriteLine($"Wind: {weatherresponse.wind.gust}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public class response
        {
            public Coord coordinates { get; set; }
            public List<Weather> weather { get; set; }
            public string bases {  get; set; }
            public Maind main {  get; set; }
            public int visibility {  get; set; }
            public Wind wind {  get; set; }
            public Rain rain {  get; set; }
            public Clouds clouds {  get; set; }
            public long dt {  get; set; }
            public Sys sys {  get; set; }
            public int timezone {  get; set; }
            public int id {  get; set; }
            public string Name {  get; set; }
            public int cod {  get; set; }
        }
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
        public class Weather
        {
            public int id { get; set; }
            public string main {  get; set; }
            public string descritpion {  get; set; }
            public string icon {  get; set; }
        }
        public class Maind
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public int sea_level { get; set; }
            public int grnd_level { get; set; }
        }
        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
            public double gust { get; set; }
        }
        public class Rain
        {
            public double h1 { get; set; }
        }
        public class Clouds
        {
            public int all { get; set; }
        }
        public class Sys
        {
            public int type { get; set; }
            public int id { set; get; }
            public long sunrise {  get; set; }
            public long sunset {  get; set; }
        }
    }
}
