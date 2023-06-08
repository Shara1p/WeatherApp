using WeatherApp.View;

namespace WeatherApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(WeekWeatherView), typeof(WeekWeatherView));
        Routing.RegisterRoute(nameof(LocationsListView), typeof(LocationsListView));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
