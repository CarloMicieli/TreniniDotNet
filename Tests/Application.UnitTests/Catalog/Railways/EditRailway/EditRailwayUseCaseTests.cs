using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public class EditRailwayUseCaseTests : CatalogUseCaseTests<EditRailwayUseCase, EditRailwayOutput, EditRailwayOutputPort>
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

            await useCase.Execute(NewEditRailwayInput.With(railwaySlug: Slug.Of("RhB")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayNotFound(Slug.Of("RhB"));
        }

        [Fact]
        public async Task EditRailway_ShouldEditRailway()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeRailwaysUseCase(Start.WithSeedData, NewEditRailway);

            await useCase.Execute(NewEditRailwayInput.With(
                railwaySlug: Slug.Of("RhB"),
                companyName: "Ferrovia Retica"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var railway = dbContext.Railways.FirstOrDefault(it => it.Slug == Slug.Of("RhB"));
            railway.Should().NotBeNull();
            railway?.CompanyName.Should().Be("Ferrovia Retica");
            railway?.Version.Should().Be(2);
        }


        private EditRailwayUseCase NewEditRailway(RailwayService railwayService, EditRailwayOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditRailwayUseCase(outputPort, railwayService, unitOfWork);
        }
    }
}
