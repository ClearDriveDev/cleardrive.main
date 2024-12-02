using System.Windows;
using ClearDrive.desktop.Views.Content;
using Microsoft.Extensions.DependencyInjection;

namespace ClearDrive.desktop.Views
{
    public partial class MainView : Window
    {
        private readonly IServiceProvider? _serviceProvider;
        private MapPage? _mapPage;
        private DragAndDropTablePage? _dragAndDropTablePage;

        public object CurrentView
        {
            get { return (object)GetValue(CurrentViewProperty); }
            set { SetValue(CurrentViewProperty, value); }
        }

        public static readonly DependencyProperty CurrentViewProperty =
            DependencyProperty.Register("CurrentView", typeof(object), typeof(MainView), new PropertyMetadata(null));

        public MainView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = this;
            _serviceProvider = serviceProvider; 
        }

        public MainView() { }

        private async void OnTodoButtonClick(object sender, RoutedEventArgs e)
        {
            if (_dragAndDropTablePage == null)
            {
                _dragAndDropTablePage = _serviceProvider.GetRequiredService<DragAndDropTablePage>();
                await _dragAndDropTablePage.LoadData();  
            }

            CurrentView = _dragAndDropTablePage;
        }

        private async void OnMapButtonClick(object sender, RoutedEventArgs e)
        {
            if (_mapPage == null)
            {
                _mapPage = _serviceProvider.GetRequiredService<MapPage>();
                await _mapPage.LoadData();
            }

            CurrentView = _mapPage;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
