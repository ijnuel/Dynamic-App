using Application.Models.Dtos.Users;
using Application.Services.Abstractions;
using Application.Services.Implementations;
using Core.Data;
using Core.DbContext;
using Core.EntityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class MiddlewareExtensions
    {
        public async static Task<IApplicationBuilder> MigrateDatabase(this IApplicationBuilder app, IServiceCollection services)
        {
            using (var context = (ApplicationDBContext)services.BuildServiceProvider().GetService(typeof(ApplicationDBContext)))
            {
                if (context != null && context.Database.IsCosmos())
                {
                    context.Database.EnsureCreated();
                }
                await SeedAdmin(context, services);
            }

            return app;
        }

        private async static Task<ApplicationDBContext> SeedAdmin(ApplicationDBContext context, IServiceCollection services)
        {
            if (await context.Users.FirstOrDefaultAsync(user => user.Email == SeededData.AdminEmail) == null)
            {
                var _userService = (IUserService)services.BuildServiceProvider().GetService(typeof(IUserService));
                if (_userService != null)
                {
                    var (userCreated, createUserResponse) = await _userService.CreateUser(new CreateUserDto
                    {
                        Email = SeededData.AdminEmail,
                        Password = SeededData.AdminPassword,
                        FirstName = SeededData.AdminName,
                        LastName = SeededData.AdminName,
                        MiddleName = SeededData.AdminName,
                        DateOfBirth = DateTime.UtcNow
                    });
                }
            }
            return context;
        }
    }
}
