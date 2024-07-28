using FluentValidation;

namespace wf.Api.Middleware.ModelValidation;

public static class ModelValidationsPipelineExtensions
{
    public static IServiceCollection ConfigureModelValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(Program), ServiceLifetime.Singleton);
        services.AddValidatorsFromAssemblyContaining(typeof(Business.Bootstrapper));

        return services;
    }
}
