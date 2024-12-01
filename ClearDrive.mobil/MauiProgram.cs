namespace ClearDrive.mobil
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
