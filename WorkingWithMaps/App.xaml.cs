using WorkingWithMaps.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace WorkingWithMaps;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<PinItemsSourcePage>();
    }
}
