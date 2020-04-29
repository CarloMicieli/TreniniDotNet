using FluentValidation.Validators;

namespace TreniniDotNet.Common.Validation
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
}