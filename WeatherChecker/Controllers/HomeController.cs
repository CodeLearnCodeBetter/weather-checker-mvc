using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherChecker.Models;
using System.Web;
using System.Reflection;

namespace WeatherChecker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string DEFAULT_CITY = "Pretoria";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            WeatherDotComApi.InitializeClient();
        }

        public IActionResult Index()
        {

            //  For testing pass a City name
            var ip = this.HttpContext.Connection.RemoteIpAddress;
            Realtime model = WeatherDotComApi.LoadCurrent(ip.ToString()).Result;

            //  Ip ip not found set to London as default
            if (model.location == null)
            {
                model = WeatherDotComApi.LoadCurrent(DEFAULT_CITY).Result;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Current() 
        {
            Realtime model = WeatherDotComApi.LoadCurrent("London").Result;
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult index(string searchString, string currentLocation)
        {
            Realtime model = WeatherDotComApi.LoadCurrent(searchString).Result;


            //  Check if no location was found
            if (model.location == null)
            {
                model = WeatherDotComApi.LoadCurrent(currentLocation).Result;
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}