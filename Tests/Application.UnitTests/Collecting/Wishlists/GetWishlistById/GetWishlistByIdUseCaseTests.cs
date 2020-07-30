using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public class GetWishlistByIdUseCaseTests : WishlistUseCaseTests<GetWishlistByIdUseCase, GetWishlistByIdInput, GetWishlistByIdOutput, GetWishlistByIdOutputPort>
    {
        [Fact]
        public async Task GetWishlistById_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetWishlistById_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetWishlistByIdInput(null, Guid.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputError_WhenTheWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var id = Guid.NewGuid();
            await useCase.Execute(new GetWishlistByIdInput("George", id));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistWasNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputTheWishlist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var id = CollectingSeedData.Wishlists.GeorgeFirstList().Id;
            await useCase.Execute(new GetWishlistByIdInput("George", id));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Wishlist.Should().NotBeNull();
            output.Wishlist.Id.Should().Be(id);
        }

        private GetWishlistByIdUseCase CreateUseCase(
            IGetWishlistByIdOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new GetWishlistByIdUseCase(new GetWishlistByIdInputValidator(), outputPort, wishlistsService);
    }
}
