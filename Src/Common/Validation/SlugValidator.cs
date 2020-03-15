using FluentValidation.Resources;
using FluentValidation.Validators;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Validation
{
    internal class SlugValidator : PropertyValidator
    {
        public SlugValidator()
            : base(new LanguageStringSource(nameof(SlugValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            Slug? value = (Slug)context.PropertyValue;
            return value.HasValue && value.Value != Slug.Empty;
        }
    }
}