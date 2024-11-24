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

namespace CAS.desktop.ViewModels;

public partial class MapPageViewModel : BaseViewModelWithAsyncInitialization
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();

    public MapPageViewModel()
    {
        _clearDriveService = new ClearDriveService();
        
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

    public GMapMarker CreateMarker(PointLatLng temp)
    {

        GMapMarker marker = new GMapMarker(temp);

        Ellipse ellipse = new Ellipse
        {
            Width = 20,
            Height = 20,
            Fill = Brushes.Red,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        marker.Shape = ellipse;

        return marker;
    }
}
