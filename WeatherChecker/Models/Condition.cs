namespace WeatherChecker.Models
{
    public class Condition : WeatherApiModel
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }
}
