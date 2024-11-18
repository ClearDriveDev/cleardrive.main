using WorkingWithMaps.Models;
using Microsoft.Maui.Controls.Maps;
using WorkingWithMaps.Services;

namespace WorkingWithMaps.ViewModels;

public class PinItemsSourcePageViewModel
{
    private readonly IClearDriveService? _clearDriveService;
    private List<Position> _locations;

    public PinItemsSourcePageViewModel()
    {
        _locations = new List<Position>();
    }

    public PinItemsSourcePageViewModel(IClearDriveService? clearDriveService)
    {
        _locations = new List<Position>();
        _clearDriveService = clearDriveService;
        
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

    public override async Task InitializeAsync()
    {
        if (_clearDriveService is not null)
        {
            List<Position> positions = await _clearDriveService.SelectAll();
            Students = new ObservableCollection<Student>(students);
        }
    }
}
