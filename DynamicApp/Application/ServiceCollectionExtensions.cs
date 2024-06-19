using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Models;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Core.SessionIdentitier;
using Application.Services.Abstractions.Base;
using Application.Services.Abstractions;
using Application.Services.Implementations;
using Core;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppAuthorizations(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigin = "http://localhost:4200";

            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => 
                policy.WithOrigins(allowedOrigin)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["AccessToken"];
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            context.Response.Headers["Access-Control-Allow-Origin"] = allowedOrigin;
                            context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
                        }
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.Headers["Access-Control-Allow-Origin"] = allowedOrigin;
                        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();


            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ISessionIdentifier, SessionIdentifier>();
            services.AddScoped<IUserService, UserService>();
            services.AddEntityImplementations<IBaseService>(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
