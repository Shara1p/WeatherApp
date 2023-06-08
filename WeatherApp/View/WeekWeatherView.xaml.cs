using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class WeekWeatherView : ContentPage
{
    public WeekWeatherView(WeekViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
}