using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public sealed class CreateRailwayUseCaseTests : RailwayUseCaseTests<CreateRailwayUseCase, CreateRailwayInput, CreateRailwayOutput, CreateRailwayOutputPort>
    {
        [Fact]
        public async Task CreateRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewCreateRailwayInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateRailway_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateRailway_ShouldNotCreateANewRailway_WhenRailwayAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.Empty, CreateUseCase);

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

        private CreateRailwayUseCase CreateUseCase(
            CreateRailwayOutputPort outputPort,
            RailwaysService railwaysService,
            IUnitOfWork unitOfWork) =>
            new CreateRailwayUseCase(new CreateRailwayInputValidator(), outputPort, railwaysService, unitOfWork);
    }
}
