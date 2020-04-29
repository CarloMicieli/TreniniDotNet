using System;
using FluentValidation.Validators;

namespace TreniniDotNet.Common.Validation
{
    internal class UriValidator : PropertyValidator
    {
        public UriValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid URI")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string? value = context.PropertyValue as string;
            if (value is null)
            {
                return true;
            }

            return Uri.TryCreate(value, UriKind.Absolute, out _);
        }
    }
}