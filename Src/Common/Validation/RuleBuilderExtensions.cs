#nullable disable
using FluentValidation;

namespace TreniniDotNet.Common.Validation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> AbsoluteUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new UriValidator());
        }
    }
}
