using FluentValidation;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public sealed class CreateScaleInputValidator : AbstractValidator<CreateScaleInput>
    {
        public CreateScaleInputValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
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

    public sealed class ScaleGaugeInputValidator : AbstractValidator<ScaleGaugeInput>
    {
        public ScaleGaugeInputValidator()
        {
            RuleFor(x => x.TrackGauge)
                .IsEnumName(typeof(TrackGauge), caseSensitive: false);

            RuleFor(x => x.Millimeters)
                .GreaterThan(0M);

            RuleFor(x => x.Inches)
                .GreaterThan(0M);
        }
    }
}
