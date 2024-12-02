﻿using ClearDrive.desktop.ViewModels;
using ClearDrive.desktop.Views.Content;
using ClearDrive.desktop.Views;
using ClearDrive.shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClearDrive.desktop
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Az Ioc konténer inicializálása
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // A MainView dinamikus létrehozása a DI rendszerből
            var mainWindow = ServiceProvider.GetRequiredService<MainView>();
            MainWindow = mainWindow;
            MainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Szolgáltatások regisztrálása
            services.AddHttpClient("ClearDriveApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:7090/");
            });

            services.AddScoped<IClearDriveService, ClearDriveService>();
            services.AddSingleton<MapPageViewModel>();
            services.AddSingleton<MapPage>();
            services.AddSingleton<DragAndDropTableViewModel>();
            services.AddSingleton<DragAndDropTablePage>();
            services.AddSingleton<MainView>();  // MainView regisztrálása a DI konténerbe
        }
    }

}
