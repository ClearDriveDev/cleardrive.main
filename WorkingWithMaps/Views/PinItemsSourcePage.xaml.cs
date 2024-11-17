using WorkingWithMaps.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace WorkingWithMaps.Views;

public partial class PinItemsSourcePage : ContentPage
{
    public PinItemsSourcePage()
    {
        InitializeComponent();
        BindingContext = new PinItemsSourcePageViewModel();
        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(46.25057064168142, 20.153861045837406), Distance.FromMiles(1500)));
    }

    public Location currentLocation { get;  set; }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        currentLocation = e.Location;
        
    }
}
