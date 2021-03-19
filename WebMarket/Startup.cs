using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebMarket
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
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
                    await context.Response.WriteAsync(_configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    "default" /* название маршрута*/ ,
                    "{controller=Home}/{action=Index}/{id?}" /* шаблон сопоставления адреса из адресной строки с основными понятиями MVC */ );
            });
        }
    }
}