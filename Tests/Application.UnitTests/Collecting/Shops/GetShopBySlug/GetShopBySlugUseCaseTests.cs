using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Shops.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public class GetShopBySlugUseCaseTests : CollectingUseCaseTests<GetShopBySlugUseCase, GetShopBySlugOutput, GetShopBySlugOutputPort>
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


        private GetShopBySlugUseCase NewGetShopBySlug(
            ShopsService shopsService,
            GetShopBySlugOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetShopBySlugUseCase(outputPort, shopsService);
        }
    }
}
