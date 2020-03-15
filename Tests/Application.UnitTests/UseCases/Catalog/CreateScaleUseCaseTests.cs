using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateScaleUseCaseTests : UseCaseTestHelper<CreateScale, CreateScaleOutput, CreateScaleOutputPort>
    {
        [Fact]
        public async Task CreateScale_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            await useCase.Execute(new CreateScaleInput(null, null, null, null, null));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateScale_Should_CreateANewScale()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewCreateScale);

            var input = new CreateScaleInput(
                "H0",
                87M,
                16.5M,
                TrackGauge.Standard.ToString(),
                "notes");

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("h0"), output!.Slug);
        }

        [Fact]
        public async Task CreateScale_ShouldNotCreateANewScale_WhenScaleAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewCreateScale);

            var name = "H0";
            var input = new CreateScaleInput(
                name,
                87M,
                16.5M,
                TrackGauge.Standard.ToString(),
                "notes");

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
