using FluentValidation;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Domain.Validation;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditScale
{
    public sealed class EditScaleInputValidator : AbstractValidator<EditScaleInput>
    {
        public EditScaleInputValidator()
        {
            RuleFor(x => x.ScaleSlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedScaleValuesValidator());
        }
    }

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
