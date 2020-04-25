using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Railways;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;
using Xunit;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.UseCases.Catalog.Railways
{
    public class EditRailwayUseCaseTests : CatalogUseCaseTests<EditRailway, EditRailwayOutput, EditRailwayOutputPort>
    {
        [Fact]
        public async Task EditRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewEditRailway);

            await useCase.Execute(NewEditRailwayInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditRailway_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewEditRailway);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditRailway_ShouldOutputAnError_WhenRailwayIsNotFound()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewEditRailway);

            await useCase.Execute(NewEditRailwayInput.With(RailwaySlug: Slug.Of("RhB")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayNotFound(Slug.Of("RhB"));
        }

        [Fact]
        public async Task EditRailway_ShouldEditRailway()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeRailwaysUseCase(Start.WithSeedData, NewEditRailway);

            await useCase.Execute(NewEditRailwayInput.With(
                RailwaySlug: Slug.Of("RhB"),
                CompanyName: "Ferrovia Retica"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }


        private EditRailway NewEditRailway(RailwayService railwayService, EditRailwayOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditRailway(outputPort, railwayService, unitOfWork);
        }
    }
}
