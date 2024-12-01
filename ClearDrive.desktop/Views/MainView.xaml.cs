using System.Windows;
using ClearDrive.desktop.Views;
using ClearDrive.desktop.Views.Content;

namespace ClearDrive.desktop.Views
{

    public partial class MainView : Window
    {
        public object CurrentView
        {
            get { return (object)GetValue(CurrentViewProperty); }
            set { SetValue(CurrentViewProperty, value); }
        }

        public static readonly DependencyProperty CurrentViewProperty =
            DependencyProperty.Register("CurrentView", typeof(object), typeof(MainView), new PropertyMetadata(null));

        public MainView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnTodoButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentView = new DragAndDropTablePage(); 
        }

        private void OnMapButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentView = new MapPage(); 
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
