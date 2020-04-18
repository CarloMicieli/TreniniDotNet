using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public class CreateShopUseCaseTests : CollectionUseCaseTests<CreateShop, CreateShopOutput, CreateShopOutputPort>
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

            await useCase.Execute(CollectionInputs.CreateShop.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateShop_ShouldOutputError_WhenShopSlugAlreadyUsed()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.WithSeedData, NewCreateShop);

            await useCase.Execute(CollectionInputs.CreateShop.With(Name: "Tecnomodel"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertShopAlreadyExists("Tecnomodel");
        }

        [Fact]
        public async Task CreateShop_ShouldCreateNewShops()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeShopUseCase(Start.Empty, NewCreateShop);

            var id = Guid.NewGuid();
            SetNextGeneratedGuid(id);

            var input = CollectionInputs.CreateShop.With(Name: "Tecnomodel");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(new ShopId(id));
            output.Slug.Should().Be(Slug.Of("Tecnomodel"));
        }

        private CreateShop NewCreateShop(
            ShopsService shopsService,
            CreateShopOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateShop(outputPort, shopsService, unitOfWork);
        }
    }
}
