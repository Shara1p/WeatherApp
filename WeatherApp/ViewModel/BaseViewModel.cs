using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Model;
using WeatherApp.Services;

namespace WeatherApp.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        private MainWeather _mainWeather;

        private WeatherData _weatherDataFromApi;
        private WeatherService _weatherService;

        private IGeolocation _geolocation;
        private IConnectivity _connectivity;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !IsBusy;

        public BaseViewModel(WeatherService weatherService, IGeolocation geolocation, IConnectivity connectivity)
        {
            _weatherService = weatherService;
            _geolocation = geolocation;
            _connectivity = connectivity;
        }

        [RelayCommand]
        public async Task<MainWeather> getCurrentWeather()
        {
            if (IsBusy)
            {
                return null;
            }

            try
            {
                _ = requestLocation();
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue!", "Check tour internet and try again!", "OK");
                    return null;
                }

                IsBusy = true;
                var location = await getUserLocation();
                if (location is null)
                {
                    return null;
                }

                string longtitude = Math.Round(location.Longitude, 0).ToString();
                string latitude = Math.Round(location.Latitude, 0).ToString();
                string query = GlobalUsings.Constants.weatherApiEndPoint + latitude + ", " + longtitude +
                               GlobalUsings.Constants.weatherForecast;
                _weatherDataFromApi = await _weatherService.getWeather(query);
                _mainWeather = new MainWeather(_weatherDataFromApi);
                return _mainWeather;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Weather: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            return _mainWeather;

        }
        
        public async Task<MainWeather> getCurrentWeather(string cityName)
        {
            if (IsBusy)
            {
                return null;
            }

            try
            {
                IsBusy = true;
                
                string query = GlobalUsings.Constants.weatherApiEndPoint + cityName +
                               GlobalUsings.Constants.weatherForecast;
                _weatherDataFromApi = await _weatherService.getWeather(query);
                if (_weatherDataFromApi == null)
                {
                    return null;
                }
                _mainWeather = new MainWeather(_weatherDataFromApi);
                return _mainWeather;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Weather: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            return _mainWeather;

        }

        private async Task<Microsoft.Maui.Devices.Sensors.Location> getUserLocation()
        {
            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                    location = await _geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30),
                    });
                }

                return location;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            return null;
        }

        private async Task requestLocation() // works only for Android
        {
            var status = PermissionStatus.Unknown;

            status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                return;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                await Shell.Current.DisplayAlert("Needs Permission!!!", "because.", "OK");
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission required!!!",
                    "Location permission required for getting weather ",
                    "App does not store your location for spying.", "OK");
            }
        }
    }
}
