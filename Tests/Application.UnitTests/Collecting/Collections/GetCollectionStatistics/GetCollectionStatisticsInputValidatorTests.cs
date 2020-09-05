using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public class GetCollectionStatisticsInputValidatorTests
    {
        private GetCollectionStatisticsInputValidator Validator { get; }

        public GetCollectionStatisticsInputValidatorTests()
        {
            Validator = new GetCollectionStatisticsInputValidator();
        }

        [Fact]
        public void GetCollectionStatisticsInputValidator_ShouldFailToValidate_WhenOwnerIsNull()
        {
            var input = new GetCollectionStatisticsInput(null);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetCollectionStatisticsInputValidator_ShouldFailToValidate_WhenOwnerIsEmpty()
        {
            var input = new GetCollectionStatisticsInput("     ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetCollectionStatisticsInputValidator_ShouldValidateValidInputs()
        {
            var input = new GetCollectionStatisticsInput("George");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}