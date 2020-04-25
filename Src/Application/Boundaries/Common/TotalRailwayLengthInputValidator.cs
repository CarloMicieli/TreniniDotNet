using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Common
{
    public sealed class TotalRailwayLengthInputValidator : AbstractValidator<TotalRailwayLengthInput>
    {
        public TotalRailwayLengthInputValidator()
        {
            RuleFor(x => x.Kilometers)
                .GreaterThan(0);

            RuleFor(x => x.Miles)
                .GreaterThan(0);
        }
    }
}
