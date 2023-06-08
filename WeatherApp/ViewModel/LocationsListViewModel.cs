using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Model;
using WeatherApp.Services;

namespace WeatherApp.ViewModel
{
    public partial class LocationsListViewModel : BaseViewModel
    {
        MainWeather mainWeather;
        public ObservableCollection<MainWeather> mainWeatherList { get; set; } = new();
        public LocationsListViewModel(WeatherService weatherService, IGeolocation geolocation, IConnectivity connectivity) : base(weatherService, geolocation, connectivity)
        {
        }

        [RelayCommand]
        public void addLocation(string cityName)
        {
            _ = assignWeather(cityName);
        }

        [RelayCommand]
        public void removeLocation(MainWeather mainWeather)
        {
            if (mainWeather is null)
            {
                return;
            }
            mainWeatherList.Remove(mainWeather);
        }

        [RelayCommand]
        public async Task goToMainPage(MainWeather mainWeather)
        {
            if (mainWeather is null)
            {
                return;
            }

            mainWeather = await getCurrentWeather(mainWeather.CityName + " " + mainWeather.Country);

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"MainWeather", mainWeather}
            });
        }

        [RelayCommand]
        public async Task goBack()
        {
            await Shell.Current.GoToAsync("..", true);
        }


        private async Task assignWeather(string cityName)
        {
            mainWeather = await getCurrentWeather(cityName);
            if (mainWeather == null)
            {
                await Shell.Current.DisplayAlert("Location issue!", "Invalid Location name", "OK");
                return;
            }

            if (mainWeatherList.Count == 0)
            {
                mainWeatherList.Add(mainWeather);
            } else
            {
                foreach (var item in mainWeatherList)
                {
                    if (mainWeather.CityName == item.CityName)
                    {
                        return;
                    }
                }
                mainWeatherList.Add(mainWeather);
            }

            mainWeather = null;
        }
        

    }
}
