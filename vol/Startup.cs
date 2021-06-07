using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vol.Models.Context;
using vol.Models.Entity;
using vol.Models.Factory;
using vol.Models.Interfaces;
using vol.Models.Repository;
using vol.Models.UnitOfWork;

namespace vol
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option => 
                option.UseLazyLoadingProxies()
                .UseInMemoryDatabase(databaseName : "test")
            );

            services.AddControllersWithViews();
            services.AddScoped<Func<AppDbContext>>((provider) => 
            () => provider.GetService<AppDbContext>());
            
            services.AddScoped<DbFactory>();   
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>)) 
            .AddScoped<IRepository<Flight>,FlightRepository>()
            .AddScoped<IRepository<Plane>,PlaneRepository>()
            .AddScoped<IRepository<Aeroport>,AeroportRepository>();
            
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Flight}/{action=Index}/{id?}");
            });
        }
    }
}
