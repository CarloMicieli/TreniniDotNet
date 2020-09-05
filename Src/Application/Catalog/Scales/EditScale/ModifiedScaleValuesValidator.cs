using FluentValidation;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public sealed class ModifiedScaleValuesValidator : AbstractValidator<ModifiedScaleValues>
    {
        public ModifiedScaleValuesValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(1)
                .MaximumLength(10);

            RuleFor(x => x.Gauge)
                .SetValidator(new ScaleGaugeInputValidator());

            RuleFor(x => x.Ratio)
                .GreaterThan(0M);

            RuleFor(x => x.Description)
                .MaximumLength(2500);

            RuleFor(x => x.Weight)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}