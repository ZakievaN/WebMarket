using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebMarket.DAL.Context;
using WebMarket.Data;
using WebMarket.Infrastructure.Conventions;
using WebMarket.Infrastructure.Services.InCookies;
using WebMarket.Infrastructure.Services.InMemory;
using WebMarket.Infrastructure.Services.InSQL;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarketDomain.Entityes.Identity;

namespace WebMarket
{
    public record Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebMarketDB>(opt => 
            opt.UseSqlServer(configuration.GetConnectionString("Default"))
            );

            services.AddTransient<WebMarketDbInitializer>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebMarketDB>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "GB.WebMarket";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICartServices, InCookiesCartServices>();


            services
                .AddControllersWithViews(
                    mvc =>
                    {
                        mvc.Conventions.Add(new ApplicationConvention());
                    })
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebMarketDbInitializer db)
        {
            db.Initialize();

            //  Блок обрабатывающий исключения, если работа в режиме разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  Заставляет сервер отдавать статическое содержимое
            app.UseStaticFiles();

            //  Маршрутизация приложения (к какому ресурсу приложения обратился браузер)
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //  Конфигурация адресов, на которые откправляются запросы и возвращаются ответы
            app.UseEndpoints(endpoints =>
            {
                //  Запрос - ответ ("/" - _configuration["Greetings"])
                endpoints.MapGet("/Greetings", async context =>
                {
                    await context.Response.WriteAsync(configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "default" /* название маршрута*/ ,
                    "{controller=Home}/{action=Index}/{id?}" /* шаблон сопоставления адреса из адресной строки с основными понятиями MVC */ );
            });
        }
    }
}