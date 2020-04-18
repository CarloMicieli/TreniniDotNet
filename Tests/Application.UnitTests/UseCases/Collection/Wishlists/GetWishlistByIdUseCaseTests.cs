using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class GetWishlistByIdUseCaseTests : CollectionUseCaseTests<GetWishlistById, GetWishlistByIdOutput, GetWishlistByIdOutputPort>
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

            await useCase.Execute(new GetWishlistByIdInput(Guid.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputError_WhenTheWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistById);

            var id = Guid.NewGuid();
            await useCase.Execute(new GetWishlistByIdInput(id));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistWasNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task GetWishlistById_ShouldOutputTheWishlist()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewGetWishlistById);

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            await useCase.Execute(new GetWishlistByIdInput(id.ToGuid()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Wishlist.Should().NotBeNull();
            output.Wishlist.WishlistId.Should().Be(id);
        }

        private GetWishlistById NewGetWishlistById(
            WishlistService wishlistService,
            GetWishlistByIdOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetWishlistById(outputPort, wishlistService);
        }
    }
}
