using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;

namespace WeatherChecker.Models
{
    public static class WeatherDotComApi
    {
        public static HttpClient ApiClient { get; set; }
        public static string BaseAddress = @"http://api.weatherapi.com/v1/";
        public const string Key = "44f72b976ae34010af1152641231012";
        public const string CurrentEndPoint = "current.json?";
        public const string ForecastEndPoint = "forecast.json?";
        public const string SearchAutoEndPoint = "search.json?";

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(BaseAddress);
        }

        //  Get realtime weather for a location
        [HttpGet]
        public static async Task<Realtime> LoadCurrent(string city)
        {
            string getUri = CurrentEndPoint + (city == "" ? "" : "&q=" + city);
            Realtime responseObj = new Realtime();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(ApiClient.BaseAddress + getUri);
                request.Method = HttpMethod.Get;
                request.Headers.Add("key", Key);

                using (HttpResponseMessage response = await ApiClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonSerializer.Deserialize<Realtime>(responseContent);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return responseObj;
        }
    }
}
