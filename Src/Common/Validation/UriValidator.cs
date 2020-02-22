using FluentValidation.Resources;
using FluentValidation.Validators;
using System;

namespace TreniniDotNet.Domain.Validation
{
    internal class UriValidator : PropertyValidator
    {
        public UriValidator() 
            : base(new LanguageStringSource(nameof(UriValidator)))
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