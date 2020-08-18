using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public class EditWishlistUseCaseTests : WishlistUseCaseTests<EditWishlistUseCase, EditWishlistInput, EditWishlistOutput, EditWishlistOutputPort>
    {
        [Fact]
        public async Task EditWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditWishlistInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditWishlist_ShouldReturnError_WhenWishlistToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);
            var id = Guid.NewGuid();

            await useCase.Execute(NewEditWishlistInput.With(id, new Owner("George")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task EditWishlist_ShouldReturnError_WhenOwnerCannotEditTheWishlist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);
            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;

            var input = NewEditWishlistInput.With(
                id,
                new Owner("Richard"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertNotAuthorizedToEditThisWishlist(new Owner("Richard"));
        }

        [Fact]
        public async Task EditWishlist_ShouldEditWishlists()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var input = NewEditWishlistInput.With(
                wishlist.Id,
                wishlist.Owner);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }

        private EditWishlistUseCase CreateUseCase(
            IEditWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new EditWishlistUseCase(new EditWishlistInputValidator(), outputPort, wishlistsService, unitOfWork);
    }
}
