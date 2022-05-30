using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace PickPoint.Test.Common.Swagger.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string version, params string[] assemblyNames)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = title,
                Version = version,
                Contact = new OpenApiContact
                {
                    Name = "PickPoint",
                    Url = new Uri("https://pickpoint.ru")
                },
                License = new OpenApiLicense
                {
                    Name = "PickPoint API License",
                    Url = new Uri("https://pickpoint.ru")
                }
            });

            foreach (var assemblyName in assemblyNames)
            {
                var xmlFilename = $"{assemblyName}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
            }

            var xmlCommonFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommonFilename), true);
        });

        services.AddFluentValidationRulesToSwagger();

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithDefaultUi(this IApplicationBuilder app, string name)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.EnableDeepLinking();
            c.DisplayRequestDuration();
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{name}");
            c.DocExpansion(DocExpansion.None);
        });

        return app;
    }
}
