using System.Globalization;
using FluentValidation.Validators;

namespace TreniniDotNet.SharedKernel.Countries
{
    internal class CountryCodeValidator : PropertyValidator
    {
        public CountryCodeValidator()
            : base("{PropertyName}: '{PropertyValue}' is not a valid country code")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!(context.PropertyValue is string value))
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
                // Nothing I can do about it
                return false;
            }
        }
    }
}
