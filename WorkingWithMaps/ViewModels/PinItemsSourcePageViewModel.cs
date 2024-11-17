using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkingWithMaps.Models;
using WorkingWithMaps.Views;

namespace WorkingWithMaps.ViewModels;

public class PinItemsSourcePageViewModel
{
    int _pinCreatedCount = 0;
    readonly ObservableCollection<Position> _positions;

    public IEnumerable Positions => _positions;

    public ICommand AddLocationCommand { get; }
    public ICommand RemoveLocationCommand { get; }
    public ICommand ClearLocationsCommand { get; }
    public ICommand UpdateLocationsCommand { get; }
    public ICommand ReplaceLocationCommand { get; }

    public PinItemsSourcePageViewModel()
    {
        _positions = new ObservableCollection<Position>()
        {
            new Position(new Location(46.25057064168142, 20.153861045837406)),
            new Position(new Location(46.24697970247574 ,20.16021251678467)),
            new Position(new Location(46.249917761129915, 20.164418220520023)),
            
        };

        AddLocationCommand = new Command(AddLocation);
        RemoveLocationCommand = new Command(RemoveLocation);
        ClearLocationsCommand = new Command(() => _positions.Clear());
        UpdateLocationsCommand = new Command(UpdateLocations);
        ReplaceLocationCommand = new Command(ReplaceLocation);
    }

    void AddLocation()
    {
        _positions.Add(NewPosition());
    }

    void RemoveLocation()
    {
        if (_positions.Any())
        {
            _positions.Remove(_positions.First());
        }
    }

    void UpdateLocations()
    {
        if (!_positions.Any())
        {
            return;
        }

        double lastLatitude = _positions.Last().LocationINPC.Latitude;
        foreach (Position position in Positions)
        {
            position.LocationINPC = new Location(lastLatitude, position.LocationINPC.Longitude);
        }
    }

    void ReplaceLocation()
    {
        if (!_positions.Any())
        {
            return;
        }

        _positions[_positions.Count - 1] = NewPosition();
    }

    Position NewPosition()
    {
        return null;
            
    }
}
