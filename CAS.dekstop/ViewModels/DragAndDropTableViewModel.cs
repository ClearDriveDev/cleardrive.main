using CAS.dekstop.Models;
using CAS.dekstop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Diagnostics;
using CAS.dekstop.Responses;
using CAS.dekstop.ViewModels.Base;
using GMap.NET.WindowsPresentation;
using System.Drawing;
using GMap.NET;
using CAS.desktop.Services;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using SharpVectors.Converters;
using SharpVectors.Renderers;
using CAS.dekstop.Views;

namespace CAS.desktop.ViewModels;

public partial class DragAndDropTableViewModel : BaseViewModelWithAsyncInitialization
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();

    public DragAndDropTableViewModel()
    {
        _clearDriveService = new ClearDriveService();

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
