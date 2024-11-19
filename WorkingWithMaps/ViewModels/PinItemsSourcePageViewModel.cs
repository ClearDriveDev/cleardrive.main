using WorkingWithMaps.Models;
using Microsoft.Maui.Controls.Maps;
using WorkingWithMaps.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkingWithMaps.ViewModels.Base;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace WorkingWithMaps.ViewModels;

public partial class PinItemsSourcePageViewModel : BaseViewModel
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();

    [ObservableProperty]
    private Position _selectedLocation;

    public PinItemsSourcePageViewModel()
    {
        SelectedLocation = new Position();
    }

    public PinItemsSourcePageViewModel(IClearDriveService? clearDriveService)
    {
        SelectedLocation = new Position();
        _clearDriveService = clearDriveService;
        
    }

    [RelayCommand]
    public async Task DoSave(Position newPosition)
    {
        if (_clearDriveService is not null)
        {
            string result = "";
            if (!newPosition.HasId)
                result = await _clearDriveService.InsertAsync(newPosition);

            if (result.Length == 0)
            {
                await UpdateView();
            }
        }
    }

    [RelayCommand]
    void DoNewStudent()
    {
        SelectedLocation = new Position();
    }

    [RelayCommand]
    public async Task DoRemove(Position position)
    {
        if (_clearDriveService is not null)
        {
            string result = await _clearDriveService.DeleteAsync(position.Id);
            if (result.Length==0)
            {
                await UpdateView();
            }
        }
    }

    /*public void AddLocation(Location temp)
    {
        _locations.Add(LocationToPositionConverter(temp));
    }*/

    /*public Position LocationToPositionConverter(Location temp) 
    {
        Position _temp_max = new Position(new Location(0,0));
        _temp_max.LocationINPC = temp;
       return _temp_max;
    }*/

    public override async Task InitializeAsync()
    {
        await UpdateView();
    }

    private async Task UpdateView()
    {
        if (_clearDriveService is not null)
        {
            List<Position> positions = await _clearDriveService.SelectAll();
            Locations = new ObservableCollection<Position>(positions);
        }
    }

    public Pin CreatePin(Location temp)
    {
        return new Pin
        {
            Location = temp,
            Label = temp.Timestamp.Year.ToString() + "." + temp.Timestamp.Month.ToString() + "." + temp.Timestamp.Day.ToString() + "  " + temp.Timestamp.Hour.ToString() + ":" + temp.Timestamp.Minute.ToString() + ":" + temp.Timestamp.Second.ToString(),
            Address = ((float)temp.Longitude).ToString() + ", " + ((float)temp.Latitude).ToString(),
            Type = PinType.SavedPin
        };
    }

    /*public void RemoveLocation(Position temp)
    {
        if (_locations.Any())
        {
            _locations.Remove(temp);
        }
    }*/
}
