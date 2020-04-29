using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IADbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Business;
using FinancialDataClient;
using Services;

namespace IA
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddAutoMapper(typeof(AutoMapperConfigurations));

            services.AddControllersWithViews();

            RegisterDbContexts(services);
            RegisterIaServices(services);
            RegisterIaBusiness(services);
            RegisterFinancialDataClient(services);
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
            }
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


        private void RegisterDbContexts(IServiceCollection services)
        {
            //if (_env.IsDevelopment())
            //{
            //    services.AddDbContext<IAContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("IADB1_dev")));
            //}
            //else
            //{
                services.AddDbContext<IAContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:IADB1_prod"]));
            //}
        }

        private void RegisterIaServices(IServiceCollection services)
        {
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
        }

        private void RegisterIaBusiness(IServiceCollection services)
        {
            services.AddScoped<IPortfolioBusiness, PortfolioBusiness>();
        }

        private void RegisterFinancialDataClient(IServiceCollection services)
        {
            //services.AddScoped<IFinDataClient>(x => new FinDataClient(Configuration.GetConnectionString("IADatabase")));
        }
    }
}
