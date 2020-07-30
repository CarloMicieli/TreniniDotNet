using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public class CreateShopUseCaseTests : ShopUseCaseTests<CreateShopUseCase, CreateShopInput, CreateShopOutput, CreateShopOutputPort>
    {
        [Fact]
        public async Task CreateShop_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateShop_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewCreateShopInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateShop_ShouldOutputError_WhenShopSlugAlreadyUsed()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(NewCreateShopInput.With(name: "Tecnomodel"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertShopAlreadyExists("Tecnomodel");
        }

        [Fact]
        public async Task CreateShop_ShouldCreateNewShops()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var id = Guid.NewGuid();
            SetNextGeneratedGuid(id);

            var input = NewCreateShopInput.With(name: "Tecnomodel");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(new ShopId(id));
            output.Slug.Should().Be(Slug.Of("Tecnomodel"));
        }

        private CreateShopUseCase CreateUseCase(
            ICreateShopOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new CreateShopUseCase(new CreateShopInputValidator(), outputPort, shopsService, unitOfWork);
    }
}
