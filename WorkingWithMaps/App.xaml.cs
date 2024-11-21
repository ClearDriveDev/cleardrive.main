using WorkingWithMaps.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace WorkingWithMaps;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        // Using the service provider to resolve the main page
        MainPage = serviceProvider.GetRequiredService<PinItemsSourcePage>();
    }
}
