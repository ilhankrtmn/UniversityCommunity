using Microsoft.Extensions.DependencyInjection;
using UniversityCommunity.Business;
using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.UnitOfWork;

namespace ELECTRACORE.Business.Utilities.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

            services.AddHttpClient();

            services.Scan(scan => scan
           .FromAssemblies(typeof(IScopedRepository).Assembly)
           .AddClasses(classes => classes.AssignableTo<IScopedRepository>(), publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

            services.Scan(scan => scan
           .FromAssemblies(typeof(IScopedService).Assembly)
           .AddClasses(classes => classes.AssignableTo<IScopedService>(), publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

            return services;
        }
    }
}
