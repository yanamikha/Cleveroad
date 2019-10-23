using Newtonsoft.Json;
using System;

namespace TheWeather.Controllers
{
    public partial class ValuesController
    {
        public static class Parse
        {   
            public static string CurrentDay(string str)
            {
                dynamic jsonDe = JsonConvert.DeserializeObject(str);
                long timeUTC = jsonDe.dt;
                int timeZone = jsonDe.timezone / 3600;           
                string temperature = jsonDe.main.temp;                    
                double windSpeed = jsonDe.wind.speed;
                double clouds = jsonDe.clouds.all;
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timeUTC).AddHours(timeZone);
                return date.ToString("yyyy-MM-dd HH:mm:ss") + "\n\nTemperature: " + temperature + "°C\nWind speed:  " + windSpeed + " meter/sec\nCloudiness:  " + clouds + "%.";
            }

            public static string FiveDaysForecast(string str)            
            {
                dynamic jsonDe = JsonConvert.DeserializeObject(str);

                DateTime[] timeISO = new DateTime[jsonDe.list.Count];
                int[] min_temperature = new int[jsonDe.list.Count];
                int[] max_temperature = new int[jsonDe.list.Count];
                double[] windSpeed = new double[jsonDe.list.Count];
                double[] clouds = new double[jsonDe.list.Count];

                for (int i = 0; i < jsonDe.list.Count; i++)
                {
                    timeISO[i] = jsonDe.list[i].dt_txt;
                    min_temperature[i] = jsonDe.list[i].main.temp_min;
                    max_temperature[i] = jsonDe.list[i].main.temp_max;
                    windSpeed[i] = jsonDe.list[i].wind.speed;
                    clouds[i] = jsonDe.list[i].clouds.all;
                }

                string result = string.Empty;
                for (int i = 0; i < jsonDe.list.Count; i++)
                {
                    if (timeISO[i].Hour == 12)
                        result += timeISO[i] + "\nMin temperature: " + min_temperature[i] + "°C, Max temperature: " + min_temperature[i] + "°C\nWind speed:  " + windSpeed[i] + " meter/sec\nCloudiness:  " + clouds[i] + "%.\n\n";
                }
                return result;
            }
        
        }
    }
}
