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
            //  ���� �������������� ����������, ���� ������ � ������ ����������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  ���������� ������ �������� ����������� ����������
            app.UseStaticFiles();

            //  ������������� ���������� (� ������ ������� ���������� ��������� �������)
            app.UseRouting();

            //  ������������ �������, �� ������� ������������� ������� � ������������ ������
            app.UseEndpoints(endpoints =>
            {
                //  ������ - ����� ("/" - _configuration["Greetings"])
                endpoints.MapGet("/Greetings", async context =>
                {
                    await context.Response.WriteAsync(_configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    "default" /* �������� ��������*/ ,
                    "{controller=Home}/{action=Index}/{id?}" /* ������ ������������� ������ �� �������� ������ � ��������� ��������� MVC */ );
            });
        }
    }
}