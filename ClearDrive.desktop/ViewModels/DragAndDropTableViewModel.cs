using ClearDrive.shared.Models;
using ClearDrive.shared.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using ClearDrive.shared.Responses;
using ClearDrive.desktop.ViewModels.Base;

namespace ClearDrive.desktop.ViewModels;

public partial class DragAndDropTableViewModel : BaseViewModelWithAsyncInitialization
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();

    public DragAndDropTableViewModel(IClearDriveService? clearDriveService)
    {
        _clearDriveService = clearDriveService;

    }

    [RelayCommand]
    public async Task DoUpdate(Position positionToUpdate)
    {
        if (_clearDriveService is not null)
        {

            ControllerResponse result = await _clearDriveService.UpdateAsync(positionToUpdate);

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
}
