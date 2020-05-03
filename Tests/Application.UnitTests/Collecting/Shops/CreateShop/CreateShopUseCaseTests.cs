using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Shops.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public class CreateShopUseCaseTests : CollectingUseCaseTests<CreateShopUseCase, CreateShopOutput, CreateShopOutputPort>
    {
        [Fact]
        public async Task CreateShop_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewCreateShop);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateShop_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewCreateShop);

            await useCase.Execute(CollectingInputs.CreateShop.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateShop_ShouldOutputError_WhenShopSlugAlreadyUsed()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.WithSeedData, NewCreateShop);

            await useCase.Execute(CollectingInputs.CreateShop.With(name: "Tecnomodel"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertShopAlreadyExists("Tecnomodel");
        }

        [Fact]
        public async Task CreateShop_ShouldCreateNewShops()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeShopUseCase(Start.Empty, NewCreateShop);

            var id = Guid.NewGuid();
            SetNextGeneratedGuid(id);

            var input = CollectingInputs.CreateShop.With(name: "Tecnomodel");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(new ShopId(id));
            output.Slug.Should().Be(Slug.Of("Tecnomodel"));
        }

        private CreateShopUseCase NewCreateShop(
            ShopsService shopsService,
            CreateShopOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateShopUseCase(outputPort, shopsService, unitOfWork);
        }
    }
}
