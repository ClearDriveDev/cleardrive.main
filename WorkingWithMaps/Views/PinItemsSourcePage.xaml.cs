using WorkingWithMaps.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace WorkingWithMaps.Views;

public partial class PinItemsSourcePage : ContentPage
{

    private async Task SetUserLocationOnMapAsync()
    {
        try
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
            }
            else
            {
                await DisplayAlert("Figyelem!", "Nem tudunk hozzaferni a helyadataihoz, igy alapertelmezetten Szegedre iranyitottuk!", "Ok");
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(46.25336, 20.147209), Distance.FromKilometers(1)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba a helyzet lekérdezésekor: {ex.Message}");
        }
    }

    private PinItemsSourcePageViewModel _pinItemsSourcePageViewModel;
    private Location _currentLocation;
    public PinItemsSourcePage()
    {
        InitializeComponent();
        map.IsShowingUser = true;
        map.IsScrollEnabled = true;
        map.IsZoomEnabled = true;
        _pinItemsSourcePageViewModel = new PinItemsSourcePageViewModel();
        //BindingContext = _pinItemsSourcePageViewModel;
        SetUserLocationOnMapAsync();
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        if (map.Pins.Count > 0) 
        {
            map.Pins.Clear();
        }
        map.Pins.Add(_pinItemsSourcePageViewModel.CreatePin(e.Location));
        map.MoveToRegion(MapSpan.FromCenterAndRadius(e.Location, Distance.FromMeters(100)));
        _currentLocation = e.Location;
    }

    private void AddButton(object sender, EventArgs e)
    {
        if (_currentLocation != null) 
        {
            _pinItemsSourcePageViewModel.AddLocation(_currentLocation);
        }
        else
        {
            DisplayAlert("Figyelem!", "Nincs helyzet megadva!", "Ok");
        }
    }

    private void RemoveButton(object sender, EventArgs e)
    {
        map.Pins.Clear();
        _currentLocation = null;
    }

    private void OnViewButtonClicked(object sender, EventArgs e)
    {
        map.MapType = (map.MapType == MapType.Street) ? MapType.Hybrid : MapType.Street;
    }
}
