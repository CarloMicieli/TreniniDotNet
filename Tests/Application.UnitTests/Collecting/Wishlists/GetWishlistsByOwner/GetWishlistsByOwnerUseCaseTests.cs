using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public class GetWishlistsByOwnerUseCaseTests : CollectingUseCaseTests<GetWishlistsByOwnerUseCase, GetWishlistsByOwnerOutput, GetWishlistsByOwnerOutputPort>
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

        private GetWishlistsByOwnerUseCase NewGetWishlistsByOwner(
            WishlistService wishlistsService,
            GetWishlistsByOwnerOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetWishlistsByOwnerUseCase(outputPort, wishlistsService);
        }
    }
}
