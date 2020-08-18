using FluentValidation;

namespace TreniniDotNet.SharedKernel.Countries
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> CountryCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CountryCodeValidator());
        }
    }
}
