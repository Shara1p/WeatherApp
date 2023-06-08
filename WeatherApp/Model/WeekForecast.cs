using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class WeekForecast
    {
        public double Maxtemp_c { get; set; }
        public double Mintemp_c { get; set; }
        public string WeatherIcon { get; set; }
        public string WaterFall { get; set; }
        public string WindSpeed { get; set; }
        public string Humidity { get; set; }

        public string DayOfWeek { get; set; }

        public WeekForecast(Forecastday day)
        {
            Maxtemp_c = day.day.maxtemp_c;
            Mintemp_c = day.day.mintemp_c;
            WeatherIcon = "http:" + day.day.condition.icon;
            WaterFall = day.day.totalprecip_mm.ToString();
            WindSpeed = day.day.maxwind_kph.ToString();
            Humidity = day.day.avghumidity.ToString();
            DayOfWeek = day.date;
            var date = DateTime.Parse(DayOfWeek);
            DayOfWeek = date.ToString("dddd");
        }

    }
}
