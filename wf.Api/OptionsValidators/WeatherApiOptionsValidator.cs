using FluentValidation;
using wf.Domain.Options;

namespace wf.Api.OptionsValidators;

public sealed class WeatherApiOptionsValidator : AbstractValidator<WeatherApiOptions>
{
    public WeatherApiOptionsValidator()
    {
        RuleFor(request => request.ApiKey).NotEmpty();
        RuleFor(request => request.BaseUrl).NotEmpty();
    }
}
