using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMarket.Infrastructure.Conventions;
using WebMarket.Infrastructure.Services;
using WebMarket.Infrastructure.Services.Interfaces;

namespace WebMarket
{
    public record Startup(IConfiguration configuration)
    {
        //private IConfiguration _configuration { get; }
        //
        //public Startup(IConfiguration Configuration)
        //{
        //    _configuration = Configuration;
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddTransient<IProductData, InMemoryProductData>();


            services
                .AddControllersWithViews(
                    mvc =>
                    {
                        //mvc.Conventions.Add(new ActionDescriptionAttribute("123"));
                        mvc.Conventions.Add(new ApplicationConvention());
                    })
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //  Блок обрабатывающий исключения, если работа в режиме разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  Заставляет сервер отдавать статическое содержимое
            app.UseStaticFiles();

            //  Маршрутизация приложения (к какому ресурсу приложения обратился браузер)
            app.UseRouting();

            //  Конфигурация адресов, на которые откправляются запросы и возвращаются ответы
            app.UseEndpoints(endpoints =>
            {
                //  Запрос - ответ ("/" - _configuration["Greetings"])
                endpoints.MapGet("/Greetings", async context =>
                {
                    await context.Response.WriteAsync(configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    "default" /* название маршрута*/ ,
                    "{controller=Home}/{action=Index}/{id?}" /* шаблон сопоставления адреса из адресной строки с основными понятиями MVC */ );
            });
        }
    }
}