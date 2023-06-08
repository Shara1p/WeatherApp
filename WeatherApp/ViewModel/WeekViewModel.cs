using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Model;
using WeatherApp.Services;

namespace WeatherApp.ViewModel
{
    [QueryProperty("MainWeather", "MainWeather")]
    public partial class WeekViewModel : BaseViewModel
    {
        [ObservableProperty]
        public MainWeather mainWeather;

        public WeekViewModel(WeatherService weatherService, IGeolocation geolocation, IConnectivity connectivity) : base(weatherService, geolocation, connectivity)
        {
        }

        [RelayCommand]
        public async Task goBack()
        {
            MainWeather = await getCurrentWeather(MainWeather.CityName + " " + MainWeather.Country);
            await Shell.Current.GoToAsync($"..", true, new Dictionary<string, object>
            {
                {"MainWeather", MainWeather}
            });
        }

    }
}
