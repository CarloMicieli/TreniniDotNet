using FluentValidation;
using FluentValidation.Validators;

namespace TreniniDotNet.SharedKernel.Slugs
{
    internal class SlugValidator : PropertyValidator
    {
        public SlugValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid slug")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            Slug? value = (Slug)context.PropertyValue;
            return value.HasValue && value.Value != Slug.Empty;
        }
    }

    public static class Foo
    {
        public static IRuleBuilderOptions<T, Slug> ValidSlug<T>(this IRuleBuilder<T, Slug> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SlugValidator());
        }
    }
}
