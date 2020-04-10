using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;
using Xunit;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateRailwayUseCaseTests : UseCaseTestHelper<CreateRailway, CreateRailwayOutput, CreateRailwayOutputPort>
    {
        [Fact]
        public async Task CreateRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            await useCase.Execute(NewRailwayInput.NewEmpty());

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
            var input = NewRailwayInput.With(
                Name: name, 
                CompanyName: "Die Bahn", 
                Country: "DE",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "active",
                    OperatingSince: DateTime.Now.AddDays(-1), 
                    OperatingUntil: null));

            await useCase.Execute(input);

            outputPort.ShouldHaveRailwayAlreadyExistsMessage($"Railway '{name}' already exists");
        }

        [Fact]
        public async Task CreateRailway_Should_CreateANewRailway()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            var input = NewRailwayInput.With(
                Name: "DB",
                CompanyName: "Die Bahn",
                Country: "DE",
                PeriodOfActivity: NewPeriodOfActivityInput.With(
                    Status: "active",
                    OperatingSince: DateTime.Now.AddDays(-1),
                    OperatingUntil: null),
                TotalLength: NewTotalRailwayLengthInput.With(
                    Kilometers: 1000M,
                    Miles: 3445M),
                Gauge: NewRailwayGaugeInput.With(
                    TrackGauge: TrackGauge.Standard.ToString(),
                    Millimeters: 1435M,
                    Inches: 56.5M));
            
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
