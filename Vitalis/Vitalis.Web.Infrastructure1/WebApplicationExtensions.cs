using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Vitalis.Data.Seeding.Contracts;

namespace Vitalis.Web.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseRolesSeeder(this IApplicationBuilder app)
        {
            using IServiceScope scope = app
                .ApplicationServices
                .CreateScope();
            IIdentitySeeder identitySeeder = scope
                .ServiceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder
                .SeedRolesAsync()
                .GetAwaiter()
                .GetResult();
            return app;
        }

        public static IApplicationBuilder UseAdminUserSeeder(this IApplicationBuilder app)
        {
            using IServiceScope scope = app
                .ApplicationServices
                .CreateScope();
            IIdentitySeeder identitySeeder = scope
                .ServiceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder
                .SeedAdminUserAsync()
                .GetAwaiter()
                .GetResult();

            return app; 

        }
    }
}
