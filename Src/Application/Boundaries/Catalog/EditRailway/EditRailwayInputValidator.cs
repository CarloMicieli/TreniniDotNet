using FluentValidation;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditRailway
{
    public sealed class EditRailwayInputValidator : AbstractValidator<EditRailwayInput>
    {
        public EditRailwayInputValidator()
        {
            RuleFor(x => x.RailwaySlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedRailwayValuesValidator());
        }
    }

    public sealed class ModifiedRailwayValuesValidator : AbstractValidator<ModifiedRailwayValues>
    {
        public ModifiedRailwayValuesValidator()
        {
            RuleFor(x => x.Name)
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
