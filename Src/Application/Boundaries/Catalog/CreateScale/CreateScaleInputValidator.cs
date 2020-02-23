using FluentValidation;
using TreniniDotNet.Domain.Catalog.Scales;

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
                .GreaterThan(0M);

            RuleFor(x => x.Ratio)
                .GreaterThan(0M);

            RuleFor(x => x.TrackGauge)
                .IsEnumName(typeof(TrackGauge), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(2500);
        }
    }
}
