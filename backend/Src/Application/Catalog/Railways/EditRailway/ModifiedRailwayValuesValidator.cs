#nullable disable
using FluentValidation;
using TreniniDotNet.SharedKernel.Countries;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
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
