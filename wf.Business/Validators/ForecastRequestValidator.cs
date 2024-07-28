using FluentValidation;
using wf.Domain.Common;
using wf.Domain.Dto;

namespace wf.Business.Validators;

public sealed class ForecastRequestValidator : AbstractValidator<ForecastRequest>
{
    private const int ForecastDays = 5; // OpenWeatherMap free API limit

    private readonly IDateTimeProvider _dateTimeProvider;

    public ForecastRequestValidator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;

        RuleFor(request => request.City).NotEmpty();
        RuleFor(request => request.Date).Must(ValidateDate).WithMessage("Date should be more than today and less than 5 days ahead.");
    }

    private bool ValidateDate(DateOnly date)
    {
        var isMoreThanNow = date.ToDateTime(TimeOnly.MinValue) > _dateTimeProvider.UtcNow;
        var isLessThanLimit = date.ToDateTime(TimeOnly.MinValue) < _dateTimeProvider.UtcNow.AddDays(ForecastDays);
        return isMoreThanNow && isLessThanLimit;
    }
}
