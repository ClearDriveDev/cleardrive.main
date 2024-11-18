using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkingWithMaps.Models;
using WorkingWithMaps.Views;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkingWithMaps.ViewModels;

public class PinItemsSourcePageViewModel
{

    private List<Position> _locations;
    private Location _currentLocation;
    private Location CurrentLocation { get => _currentLocation; set => _currentLocation = value; }

    public PinItemsSourcePageViewModel()
    {
        _locations = new List<Position>();
        CurrentLocation = new Location();
    }

    public void AddLocation(Location temp)
    {
        _locations.Add(LocationToPositionConverter(temp));
    }

    public Pin CreatePin(Location temp)
    {
        return new Pin
        {
            Location = temp,
            Label = temp.Timestamp.Year.ToString()+"."+temp.Timestamp.Month.ToString()+"."+temp.Timestamp.Day.ToString()+"  "+ temp.Timestamp.Hour.ToString()+":"+temp.Timestamp.Minute.ToString()+":"+temp.Timestamp.Second.ToString(),
            Address = ((float)temp.Longitude).ToString() + ", " + ((float)temp.Latitude).ToString(),
            Type = PinType.SavedPin
        };
    }
    
    public Position LocationToPositionConverter(Location temp) 
    {
        Position _temp_max = new Position(new Location(0,0));
        _temp_max.LocationINPC = temp;
       return _temp_max;
    }

    public void RemoveLocation(Position temp)
    {
        if (_locations.Any())
        {
            _locations.Remove(temp);
        }
    }

   public async Task SetUserLocationOnMapAsync()
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
}
