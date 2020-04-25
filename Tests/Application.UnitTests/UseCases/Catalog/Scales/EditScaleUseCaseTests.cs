using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.UseCases.Catalog.Scales
{
    public class EditScaleUseCaseTests : CatalogUseCaseTests<EditScale, EditScaleOutput, EditScaleOutputPort>
    {
        [Fact]
        public async Task EditScale_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewEditScale);

            await useCase.Execute(NewEditScaleInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditScale_ShouldOutputError_WhenScaleIsNotFound()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewEditScale);

            await useCase.Execute(NewEditScaleInput.With(ScaleSlug: Slug.Of("H0m")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("H0m"));
        }

        [Fact]
        public async Task EditScale_ShouldEditScale()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeScalesUseCase(Start.WithSeedData, NewEditScale);

            var input = NewEditScaleInput.With(
                ScaleSlug: Slug.Of("H0m"),
                Ratio: 87M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }


        private EditScale NewEditScale(ScaleService scaleService, EditScaleOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditScale(outputPort, scaleService, unitOfWork);
        }
    }
}
