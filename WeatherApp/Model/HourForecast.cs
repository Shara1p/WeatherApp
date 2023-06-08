namespace WeatherApp.Model
{
    public class HourForecast
    {
        public string Hour { get; set; }
        public string HourIcon { get; set; }
        public string TempC { get; set; }

        public HourForecast(Hour hour)
        {
            Hour = hour.time.Substring(10);
            HourIcon = "http:" + hour.condition.icon;
            TempC = hour.feelslike_c.ToString();
        }
    }
}
