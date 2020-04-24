using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shops;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public class GetShopBySlugUseCaseTests : CollectionUseCaseTests<GetShopBySlug, GetShopBySlugOutput, GetShopBySlugOutputPort>
    {
        [Fact]
        public async Task GetShopBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewGetShopBySlug);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetShopBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewGetShopBySlug);

            await useCase.Execute(new GetShopBySlugInput(null));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetShopBySlug_ShouldOutputError_WhenShopWasNotFound()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewGetShopBySlug);

            var expectedSlug = Slug.Of("not found");

            await useCase.Execute(new GetShopBySlugInput(expectedSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertShopWasNotFound(expectedSlug);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldOutputTheShop()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.WithSeedData, NewGetShopBySlug);

            var expectedSlug = Slug.Of("Tecnomodel");

            await useCase.Execute(new GetShopBySlugInput(expectedSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Shop.Should().NotBeNull();
            output.Shop.Slug.Should().Be(expectedSlug);
        }


        private GetShopBySlug NewGetShopBySlug(
            ShopsService shopsService,
            GetShopBySlugOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetShopBySlug(outputPort, shopsService);
        }
    }
}
