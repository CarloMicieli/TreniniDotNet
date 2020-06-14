using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public class CreateRailwayUseCaseTests : CatalogUseCaseTests<CreateRailwayUseCase, CreateRailwayOutput, CreateRailwayOutputPort>
    {
        [Fact]
        public async Task CreateRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            await useCase.Execute(NewCreateRailwayInput.Empty);

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
            var input = NewCreateRailwayInput.With(
                name: name,
                companyName: "Die Bahn",
                country: "DE",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "active",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: null));

            await useCase.Execute(input);

            outputPort.AssertRailwayAlreadyExists(Slug.Of("DB"));
        }

        [Fact]
        public async Task CreateRailway_Should_CreateANewRailway()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeRailwaysUseCase(Start.Empty, NewCreateRailway);

            var expectedSlug = Slug.Of("db");

            var input = NewCreateRailwayInput.With(
                name: "DB",
                companyName: "Die Bahn",
                country: "DE",
                periodOfActivity: NewPeriodOfActivityInput.With(
                    status: "active",
                    operatingSince: DateTime.Now.AddDays(-1),
                    operatingUntil: null),
                totalLength: NewTotalRailwayLengthInput.With(
                    kilometers: 1000M,
                    miles: 3445M),
                gauge: NewRailwayGaugeInput.With(
                    trackGauge: TrackGauge.Standard.ToString(),
                    millimeters: 1435M,
                    inches: 56.5M));

            await useCase.Execute(input);

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.Slug.Should().NotBeNull();
            output.Slug.Should().Be(expectedSlug);

            dbContext.Railways.Any(it => it.Slug == expectedSlug).Should().BeTrue();
        }

        private CreateRailwayUseCase NewCreateRailway(RailwayService railwayService, CreateRailwayOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateRailwayUseCase(outputPort, railwayService, unitOfWork);
        }
    }
}
