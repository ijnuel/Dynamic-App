using Core;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Abstractions;
using Repositories.Abstractions.Base;
using Repositories.Implementations;
using Repositories.Implementations.Base;
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
