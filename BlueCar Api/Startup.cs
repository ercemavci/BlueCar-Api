using BlueCar.Data.Data.Access.Concrete;
using BlueCar.Payments;
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

namespace BlueCar_Api
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
            services.AddControllers();
            services.AddMongoDbSettings(Configuration);
            services.AddSingleton<IBlueCarsDal, CarsMongoDbDal>();
            services.AddSingleton<IReservationsDal, ReservationsMongoDbDal>();
            services.AddSingleton<ICompanyDal, CompanyMongoDbDal>();
            services.AddSingleton<IBlueCarsListDal, carListMongoDbDal>();
            services.AddSingleton<IBlueCarsLocationDal, LocationMongoDbDal>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "BLUECAR API", Version = "v1" });
            });
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<IyzipaySettings>(Configuration.GetSection("IyzipaySettings"));

            services.AddControllers();

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration _configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var applicationName = "";

            if (!env.IsDevelopment())
            {
                applicationName = "/" + _configuration.GetValue<IyzipaySettings>("IyzicoSettings:ApiKey");
            }
            app.UseRouting();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "BLUECAR API");
            });
   
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors("AllowAnyOrigin");
        }
    }
}
