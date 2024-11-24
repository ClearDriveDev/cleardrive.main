using System.Configuration;
using System.Data;
using System.Windows;

namespace CAS.dekstop
{
    public partial class App : Application
    {
        private readonly bool _loginPage = false;
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.ConfigureViewViewModels();
                    services.ConfigureHttpCliens();
                    services.ConfigureApiServices();
                })
                .Build();

        }
    

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
            base.OnExit(e);
        }

    }

}
