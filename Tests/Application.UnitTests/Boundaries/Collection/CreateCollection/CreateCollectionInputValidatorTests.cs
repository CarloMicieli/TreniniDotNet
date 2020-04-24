using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public class CreateCollectionInputValidatorTests
    {
        private CreateCollectionInputValidator Validator { get; }

        public CreateCollectionInputValidatorTests()
        {
            Validator = new CreateCollectionInputValidator();
        }

        [Fact]
        public void CreateCollectionInput_ShouldSucceedValidation()
        {
            var input = CollectionInputs.CreateCollection.With(Owner: "George", Notes: "My notes");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectionInputs.CreateCollection.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void CreateCollectionInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectionInputs.CreateCollection.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
