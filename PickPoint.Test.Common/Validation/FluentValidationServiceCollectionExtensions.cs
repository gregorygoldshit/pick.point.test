using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PickPoint.Test.Common.Validation;

public static class FluentValidationServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
