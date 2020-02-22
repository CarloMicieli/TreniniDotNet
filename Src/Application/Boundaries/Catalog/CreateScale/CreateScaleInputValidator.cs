using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public sealed class CreateScaleInputValidator : AbstractValidator<CreateScaleInput>
    {
        public CreateScaleInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(10);

            RuleFor(x => x.Gauge)
                .GreaterThanOrEqualTo(0M);

            RuleFor(x => x.Ratio)
                .GreaterThanOrEqualTo(0M);
        }
    }
}
