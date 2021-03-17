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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //блок отбрабатывающий исключения, если работа в режиме разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //заставляет сервер отдавать статическое содержимое
            app.UseStaticFiles();

            //маршрутизация приложения (к какому ресурсу приложения обратился браузер)
            app.UseRouting();

            //конфигурация адресов, на которые откправляются запросы и возвращаются ответы
            app.UseEndpoints(endpoints =>
            {
                //запрос - ответ ("/" - _configuration["Greetings"])
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
