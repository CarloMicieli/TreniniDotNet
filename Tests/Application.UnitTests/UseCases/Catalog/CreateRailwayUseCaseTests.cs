using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateRailwayUseCaseTests : UseCaseTestHelper<CreateRailway, CreateRailwayOutput, CreateRailwayOutputPort>
    {
        [Fact]
        public async Task CreateRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            await useCase.Execute(new CreateRailwayInput(null, null, null, null, null, null));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateRailway_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateRailway_ShouldNotCreateANewRailway_WhenRailwayAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.WithSeedData, NewCreateRailway);

            var name = "DB";
            var input = new CreateRailwayInput(name, "Die Bahn", "DE", "active", DateTime.Now.AddDays(-1), null);

            await useCase.Execute(input);

            outputPort.ShouldHaveRailwayAlreadyExistsMessage($"Railway '{name}' already exists");
        }

        [Fact]
        public async Task CreateRailway_Should_CreateANewRailway()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "active", DateTime.Now.AddDays(-1), null);

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;

            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("db"), output!.Slug);
        }

        private CreateRailway NewCreateRailway(RailwayService railwayService, CreateRailwayOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateRailway(outputPort, railwayService, unitOfWork);
        }
    }
}
