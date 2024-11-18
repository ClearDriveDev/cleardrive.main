using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWithMaps.Services;

namespace WorkingWithMaps.Extensions
{
    public static class ApiServiceExtensions
    {
        public static void ConfigureApiServices(this IServiceCollection services)
        {
            services.AddScoped<IClearDriveService, ClearDriveService>();
        }
    }
}
