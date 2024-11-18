using WorkingWithMaps.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace WorkingWithMaps.Views;

public partial class PinItemsSourcePage : ContentPage
{

    private PinItemsSourcePageViewModel _pinItemsSourcePageViewModel;
    private Location currentLocation;

    async Task SetUserLocationOnMapAsync()
    {
        try
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromMiles(1)));
            }
            else
            {
                await DisplayAlert("Figyelem!", "Nem tudunk hozzaferni a helyadataihoz, igy alapertelmezetten Szegedre iranyitottuk!", "Ok");
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(46.25336, 20.147209), Distance.FromMiles(100)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba a helyzet lekérdezésekor: {ex.Message}");
        }
    }
    public PinItemsSourcePage()
    {
        InitializeComponent();
        map.IsShowingUser = true;
        map.IsScrollEnabled = true;
        map.IsZoomEnabled = true;
        _pinItemsSourcePageViewModel = new PinItemsSourcePageViewModel();
        BindingContext = new PinItemsSourcePageViewModel();
        SetUserLocationOnMapAsync();
    }

    public void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        if (map.Pins.Count > 0) 
        {
            map.Pins.Clear();
        }
        map.Pins.Add(_pinItemsSourcePageViewModel.CreatePin(e.Location));
        map.MoveToRegion(MapSpan.FromCenterAndRadius(e.Location, Distance.FromMiles(5)));
        currentLocation = e.Location;
    }

    public void AddButton(object sender, EventArgs e)
    {
        if (currentLocation != null) 
        {
            _pinItemsSourcePageViewModel.AddLocation(currentLocation);
            _pinItemsSourcePageViewModel.PinCreatedCount++;
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
