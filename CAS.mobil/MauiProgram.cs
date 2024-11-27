using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using CAS.mobil.Extensions;
using CAS.mobil.Services;
using CAS.mobil.ViewModels;
using CAS.mobil.Views;

namespace CAS.mobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>();
            
           builder.Services.AddHttpClient("ClearDriveApi", client =>
            {
                client.BaseAddress = new Uri("https://10.0.2.2:7090/");
            });
            builder.Services.AddScoped<IClearDriveService, ClearDriveService>();
            builder.Services.AddSingleton<PinItemsSourcePageViewModel>();
            builder.Services.AddSingleton<PinItemsSourcePage>();
            builder.UseMauiMaps();

            return builder.Build();
        }
    }
}
