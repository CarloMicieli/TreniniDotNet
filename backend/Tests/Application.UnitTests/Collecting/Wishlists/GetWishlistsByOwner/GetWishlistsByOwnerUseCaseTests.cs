using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public class GetWishlistsByOwnerUseCaseTests : WishlistUseCaseTests<GetWishlistsByOwnerUseCase, GetWishlistsByOwnerInput, GetWishlistsByOwnerOutput, GetWishlistsByOwnerOutputPort>
    {
        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetWishlistsByOwnerInput("", Visibility.Public.ToString()));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputError_WhenNoWishlistWasFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetWishlistsByOwnerInput("George", Visibility.Public.ToString()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistsNotFoundForTheOwner(
                new Owner("George"),
                VisibilityCriteria.Public);
        }

        [Fact]
        public async Task GetWishlistsByOwner_ShouldOutputWishlistsByOwner()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetWishlistsByOwnerInput("George", Visibility.Private.ToString()));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Owner.Should().Be(new Owner("George"));
            output.Visibility.Should().Be(VisibilityCriteria.Private);
            output.Wishlists.Should().NotBeNull();
        }

        private GetWishlistsByOwnerUseCase CreateUseCase(
            IGetWishlistsByOwnerOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new GetWishlistsByOwnerUseCase(new GetWishlistsByOwnerInputValidator(), outputPort, wishlistsService);
    }
}
