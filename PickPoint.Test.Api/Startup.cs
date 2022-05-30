using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Test.Common.Swagger.Extensions;
using PickPoint.Test.Common.Validation;
using PickPoint.Test.Infrastructure;
using PickPoint.Test.Infrastructure.UnitOfWork;
using Serilog;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace PickPoint.Test.Api
{
    public class Startup
    {
        private const string APPLICATION_MODULE_NAME = "PickPoint.Test.Application";
        private const string API_TITLE = "PickPoint Test API";
        private const string API_VERSION = "v1";
        private const string SERVICE_NAME = "pickpoint.test";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain.Load(APPLICATION_MODULE_NAME);

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddFluentValidation(applicationAssembly);
            services.AddMediatR(applicationAssembly);
            services.AddNpgsql<PickPointDbContext>(Configuration.GetConnectionString("PostgresConnection"));
            services.AddSwagger(API_TITLE,
                API_VERSION,
                Assembly.GetExecutingAssembly().GetName().Name, applicationAssembly.GetName().Name);
            services.AddScoped<IPickPointUnitOfWork, PickPointUnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerWithDefaultUi($"{API_TITLE} {API_VERSION}");
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
