using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkingWithMaps.Models;
using WorkingWithMaps.Views;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel;

namespace WorkingWithMaps.ViewModels;

public class PinItemsSourcePageViewModel
{
    int _pinCreatedCount = 0;

    private List<Location> _locations;
    private List<Pin> _pins;

    public PinItemsSourcePageViewModel()
    {
        _locations = new List<Location>(){ };
    }

    public Pin AddLocation(Location temp)
    {
        _locations.Add(temp);
        return new Pin
        {
            Location = temp,
            Label = temp.Timestamp.ToString(),
            Address = temp.Longitude.ToString() + temp.Latitude.ToString(),
            Type = PinType.Place
        };
    }

    public void RemoveLocation(Location temp)
    {
        if (_locations.Any())
        {
            _locations.Remove(temp);
        }
    }

   /*public void UpdateLocations()
    {
        if (!_locations.Any())
        {
            return;
        }

        double lastLatitude = _positions.Last().LocationINPC.Latitude;
        foreach (Position position in Positions)
        {
            position.LocationINPC = new Location(lastLatitude, position.LocationINPC.Longitude);
        }
    }
   */
    void ReplaceLocation()
    {
        
    }
}
