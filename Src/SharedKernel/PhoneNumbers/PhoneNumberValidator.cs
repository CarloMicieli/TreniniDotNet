using FluentValidation;
using FluentValidation.Validators;

namespace TreniniDotNet.SharedKernel.PhoneNumbers
{
    public class PhoneNumberValidator : PropertyValidator
    {
        public PhoneNumberValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid phone number")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string? value = context.PropertyValue?.ToString();
            if (value is null)
            {
                return true;
            }

            return PhoneNumber.IsValid(value);
        }
    }

    public static class Foo
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PhoneNumberValidator());
        }
    }
}
