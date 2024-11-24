using GMap.NET.MapProviders;
using GMap.NET;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using GMap.NET.WindowsPresentation;

namespace CAS.dekstop
{
    public partial class Map : Window
    {
        public Map()
        {
            InitializeComponent();

            GMapProviders.GoogleMap.ApiKey = "AIzaSyBn_LH5TSBgmEQAJD6wDAy82eWv8zQW5eE";
            GMapControl.MapProvider = GMapProviders.GoogleMap;
            GMapControl.MaxZoom = 18;
            GMapControl.MinZoom = 5;
            GMapControl.Manager.Mode = AccessMode.ServerAndCache;
            GMapControl.CacheLocation = @"C:\Temp\GMapCache";  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PointLatLng point = new PointLatLng(46.25312, 20.143497);
            GMapMarker marker = new GMapMarker(point);
            GMapControl.Position = point;
            GMapControl.Zoom = 15;
            GMapControl.Markers.Add(marker);
            
            
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