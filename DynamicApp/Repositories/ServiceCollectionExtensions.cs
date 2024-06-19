using Core;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Abstractions.Base;
using System.Reflection;

namespace Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddEntityImplementations<IBaseRepository>(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
