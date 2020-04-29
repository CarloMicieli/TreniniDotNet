using System.Threading.Tasks;
using TreniniDotNet.Application.InMemory.Catalog.Railways.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public class GetRailwayBySlugUseCaseTests : CatalogUseCaseTests<GetRailwayBySlugUseCase, GetRailwayBySlugOutput, GetRailwayBySlugOutputPort>
    {
        [Fact]
        public async Task GetRailwayBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewGetRailwayBySlug);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewGetRailwayBySlug);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputNotFound_WhenRailwayDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewGetRailwayBySlug);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("not-found")));

            outputPort.ShouldHaveRailwayNotFoundMessage("The 'not-found' railway was not found");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputRailway_WhenRailwayExists()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.WithSeedData, NewGetRailwayBySlug);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("db")));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;
            Assert.Equal(Slug.Of("db"), output.Railway?.Slug);
        }

        private GetRailwayBySlugUseCase NewGetRailwayBySlug(RailwayService railwayService, GetRailwayBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetRailwayBySlugUseCase(outputPort, railwayService);
        }
    }
}
