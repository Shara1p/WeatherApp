using WeatherApp.Services;
using WeatherApp.View;
using WeatherApp.ViewModel;

namespace WeatherApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Inter-VariableFont.ttf", "Inter");
            });

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<WeekWeatherView>();
        builder.Services.AddTransient<LocationsListView>();

        builder.Services.AddSingleton<WeatherService>();

        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<LocationsListViewModel>();
        builder.Services.AddTransient<WeatherViewModel>();
        builder.Services.AddTransient<WeekViewModel>();

        return builder.Build();
    }
}
