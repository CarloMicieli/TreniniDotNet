using FluentValidation.TestHelper;
using System;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection
{
    public class RemoveItemFromCollectionInputValidatorTests
    {
        private RemoveItemFromCollectionInputValidator Validator { get; }

        public RemoveItemFromCollectionInputValidatorTests()
        {
            Validator = new RemoveItemFromCollectionInputValidator();
        }


        [Fact]
        public void RemoveItemFromCollectionInput_ShouldSucceedValidation()
        {
            var input = CollectionInputs.RemoveItemFromCollection.With(
                Owner: new Owner("George"),
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RemoveItemFromCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectionInputs.RemoveItemFromCollection.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }
    }
}
