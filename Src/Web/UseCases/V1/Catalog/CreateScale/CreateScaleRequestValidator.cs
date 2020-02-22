using FluentValidation;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleRequestValidator : AbstractValidator<CreateScaleRequest>
    {
        public CreateScaleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(10);

            RuleFor(x => x.Gauge)
                .GreaterThanOrEqualTo(0M);

            RuleFor(x => x.Ratio)
                .GreaterThanOrEqualTo(0M);

            RuleFor(x => x.Notes)
                .MaximumLength(250);

            RuleFor(x => x.TrackGauge)
                .IsEnumName(typeof(TrackGauge), caseSensitive: false);
        }
    }
}