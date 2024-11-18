﻿using Microsoft.Extensions.Logging;

namespace WorkingWithMaps;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiMaps();

#if DEBUG
		builder.Logging.AddDebug();
#endif
#if IOS
        builder.UseGoogleMaps("AIzaSyBn_LH5TSBgmEQAJD6wDAy82eWv8zQW5eE");
#endif

        return builder.Build();
	}
}
