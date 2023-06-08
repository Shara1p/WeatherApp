namespace WeatherApp.Model
{
    public class MainWeather
    {
        public string CityName { get; set; }

        public string Country { get; set; }

        public string Date { get; set; }

        public string WeatherDescription { get; set; }

        public string WeatherIcon { get; set; }

        public string TempC { get; set; }

        public string TempFeelsLikeC { get; set; }

        public string WindSpeed { get; set; }

        public string Humidity { get; set; }

        public string WaterFall { get; set; }

        public List<HourForecast> HourForecast { get; } = new();
        public List<WeekForecast> WeekForecast { get; } = new();
        public Forecast Forecast { get; set; }
        public MainWeather(WeatherData weatherData)
        {
            CityName = weatherData.location.name;
            Country = weatherData.location.country;
            Date = weatherData.location.localtime;
            WeatherDescription = weatherData.current.condition.text;
            WeatherIcon = "http:" + weatherData.current.condition.icon;
            TempC = weatherData.current.temp_c.ToString();
            TempFeelsLikeC = weatherData.current.feelslike_c.ToString();
            WindSpeed = weatherData.current.wind_kph.ToString();
            Humidity = weatherData.current.humidity.ToString();
            WaterFall = weatherData.current.precip_mm.ToString();
            Forecast = weatherData.forecast;
            setHourForecast();
            setWeekForecast();
        }

        private void setHourForecast()
        {
            var forecastDay = Forecast.forecastday[0];
            foreach(var hour in forecastDay.hour)
            {
                HourForecast.Add(new HourForecast(hour));
            }
        }

        private void setWeekForecast()
        {
            var forecastDay = Forecast.forecastday;

            if (WeekForecast.Count != 0)
            {
                return;
            }

            foreach (var day in forecastDay)
            {
                WeekForecast.Add(new WeekForecast(day));
            }
        }
    }
}
