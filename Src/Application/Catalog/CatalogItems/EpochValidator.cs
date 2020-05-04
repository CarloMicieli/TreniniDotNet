using FluentValidation;
using FluentValidation.Validators;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class EpochValidator : PropertyValidator
    {
        public EpochValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid epoch")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!(context.PropertyValue is string value))
            {
                return true;
            }

            return Epoch.TryParse(value, out var _);
        }
    }

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string?> ValidEpoch<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EpochValidator());
        }
    }
}
