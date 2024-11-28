using ClearDrive.desktop.Views.Content;
using System.Windows;

namespace ClearDrive.desktop.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            contentControl.Content = new DragAndDropTablePage();
            var menu = new Menu();
            menu.SetMainView(this); 
            leftPanel.Content = menu; 
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

        public void ShowMap()
        {
            contentControl.Content = new MapPage();
        }

        public void ShowDragAndDropTable()
        {
            contentControl.Content = new DragAndDropTablePage();
        }
    }
}
