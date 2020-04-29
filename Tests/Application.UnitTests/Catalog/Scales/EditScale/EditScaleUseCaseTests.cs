using System.Threading.Tasks;
using TreniniDotNet.Application.InMemory.Catalog.Scales.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public class EditScaleUseCaseTests : CatalogUseCaseTests<EditScaleUseCase, EditScaleOutput, EditScaleOutputPort>
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


        private EditScaleUseCase NewEditScale(ScaleService scaleService, EditScaleOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditScaleUseCase(outputPort, scaleService, unitOfWork);
        }
    }
}
