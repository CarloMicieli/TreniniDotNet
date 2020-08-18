using FluentValidation;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class LengthOverBufferInputValidator : AbstractValidator<LengthOverBufferInput?>
    {
        public LengthOverBufferInputValidator()
        {
            RuleFor(x => x!.Millimeters)
                .GreaterThan(0);

            RuleFor(x => x!.Inches)
                .GreaterThan(0);
        }
    }

    public static class LengthOverBufferInputValidatorExtensions
    {
        public static IRuleBuilderOptions<T, LengthOverBufferInput?> ValidLengthOverBuffer<T>(this IRuleBuilder<T, LengthOverBufferInput?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new LengthOverBufferInputValidator());
        }
    }
}
