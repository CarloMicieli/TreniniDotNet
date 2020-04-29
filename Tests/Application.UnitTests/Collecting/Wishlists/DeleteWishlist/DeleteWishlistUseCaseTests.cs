using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.TestInputs.Collecting;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public class DeleteWishlistUseCaseTests : CollectingUseCaseTests<DeleteWishlistUseCase, DeleteWishlistOutput, DeleteWishlistOutputPort>
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

            await useCase.Execute(CollectingInputs.DeleteWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task DeleteWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewDeleteWishlist);

            var input = CollectingInputs.DeleteWishlist.With(
                Owner: "George",
                Id: Guid.NewGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(input.Id));
        }

        [Fact]
        public async Task DeleteWishlist_ShouldDeleteWishlist()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewDeleteWishlist);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var input = CollectingInputs.DeleteWishlist.With(
                Owner: "George",
                Id: wishlist.WishlistId.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.WishlistId);
        }


        private DeleteWishlistUseCase NewDeleteWishlist(
            WishlistService wishlistsService,
            DeleteWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new DeleteWishlistUseCase(outputPort, wishlistsService, unitOfWork);
        }
    }
}
