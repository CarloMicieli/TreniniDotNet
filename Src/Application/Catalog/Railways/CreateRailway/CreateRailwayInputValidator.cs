using FluentValidation;
using TreniniDotNet.Common.Validation;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
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