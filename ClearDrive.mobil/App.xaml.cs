using ClearDrive.mobil.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace ClearDrive.mobil;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<PinItemsSourcePage>();
    }
}
