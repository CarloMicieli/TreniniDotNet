using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class DeleteWishlistUseCaseTests : CollectionUseCaseTests<DeleteWishlist, DeleteWishlistOutput, DeleteWishlistOutputPort>
    {
        [Fact]
        public async Task DeleteWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewDeleteWishlist);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task DeleteWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewDeleteWishlist);

            await useCase.Execute(CollectionInputs.DeleteWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task DeleteWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewDeleteWishlist);

            var input = CollectionInputs.DeleteWishlist.With(Id: Guid.NewGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(input.Id));
        }

        [Fact]
        public async Task DeleteWishlist_ShouldDeleteWishlist()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewDeleteWishlist);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var input = CollectionInputs.DeleteWishlist.With(Id: wishlist.WishlistId.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.WishlistId);
        }


        private DeleteWishlist NewDeleteWishlist(
            WishlistService collectionService,
            DeleteWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new DeleteWishlist(outputPort, collectionService, unitOfWork);
        }
    }
}
