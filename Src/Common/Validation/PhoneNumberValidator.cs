using FluentValidation.Validators;
using TreniniDotNet.Common.PhoneNumbers;

namespace TreniniDotNet.Common.Validation
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
}