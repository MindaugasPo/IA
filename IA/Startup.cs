using IADbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Business;
using IA.Filters;
using Microsoft.AspNetCore.Identity;
using Services;
using Types.Entities;
using ValidationService;

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
            services.AddCors(o => o.AddPolicy("All", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddSingleton<IAValidatorFactory, ValidatorFactory>();
            services.AddAutoMapper(typeof(AutoMapperConfigurations));
            
            RegisterDbContexts(services);
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IAContext>();

            RegisterIaFilters(services);
            RegisterIaServices(services);
            RegisterIaBusiness(services);
            RegisterFinancialDataClient(services);
            services.AddControllersWithViews();
            services.AddRazorPages();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("All");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void RegisterIaFilters(IServiceCollection services)
        {
            services.AddScoped<IaValidationFilter>();
        }
        private void RegisterDbContexts(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<IAContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IADB1_dev")));
            }
            else
            {
                services.AddDbContext<IAContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IADB1_prod")));
            }
        }

        private void RegisterIaServices(IServiceCollection services)
        {
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetPriceService, AssetPriceService>();
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
