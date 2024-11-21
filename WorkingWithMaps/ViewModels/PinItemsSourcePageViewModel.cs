using WorkingWithMaps.Models;
using Microsoft.Maui.Controls.Maps;
using WorkingWithMaps.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkingWithMaps.ViewModels.Base;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WorkingWithMaps.Extensions;
using System.Diagnostics;
using WorkingWithMaps.Responses;

namespace WorkingWithMaps.ViewModels;

public partial class PinItemsSourcePageViewModel : BaseViewModelWithAsyncInitialization
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();


    public PinItemsSourcePageViewModel(IClearDriveService? clearDriveService)
    {
        _clearDriveService = clearDriveService;
        
    }

    [RelayCommand]
    public async Task DoSave(Position newPosition)
    {
        if (_clearDriveService is not null)
        {
            ControllerResponse result = await _clearDriveService.InsertAsync(newPosition);
            if (!result.HasError)
            {
                await UpdateView();
            }
            else
            {
                Debug.WriteLine($"{result.ToString()}");
            }
        }
        else
        {
            Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
        }
    }

    [RelayCommand]
    public async Task DoRemove(Position positionToDelete)
    {
        if (_clearDriveService is not null)
        {
            ControllerResponse result = await _clearDriveService.DeleteAsync(positionToDelete.Id);
            if (!result.HasError)
            {
                await UpdateView();
            }
            else
            {
                Debug.WriteLine($"{result.ToString()}");
            }
        }
        else
        {
            Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
        }
    }

    public override async Task InitializeAsync()
    {
        await UpdateView();
    }

    public async Task UpdateView()
    {
        if (_clearDriveService is not null)
        {
            List<Position> positions = await _clearDriveService.SelectAll();
            Locations = new ObservableCollection<Position>(positions);
        }
        else
        {
            Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
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
}
