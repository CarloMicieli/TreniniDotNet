using FluentValidation;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class LengthOverBufferInputValidator : AbstractValidator<LengthOverBufferInput>
    {
        public LengthOverBufferInputValidator()
        {
            RuleFor(x => x.Millimeters)
                .GreaterThan(0);

            RuleFor(x => x.Inches)
                .GreaterThan(0);
        }
    }
}
