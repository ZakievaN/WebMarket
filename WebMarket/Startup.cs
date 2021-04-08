using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebMarket.DAL.Context;
using WebMarket.Data;
using WebMarket.Infrastructure.Conventions;
using WebMarket.Infrastructure.Services.InMemory;
using WebMarket.Infrastructure.Services.InSQL;
using WebMarket.Infrastructure.Services.Interfaces;

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

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();


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
                    await context.Response.WriteAsync(configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    "default" /* �������� ��������*/ ,
                    "{controller=Home}/{action=Index}/{id?}" /* ������ ������������� ������ �� �������� ������ � ��������� ��������� MVC */ );
            });
        }
    }
}