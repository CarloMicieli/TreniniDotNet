using FluentValidation;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public sealed class RailwayGaugeInputValidator : AbstractValidator<RailwayGaugeInput>
    {
        public RailwayGaugeInputValidator()
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
