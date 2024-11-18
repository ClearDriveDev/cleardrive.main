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
    private int _pinCreatedCount = 0;

    public int PinCreatedCount{get => _pinCreatedCount; set {_pinCreatedCount = value;} }

    private List<Position> _locations;

    public PinItemsSourcePageViewModel()
    {
        _locations = new List<Position>();
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

    public void RemoveLocation(Position temp, int id)
    {
        if (_locations.Any())
        {
            _locations.Remove(temp);
        }
    }
}
