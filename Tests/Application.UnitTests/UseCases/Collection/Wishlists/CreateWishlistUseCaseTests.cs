using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class CreateWishlistUseCaseTests : CollectionUseCaseTests<CreateWishlist, CreateWishlistOutput, CreateWishlistOutputPort>
    {
        [Fact]
        public async Task CreateWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewCreateWishlist);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewCreateWishlist);

            await useCase.Execute(CollectionInputs.CreateWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateWishlist_ShouldOutputError_WhenAlreadyExistsWishlistWithTheSameSlug()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewCreateWishlist);

            var input = CollectionInputs.CreateWishlist.With(
                Owner: "George",
                ListName: "First list",
                Visibility: "Private");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistAlreadyExistsWithSlug(Slug.Of("First list"));
        }

        [Fact]
        public async Task CreateWishlist_ShouldCreateNewWishlist()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewCreateWishlist);

            var guid = Guid.NewGuid();
            SetNextGeneratedGuid(guid);

            var input = CollectionInputs.CreateWishlist.With(
                Owner: "George",
                ListName: "Second list",
                Visibility: "Private");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.WishlistId.Should().Be(new WishlistId(guid));
            output.Slug.Should().Be(Slug.Of("Second list"));
        }

        private CreateWishlist NewCreateWishlist(
            WishlistService wishlistsService,
            CreateWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateWishlist(outputPort, wishlistsService, unitOfWork);
        }
    }
}
