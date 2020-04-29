using FluentValidation;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
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
}
