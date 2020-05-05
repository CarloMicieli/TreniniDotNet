using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public class CreateScaleUseCaseTests : CatalogUseCaseTests<CreateScaleUseCase, CreateScaleOutput, CreateScaleOutputPort>
    {
        [Fact]
        public async Task CreateScale_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            await useCase.Execute(NewCreateScaleInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateScale_ShouldNotCreateANewScale_WhenScaleAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewCreateScale);

            var name = "H0";
            var input = NewCreateScaleInput.With(
                name: name,
                ratio: 87M,
                scaleGauge: NewScaleGaugeInput.With(
                    millimeters: 16.5M,
                    trackGauge: TrackGauge.Standard.ToString()),
                description: "notes");

            await useCase.Execute(input);

            outputPort.AssertScaleAlreadyExists(Slug.Of(name));
        }

        [Fact]
        public async Task CreateScale_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewCreateScale);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateScale_Should_CreateANewScale()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            var input = NewCreateScaleInput.With(
                name: "H0",
                ratio: 87M,
                scaleGauge: NewScaleGaugeInput.With(
                    millimeters: 16.5M,
                    trackGauge: TrackGauge.Standard.ToString()),
                description: "notes");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.Slug.Should().NotBeNull();
            output.Slug.Should().Be(Slug.Of("H0"));

            dbContext.Scales.Any(it => it.Slug == Slug.Of("H0")).Should().BeTrue();
        }

        private CreateScaleUseCase NewCreateScale(ScaleService scaleService, CreateScaleOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateScaleUseCase(outputPort, scaleService, unitOfWork);
        }
    }
}
