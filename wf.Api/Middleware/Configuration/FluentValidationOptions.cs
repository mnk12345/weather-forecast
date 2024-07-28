using FluentValidation;
using Microsoft.Extensions.Options;

namespace wf.Api.Middleware.Configuration;

internal sealed class FluentValidationOptions<TOptions>(string? name, IValidator<TOptions> validator) : IValidateOptions<TOptions> where TOptions : class
{
    public string? Name { get; set; } = name;

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (Name != null && Name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        validator.ValidateAndThrow(options);

        return ValidateOptionsResult.Success;
    }
}
