using WorkingWithMaps.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace WorkingWithMaps;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new PinItemsSourcePage();
    }
}
