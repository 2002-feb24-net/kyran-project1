using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRealm.DataAccess;
using GameRealm.DataAccess.Model;
using GameRealm.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClientMVC
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

            services.AddRazorPages();

            services.AddDbContext<Game_RealmContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Game_Realm")));
            services.AddTransient<IOrders, OrdersDAL>();
            services.AddTransient<ICustomer, CustomerDAL>();
            services.AddTransient<ILocations, LocationsDAL>();
            services.AddLogging(logger =>
            {
                logger.AddConfiguration(Configuration.GetSection("Logging"));               
                logger.AddConsole();
                logger.AddDebug();

            });

           
            /*services.AddScoped<GameRepository>();*/
            services.AddControllersWithViews();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
