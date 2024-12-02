using ClearDrive.shared.Services;
using ClearDrive.mobil.ViewModels;
using ClearDrive.mobil.Views;

namespace ClearDrive.mobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>();
            builder.UseMauiMaps();
            builder.Services.AddHttpClient("ClearDriveApi", client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:7090/");
            });
            builder.Services.AddScoped<IClearDriveService, ClearDriveService>();
            builder.Services.AddSingleton<PinItemsSourcePageViewModel>();
            builder.Services.AddSingleton<PinItemsSourcePage>();

            return builder.Build();
        }
    }

}
