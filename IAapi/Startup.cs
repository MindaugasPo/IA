using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IADbContext;
using Microsoft.EntityFrameworkCore;
using Services;
using ValidationService;

namespace IAapi
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
            
            services.AddSingleton<IAValidatorFactory, ValidatorFactory>();
            services.AddAutoMapper(typeof(AutoMapperConfigurations));

            RegisterDbContexts(services);

            RegisterIaServices(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IAapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IAapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("All");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
        }
    }
}
