using AutoMapper;
using LoyaltyPrime.API.Filters;
using LoyaltyPrime.Core;
using LoyaltyPrime.Data;
using LoyaltyPrime.Services;
using LoyaltyPrime.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace LoyaltyPrime.API
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
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(options =>
                                    options.Filters.Add(new ExceptionFilter()))
                    .AddNewtonsoftJson();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMemberAccountService, MemberAccountService>();
            services.AddTransient<IDataService, DataService>();

            services.AddDbContext<LoyaltyPrimeDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    x => x.MigrationsAssembly("LoyaltyPrime.Data")),
                    ServiceLifetime.Transient
                   );

            ConfigureSwagger(services);

        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Member Management System API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loyalty Prime API V1");

            });

            app.UseDeveloperExceptionPage();


        }
    }
}
