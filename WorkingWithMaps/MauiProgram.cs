using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using WorkingWithMaps.Extensions;
using WorkingWithMaps.Services;
using WorkingWithMaps.ViewModels;
using WorkingWithMaps.Views;

namespace WorkingWithMaps
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>();
            
           /* builder.Services.AddHttpClient("ClearDriveApi", client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:7090/");
            });
            builder.Services.AddScoped<IClearDriveService, ClearDriveService>();
            builder.Services.AddSingleton<PinItemsSourcePageViewModel>();
            builder.Services.AddSingleton<PinItemsSourcePage>();*/
            builder.UseMauiMaps();

            return builder.Build();
        }
    }
}
