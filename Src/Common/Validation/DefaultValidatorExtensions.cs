#nullable disable
using FluentValidation;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Validation
{
    public static class DefaultValidatorExtensions
    {
		/// <summary>
		/// Defines a Url validator for the current rule builder that ensures that the specified string is a 
		/// valid url
		/// </summary>
		public static IRuleBuilderOptions<T, string> AbsoluteUri<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new UriValidator());
		}

		public static IRuleBuilderOptions<T, string> CountryCode<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new CountryCodeValidator());
		}

		public static IRuleBuilderOptions<T, Slug> ValidSlug<T>(this IRuleBuilder<T, Slug> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new SlugValidator());
		}
	}
}
