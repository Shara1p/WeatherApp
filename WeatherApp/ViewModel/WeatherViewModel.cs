using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Model;
using WeatherApp.Services;
using WeatherApp.View;

namespace WeatherApp.ViewModel
{
    [QueryProperty(nameof(MainWeather), nameof(MainWeather))]
    public partial class WeatherViewModel : BaseViewModel
    {

        [ObservableProperty]
        public MainWeather mainWeather;

        public WeatherViewModel(WeatherService weatherService, IGeolocation geolocation, IConnectivity connectivity) : base(weatherService, geolocation, connectivity)
        {
            _ = assignWeather();
        }

        [RelayCommand]
        async Task GoToWeekForecast()
        {

            await Shell.Current.GoToAsync($"{nameof(WeekWeatherView)}", true, new Dictionary<string, object>
            {
                {"MainWeather", MainWeather}
            });
        }

        [RelayCommand]
        async Task GoToLocationsList()
        {
            await Shell.Current.GoToAsync($"{nameof(LocationsListView)}", true);
        }

        private async Task assignWeather()
        {
            MainWeather = await getCurrentWeather();
        }

        
        


    }
}
