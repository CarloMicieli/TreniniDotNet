using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public sealed class EditRailwayUseCaseTests : RailwayUseCaseTests<EditRailwayUseCase, EditRailwayInput, EditRailwayOutput, EditRailwayOutputPort>
    {
        [Fact]
        public async Task EditRailway_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditRailwayInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditRailway_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditRailway_ShouldOutputAnError_WhenRailwayIsNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditRailwayInput.With(railwaySlug: Slug.Of("RhB")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayNotFound(Slug.Of("RhB"));
        }

        [Fact]
        public async Task EditRailway_ShouldEditRailway()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(NewEditRailwayInput.With(
                railwaySlug: Slug.Of("RhB"),
                companyName: "Ferrovia Retica"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var railway = dbContext.Railways.FirstOrDefault(it => it.Slug == Slug.Of("RhB"));
            railway.Should().NotBeNull();
            railway?.CompanyName.Should().Be("Ferrovia Retica");
        }

        private EditRailwayUseCase CreateUseCase(
            EditRailwayOutputPort outputPort,
            RailwaysService railwaysService,
            IUnitOfWork unitOfWork) =>
            new EditRailwayUseCase(new EditRailwayInputValidator(), outputPort, railwaysService, unitOfWork);
    }
}
