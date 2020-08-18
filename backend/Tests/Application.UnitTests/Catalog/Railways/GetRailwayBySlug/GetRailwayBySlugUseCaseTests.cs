using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugUseCaseTests : RailwayUseCaseTests<GetRailwayBySlugUseCase, GetRailwayBySlugInput, GetRailwayBySlugOutput, GetRailwayBySlugOutputPort>
    {
        [Fact]
        public async Task GetRailwayBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputNotFound_WhenRailwayDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("not-found")));

            outputPort.ShouldHaveRailwayNotFoundMessage("The 'not-found' railway was not found");
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputRailway_WhenRailwayExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("db")));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;
            Assert.Equal(Slug.Of("db"), output.Railway?.Slug);
        }

        private GetRailwayBySlugUseCase CreateUseCase(
            GetRailwayBySlugOutputPort outputPort,
            RailwaysService railwaysService,
            IUnitOfWork unitOfWork) =>
            new GetRailwayBySlugUseCase(new GetRailwayBySlugInputValidator(), outputPort, railwaysService);
    }
}
