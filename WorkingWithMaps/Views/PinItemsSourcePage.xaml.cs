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
        _pinItemsSourcePageViewModel = new PinItemsSourcePageViewModel();
        BindingContext = new PinItemsSourcePageViewModel();
        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(0,0), Distance.FromMiles(1500)));
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        map.Pins.Add(_pinItemsSourcePageViewModel.AddLocation(e.Location));
        //map.MoveToRegion(MapSpan.FromCenterAndRadius(e.Location, Distance.FromMiles(10)));
        _pinItemsSourcePageViewModel.RemoveLocation(e.Location);


    }
}
