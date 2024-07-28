using FluentValidation;
using Microsoft.Extensions.Options;

namespace wf.Api.Middleware.Configuration;

internal static class OptionsBuildFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(x => new FluentValidationOptions<TOptions>(optionsBuilder.Name, x.GetRequiredService<IValidator<TOptions>>()));
        return optionsBuilder;
    }
}
