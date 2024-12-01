using ClearDrive.shared.Models;
using ClearDrive.shared.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using ClearDrive.shared.Responses;
using ClearDrive.desktop.ViewModels.Base;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System.Windows.Shapes;
using SharpVectors.Converters;

namespace ClearDrive.desktop.ViewModels;

public partial class MapPageViewModel : BaseViewModelWithAsyncInitialization
{
    private readonly IClearDriveService? _clearDriveService;

    [ObservableProperty]
    private ObservableCollection<Position> _locations = new();

    public MapPageViewModel()
    {
        _clearDriveService = new ClearDriveService("http://10.0.2.2:7090/");
        
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
            Width = 15,
            Height = 15,
            Fill = System.Windows.Media.Brushes.Blue,
            Stroke = System.Windows.Media.Brushes.Black,
            StrokeThickness = 2
        };

        marker.Shape = ellipse;

        return marker;
    }

    public GMapMarker CreatePin(PointLatLng temp)
    {
        GMapMarker marker = new GMapMarker(temp);

        Uri svgUri = new Uri("https://upload.wikimedia.org/wikipedia/commons/d/d1/Google_Maps_pin.svg");
        SvgViewbox svgViewbox = new SvgViewbox
        {
            Width = 35,
            Height = 35
        };

        svgViewbox.Load(svgUri);

        marker.Shape = svgViewbox;

        return marker;
    }
}
