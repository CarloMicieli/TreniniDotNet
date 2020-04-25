using FluentValidation;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public sealed class CreateRailwayInputValidator : AbstractValidator<CreateRailwayInput>
    {
        public CreateRailwayInputValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.Country)
                .CountryCode();

            RuleFor(x => x.PeriodOfActivity)
                .SetValidator(new PeriodOfActivityInputValidator());

            RuleFor(x => x.TotalLength)
                .SetValidator(new TotalRailwayLengthInputValidator());

            RuleFor(x => x.Gauge)
                .SetValidator(new RailwayGaugeInputValidator());
        }
    }
}