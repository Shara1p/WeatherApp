using Microsoft.Maui.LifecycleEvents;
using WeatherApp.Model;
using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class MainPage : ContentPage
{
    private WeatherViewModel viewmodel;
    public MainPage(WeatherViewModel viewmodel)
    {
        InitializeComponent();
        this.viewmodel = viewmodel;
        BindingContext = viewmodel;
    }

}

