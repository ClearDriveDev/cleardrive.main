using Microsoft.Extensions.DependencyInjection;
using WorkingWithMaps.ViewModels;
using WorkingWithMaps.Views;


namespace WorkingWithMaps.Extensions
{
    public static class ViewViewModelsExtensions
    {
        public static void ConfigureViewViewModels(this IServiceCollection services)
        {
            // MainView
            services.AddSingleton<PinItemsSourcePageViewModel>();
            services.AddSingleton<PinItemsSourcePage>(s => new PinItemsSourcePage()
            {
                BindingContext = s.GetRequiredService<PinItemsSourcePageViewModel>()
            });
        }
    }
}
