using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using WorkingWithMaps.Extensions; // Importáljuk a saját kiterjesztéseinket

namespace WorkingWithMaps
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Szolgáltatások regisztrálása
            builder.Services.ConfigureApiServices(); // A saját kiterjesztésed az API szolgáltatások regisztrálásához
            builder.Services.ConfigureHttpCliens(); // A HttpClient-ek regisztrálása
            builder.Services.ConfigureViewViewModels(); // A nézetek és nézetmodellek regisztrálása

            return builder
                .UseMauiApp<App>() // Az alkalmazás beállítása
                .UseMauiMaps()
                .Build();
        }
    }
}
