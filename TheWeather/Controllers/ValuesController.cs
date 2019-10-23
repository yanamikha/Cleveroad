using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TheWeather.Controllers
{
    [Route("api")]
    [ApiController]

    public partial class ValuesController : ControllerBase
    {
        // GET api/GetCurrentWeather
        [HttpGet("GetCurrentWeather")]
        public string GetCurrentWeather(string city)
        {
            string line = string.Empty;
            using (WebClient wc = new WebClient())
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(city, @"[a-z\-]$"))
                {
                    try
                    {
                        line = wc.DownloadString("https://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=58674fffe2de0c19d2c847c9cb4d01a9");
                        return Parse.CurrentDay(line);
                    }

                    catch
                    {
                        return "Incorrect city";
                    }
                }
                return "Incorrect city";
            }         
        }

        // GET api/GetForecast
        [HttpGet("GetForecast")]
        public string GetForecast(string city)
        {
            string line = string.Empty;
            using (WebClient wc = new WebClient())
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(city, @"[a-z\-]$"))
                {
                    try
                    {
                        line = wc.DownloadString("https://api.openweathermap.org/data/2.5/forecast?q=" + city + "&units=metric&appid=58674fffe2de0c19d2c847c9cb4d01a9");
                        return Parse.FiveDaysForecast(line);
                    }

                    catch
                    {
                        return "Incorrect city";
                    }
                }
                return "Incorrect city";
            }
        }
    }
}

