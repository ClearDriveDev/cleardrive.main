using ClearDrive.mobil.Views;

namespace ClearDrive.mobil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new PinItemsSourcePage();
        }
    }
}
