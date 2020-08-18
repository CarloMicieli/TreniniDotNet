using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public class GetShopBySlugUseCaseTests : ShopUseCaseTests<GetShopBySlugUseCase, GetShopBySlugInput, GetShopBySlugOutput, GetShopBySlugOutputPort>
    {
        [Fact]
        public async Task GetShopBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetShopBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetShopBySlugInput(null));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetShopBySlug_ShouldOutputError_WhenShopWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var expectedSlug = Slug.Of("not found");

            await useCase.Execute(new GetShopBySlugInput(expectedSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertShopWasNotFound(expectedSlug);
        }

        [Fact]
        public async Task GetShopBySlug_ShouldOutputTheShop()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var expectedSlug = Slug.Of("Tecnomodel");

            await useCase.Execute(new GetShopBySlugInput(expectedSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Shop.Should().NotBeNull();
            output.Shop.Slug.Should().Be(expectedSlug);
        }

        private GetShopBySlugUseCase CreateUseCase(
            IGetShopBySlugOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new GetShopBySlugUseCase(new GetShopBySlugInputValidator(), outputPort, shopsService);
    }
}
