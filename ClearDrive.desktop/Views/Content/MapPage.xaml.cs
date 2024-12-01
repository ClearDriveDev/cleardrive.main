using GMap.NET.MapProviders;
using GMap.NET;
using System.Windows;
using GMap.NET.WindowsPresentation;
using ClearDrive.desktop.ViewModels;
using System.Windows.Controls;

namespace ClearDrive.desktop.Views.Content
{
    public partial class MapPage : UserControl
    {

        private MapPageViewModel _mapPageViewModel;
        public MapPage()
        {
            InitializeComponent();
            
            _mapPageViewModel = new MapPageViewModel();
            DataContext = _mapPageViewModel;

            GMapProviders.GoogleMap.ApiKey = "AIzaSyBn_LH5TSBgmEQAJD6wDAy82eWv8zQW5eE";
            GMapControl.MapProvider = GMapProviders.GoogleMap;
            GMapControl.MaxZoom = 18;
            GMapControl.MinZoom = 5;
            GMapControl.Manager.Mode = AccessMode.ServerAndCache;
            GMapControl.CacheLocation = @"C:\Windows\Temp";  
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PointLatLng point = new PointLatLng(46.25312, 20.143497);
            GMapControl.Markers.Add(_mapPageViewModel.CreateMarker(point));
            GMapControl.Position = point;
            GMapControl.Zoom = 15;

            await _mapPageViewModel.InitializeAsync();
            foreach (var item in _mapPageViewModel.Locations)
            {
                PointLatLng temp = new PointLatLng(item.Latitude, item.Longitude);
                GMapControl.Markers.Add(_mapPageViewModel.CreatePin(temp));
            }
        }


        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (GMapControl.Zoom < GMapControl.MaxZoom)
            {
                GMapControl.Zoom++;
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (GMapControl.Zoom > GMapControl.MinZoom)
            {
                GMapControl.Zoom--;
            }
        }
    }
}