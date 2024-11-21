using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Extensions
{
    public static class HttpCliensExtension
    {
        public static void ConfigureHttpCliens(this IServiceCollection services)
        {
            services.AddHttpClient("ClearDriveApi", options =>
            {
                options.BaseAddress = new Uri("https://10.0.2.2:7090/");
            });
        }

    }
}
