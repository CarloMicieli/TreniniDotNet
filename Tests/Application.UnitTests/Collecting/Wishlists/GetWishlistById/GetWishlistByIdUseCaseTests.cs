using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public class GetWishlistByIdUseCaseTests : CollectingUseCaseTests<GetWishlistByIdUseCase, GetWishlistByIdOutput, GetWishlistByIdOutputPort>
    {
        [Fact]
        public async Task GetWishlistById_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistById);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetWishlistById_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistById);

            await useCase.Execute(new GetWishlistByIdInput(null, Guid.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputError_WhenTheWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistById);

            var id = Guid.NewGuid();
            await useCase.Execute(new GetWishlistByIdInput("George", id));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistWasNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputTheWishlist()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewGetWishlistById);

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            await useCase.Execute(new GetWishlistByIdInput("George", id.ToGuid()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Wishlist.Should().NotBeNull();
            output.Wishlist.WishlistId.Should().Be(id);
        }

        private GetWishlistByIdUseCase NewGetWishlistById(
            WishlistService wishlistService,
            GetWishlistByIdOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetWishlistByIdUseCase(outputPort, wishlistService);
        }
    }
}
