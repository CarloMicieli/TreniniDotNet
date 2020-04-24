using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class GetWishlistsByOwnerUseCaseTests : CollectionUseCaseTests<GetWishlistsByOwner, GetWishlistsByOwnerOutput, GetWishlistsByOwnerOutputPort>
    {
        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistsByOwner);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistsByOwner);

            await useCase.Execute(new GetWishlistsByOwnerInput("", Visibility.Public.ToString()));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputError_WhenNoWishlistWasFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewGetWishlistsByOwner);

            await useCase.Execute(new GetWishlistsByOwnerInput("George", Visibility.Public.ToString()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistsNotFoundForTheOwner(
                new Owner("George"),
                VisibilityCriteria.Public);
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputWishlistsByOwner()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewGetWishlistsByOwner);

            await useCase.Execute(new GetWishlistsByOwnerInput("George", Visibility.Private.ToString()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Owner.Should().Be(new Owner("George"));
            output.Visibility.Should().Be(VisibilityCriteria.Private);
            output.Wishlists.Should().NotBeNull();
        }

        private GetWishlistsByOwner NewGetWishlistsByOwner(
            WishlistService wishlistsService,
            GetWishlistsByOwnerOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetWishlistsByOwner(outputPort, wishlistsService);
        }
    }
}
