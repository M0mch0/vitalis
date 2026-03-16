using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Services.Core;
using Vitalis.Services.Core.Contracts;

namespace Vitalis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            
            builder.Services.AddDbContext<VitalisDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<VitalisDbContext>();

            builder.Services.AddScoped<ICatalogService, CatalogService>();


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
