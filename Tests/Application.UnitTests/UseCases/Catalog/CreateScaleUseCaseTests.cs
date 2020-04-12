using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateScaleUseCaseTests : UseCaseTestHelper<CreateScale, CreateScaleOutput, CreateScaleOutputPort>
    {
        [Fact]
        public async Task CreateScale_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            await useCase.Execute(NewScaleInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateScale_Should_CreateANewScale()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            var input = NewScaleInput.With(
                Name: "H0",
                Ratio: 87M,
                Gauge: NewScaleGaugeInput.With(
                    Millimeters: 16.5M,
                    TrackGauge: TrackGauge.Standard.ToString()),
                Description: "notes");

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.Slug.Should().NotBeNull();
            output.Slug.Should().Be(Slug.Of("H0"));
        }

        [Fact]
        public async Task CreateScale_ShouldNotCreateANewScale_WhenScaleAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewCreateScale);

            var name = "H0";
            var input = NewScaleInput.With(
                Name: name,
                Ratio: 87M,
                Gauge: NewScaleGaugeInput.With(
                    Millimeters: 16.5M,
                    TrackGauge: TrackGauge.Standard.ToString()),
                Description: "notes");

            await useCase.Execute(input);

            outputPort.ShouldHaveScaleAlreadyExistsMessage($"Scale '{name}' already exists");
        }

        [Fact]
        public async Task CreateScale_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewCreateScale);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        private CreateScale NewCreateScale(ScaleService scaleService, CreateScaleOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateScale(outputPort, scaleService, unitOfWork);
        }
    }
}
