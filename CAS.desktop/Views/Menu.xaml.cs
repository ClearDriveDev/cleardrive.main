using System.Windows;
using System.Windows.Controls;

namespace CAS.desktop.Views
{
    public partial class Menu : UserControl
    {
        private MainView _mainView;

        public Menu()
        {
            InitializeComponent();
        }

        public void SetMainView(MainView mainView)
        {
            _mainView = mainView;
        }

        private void ShowMap(object sender, RoutedEventArgs e)
        {
            _mainView.ShowMap();
        }

        private void ShowDragAndDropTable(object sender, RoutedEventArgs e)
        {
            _mainView.ShowDragAndDropTable();
        }
    }
}
