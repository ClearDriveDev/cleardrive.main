using CAS.backend.Context;
using CAS.backend.Repos;
using Microsoft.EntityFrameworkCore;

namespace CAS.backend.Extensions
{
    public static class CASBackendExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(option =>
                 option.AddPolicy(name: "CASCors",
                     policy =>
                     {
                         policy.WithOrigins("http://localhost:7090/")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                     }
                 )
            );
        }

        public static void ConfigureInMemoryContext(this IServiceCollection services)
        {
            string dbNameCASContext = "CAS" + Guid.NewGuid();
            services.AddDbContext<CASContext>
            (
                 options => options.UseInMemoryDatabase(databaseName: dbNameCASContext),
                 ServiceLifetime.Scoped,
                 ServiceLifetime.Scoped
            );


            string dbNameInMemoryContext = "CAS" + Guid.NewGuid();
            services.AddDbContext<CASInMemoryContext>
            (
                 options => options.UseInMemoryDatabase(databaseName: dbNameInMemoryContext),
                 ServiceLifetime.Scoped,
                 ServiceLifetime.Scoped
            );
        }

        public static void ConfigureRepos(this IServiceCollection services)
        {
            services.AddScoped<IProblemTicketRepo, ProblemTicketRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IAdministratorRepo, AdministratorRepo>();
            services.AddScoped<IPositionRepo, PositionRepo>();
        }
    }
}
