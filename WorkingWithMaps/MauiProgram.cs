using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using WorkingWithMaps.Extensions; 

namespace WorkingWithMaps
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Szolgáltatások regisztrálása
            builder.Services.ConfigureApiServices();
            builder.Services.ConfigureHttpCliens(); 
            builder.Services.ConfigureViewViewModels();
            return builder.UseMauiApp<App>()
                    .UseMauiMaps()
                    .Build();
        }
    }
}
