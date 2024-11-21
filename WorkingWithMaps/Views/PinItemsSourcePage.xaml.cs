using WorkingWithMaps.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using WorkingWithMaps.Models;
using System.Diagnostics;

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

    public PinItemsSourcePage(PinItemsSourcePageViewModel viewModel)
    {
        InitializeComponent();  
        BindingContext = viewModel;
        _pinItemsSourcePageViewModel = viewModel;

        map.IsShowingUser = true;
        map.IsScrollEnabled = true;
        map.IsZoomEnabled = true;
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await SetUserLocationOnMapAsync();
        await _pinItemsSourcePageViewModel.InitializeAsync();
        foreach (var item in _pinItemsSourcePageViewModel.Locations)
        {
            map.Pins.Add(_pinItemsSourcePageViewModel.CreatePin(item.Location));
        }

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

    private async void AddButton(object sender, EventArgs e)
    {
        if (_currentLocation != null)
        {
            Position temp = new Position(new Location(_currentLocation.Latitude, _currentLocation.Longitude));
            await _pinItemsSourcePageViewModel.DoSave(temp);
            Debug.WriteLine("\nLefutottam0.!\n");
        }
        else
        {
            await DisplayAlert("Figyelem!", "Nincs helyzet megadva!", "Ok");
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
