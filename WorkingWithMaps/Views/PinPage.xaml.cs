using Microsoft.Maui.Controls.Maps;

namespace WorkingWithMaps.Views;

public partial class PinPage : ContentPage
{
    public PinPage()
    {
        InitializeComponent();
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        Pin boardwalkPin = new Pin
        {
            Location = new Location(46.25057064168142, 20.153861045837406),
            Label = "Boardwalk",
            Address = "Santa Cruz",
            Type = PinType.Place
        };
        boardwalkPin.MarkerClicked += OnMarkerClickedAsync;
        
        Pin wharfPin = new Pin
        {
            Location = new Location(46.25057064168142, 20.153861045837406),
            Label = "Wharf",
            Address = "Santa Cruz",
            Type = PinType.Place
        };
        wharfPin.InfoWindowClicked += OnInfoWindowClickedAsync;

        map.Pins.Add(boardwalkPin);
        map.Pins.Add(wharfPin);
    }

    async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
    {
        e.HideInfoWindow = true;
        string pinName = ((Pin)sender).Label;
        await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
    }

    async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
    {
        string pinName = ((Pin)sender).Label;
        await DisplayAlert("Info Window Clicked", $"The info window was clicked for {pinName}.", "Ok");
    }
}
