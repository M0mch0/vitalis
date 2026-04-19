using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Services.Core.Contracts;
using Vitalis.Services.Core;
using Vitalis.Data.Seeding.Contracts;
using Vitalis.Data.Seeding;
namespace Vitalis.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
         /*   public static IApplicationBuilder AddApplicationServices(this IApplicationBuilder builder)
            {
                string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    
                builder.Services.AddDbContext<VitalisDbContext>(options =>
                    options.UseSqlServer(connectionString));
    
                builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddEntityFrameworkStores<VitalisDbContext>();
    
                builder.Services.AddScoped<ICatalogService, CatalogService>();
                builder.Services.AddScoped<IJournalService, JournalService>();
    
                builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

                return builder;*/
        }
}
