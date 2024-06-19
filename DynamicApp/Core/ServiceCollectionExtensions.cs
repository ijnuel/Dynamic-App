using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Core.EntityModels;
using Core.DbContext;
using System.Reflection;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosUri = configuration.GetConnectionString("URI") ?? "";
            var cosmosKey = configuration.GetConnectionString("PrimaryKey") ?? "";
            var cosmosDB = configuration.GetConnectionString("DBName") ?? "";

            services.AddTransient<IApplicationDBContext, ApplicationDBContext>();
            services.AddDbContext<ApplicationDBContext>(
                optionsBuilder => optionsBuilder.UseCosmos(cosmosUri, cosmosKey, cosmosDB,
                options =>
                {
                    options.ConnectionMode(ConnectionMode.Direct);
                    options.WebProxy(new WebProxy());
                    options.LimitToEndpoint();
                    options.GatewayModeMaxConnectionLimit(32);
                    options.MaxRequestsPerTcpConnection(8);
                    options.MaxTcpConnectionsPerEndpoint(16);
                    /*options.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                    options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                    options.RequestTimeout(TimeSpan.FromMinutes(1));*/
                }));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddEntityImplementations<BaseInterface>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var serviceClasses = types.Where(t => t.IsClass && !t.IsInterface && t.GetInterfaces().Contains(typeof(BaseInterface))).ToList();
            foreach (var serviceClass in serviceClasses)
            {
                var serviceInterface = serviceClass.GetInterfaces().Last();
                services.AddScoped(serviceInterface, serviceClass);
            }

            return services;
        }
    }
}
