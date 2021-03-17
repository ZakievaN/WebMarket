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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //блок отбрабатывающий исключени€, если работа в режиме разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //маршрутизаци€ приложени€ (к какому ресурсу приложени€ обратилс€ браузер)
            app.UseRouting();

            //конфигураци€ адресов, на которые откправл€ютс€ запросы и возвращаютс€ ответы
            app.UseEndpoints(endpoints =>
            {
                //запрос - ответ ("/" - _configuration["Greetings"])
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(_configuration["Greetings"]);
                });
            });
        }
    }
}
