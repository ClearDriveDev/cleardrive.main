using GMap.NET.MapProviders;
using GMap.NET;
using System.Windows;
using GMap.NET.WindowsPresentation;
using CAS.desktop.ViewModels;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CAS.dekstop.Views
{
    public partial class Map : Window
    {

        private MapPageViewModel _mapPageViewModel;
        public Map()
        {
            InitializeComponent();
            
            _mapPageViewModel = new MapPageViewModel();
            DataContext = _mapPageViewModel;

            GMapProviders.GoogleMap.ApiKey = "AIzaSyBn_LH5TSBgmEQAJD6wDAy82eWv8zQW5eE";
            GMapControl.MapProvider = GMapProviders.GoogleMap;
            GMapControl.MaxZoom = 18;
            GMapControl.MinZoom = 5;
            GMapControl.Manager.Mode = AccessMode.ServerAndCache;
            GMapControl.CacheLocation = @"C:\Temp\GMapCache";  
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PointLatLng point = new PointLatLng(46.25312, 20.143497);

            /*MapMarker marker = new GMapMarker(point);

            Ellipse ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            marker.Shape = ellipse;*/

            GMapControl.Position = point;

            GMapControl.Zoom = 15;

            //GMapControl.Markers.Add(marker);

            await _mapPageViewModel.InitializeAsync();
            foreach (var item in _mapPageViewModel.Locations)
            {
                PointLatLng temp = new PointLatLng(item.Latitude, item.Longitude);
                GMapControl.Markers.Add(_mapPageViewModel.CreateMarker(temp));
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