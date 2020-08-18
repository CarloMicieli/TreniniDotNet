using FluentValidation;

namespace TreniniDotNet.Application.Catalog.Railways
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
