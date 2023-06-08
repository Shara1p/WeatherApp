using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class LocationsListView : ContentPage
{
    public LocationsListView(LocationsListViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}