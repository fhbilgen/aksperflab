using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace webAPP.Pages
{
    public class weather_forecastModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string url = Environment.GetEnvironmentVariable("WEBAPIURL");
        //"https://localhost:5001/WeatherForecast";
        public IList<WeatherForecast>? Forecasts { get; set; }
        public void OnGet()
        {
        }


        private static async Task<List<WeatherForecast>> GetForecasts()
        {
            List<WeatherForecast> fcs = new List<WeatherForecast>();
            var streamTask = await client.GetStreamAsync(url);

            await foreach (var forecast in JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(streamTask))
            {
                fcs.Add(forecast);
            }
            return fcs;
        }

        public bool gfcWrapper()
        {
            Forecasts = GetForecasts().Result;
            if (Forecasts != null)
                return true;    
            else 
                return false;
        }
    }
}

