using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScaleBySlugUseCaseTests : CatalogUseCaseTests<GetScaleBySlug, GetScaleBySlugOutput, GetScaleBySlugOutputPort>
    {
        [Fact]
        public async Task GetScaleBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewGetScaleBySlug);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewGetScaleBySlug);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputNotFound_WhenScaleDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewGetScaleBySlug);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("not-found")));

            outputPort.ShouldHaveScaleNotFoundMessage("The 'not-found' scale was not found");
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputScale_WhenScaleExists()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewGetScaleBySlug);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("H0")));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;
            Assert.NotNull(output);
            Assert.NotNull(output.Scale);
            Assert.Equal(Slug.Of("H0"), output.Scale.Slug);
        }

        private GetScaleBySlug NewGetScaleBySlug(ScaleService scaleService, GetScaleBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetScaleBySlug(outputPort, scaleService);
        }
    }
}
