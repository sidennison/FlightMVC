using FlightMVC.Data;
using FlightMVC.Filters;
using FlightMVC.Repositories;
using FlightMVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace FlightMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            var authenticatedPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews(options =>
            {
                var providers = options.ModelBinderProviders;
                options.Filters.Add(new AuthorizeFilter(authenticatedPolicy));
            });

            var adminPolicy = new AuthorizationPolicyBuilder().RequireRole("Admin").Build();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("adminPolicy", adminPolicy);
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddTransient<IPassengersRepository, PassengersRepository>();
            builder.Services.AddTransient<MyExceptionFilterAttribute>();

            builder.Services.Configure<ConfigData>(builder.Configuration.GetSection("ConfigData"));

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

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //CreateUserRoles(builder.Services.BuildServiceProvider()).Wait();  // Nasty hack, because the next line threw up unepectedly:
            //CreateUserRoles(app.Services).Wait();

            app.Run();
        }

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                IdentityResult roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));

                if (roleResult.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync("bob@example.com");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}