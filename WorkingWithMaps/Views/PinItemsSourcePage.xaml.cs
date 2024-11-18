using WorkingWithMaps.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace WorkingWithMaps.Views;

public partial class PinItemsSourcePage : ContentPage
{

    private PinItemsSourcePageViewModel _pinItemsSourcePageViewModel;
    public PinItemsSourcePage()
    {
        InitializeComponent();
        map.IsShowingUser = true;
        map.IsScrollEnabled = true;
        map.IsZoomEnabled = true;
        _pinItemsSourcePageViewModel = new PinItemsSourcePageViewModel();
        BindingContext = _pinItemsSourcePageViewModel;
        _pinItemsSourcePageViewModel.SetUserLocationOnMapAsync();
    }

    public void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        if (map.Pins.Count > 0) 
        {
            map.Pins.Clear();
        }
        map.Pins.Add(_pinItemsSourcePageViewModel.CreatePin(e.Location));
        map.MoveToRegion(MapSpan.FromCenterAndRadius(e.Location, Distance.FromMeters(100)));
        _pinItemsSourcePageViewModel = e.Location;
    }

    public void AddButton(object sender, EventArgs e)
    {
        if (currentLocation != null) 
        {
            _pinItemsSourcePageViewModel.AddLocation(currentLocation);
        }
        else
        {
            DisplayAlert("Figyelem!", "Nincs helyzet megadva!", "Ok");
        }
    }

    public void RemoveButton(object sender, EventArgs e)
    {
        map.Pins.Clear();
        currentLocation = null;
    }

    public void OnViewButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        switch (button.Text)
        {
            case "Street":
                map.MapType = MapType.Street;
                break;
            case "Satellite":
                map.MapType = MapType.Hybrid;
                break;
        }
    }
}
