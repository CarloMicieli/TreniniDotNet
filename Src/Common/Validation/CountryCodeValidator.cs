using FluentValidation.Validators;
using System.Globalization;

namespace TreniniDotNet.Domain.Validation
{
    internal class CountryCodeValidator : PropertyValidator
    {
        public CountryCodeValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid country code")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            string? value = context.PropertyValue as string;
            if (value is null)
            {
                return true;
            }
            return TryConvert(value);
        }

        private bool TryConvert(string countryCode)
        {
            try
            {
                var _ = new RegionInfo(countryCode);
                return true;
            }
            catch
            {
                // Nothing I can do aboout it
                return false;
            }
        }
    }
}