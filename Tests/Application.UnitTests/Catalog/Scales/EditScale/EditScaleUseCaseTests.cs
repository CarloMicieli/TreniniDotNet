using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

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

            await useCase.Execute(NewEditScaleInput.With(scaleSlug: Slug.Of("H0m")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("H0m"));
        }

        [Fact]
        public async Task EditScale_ShouldEditScale()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeScalesUseCase(Start.WithSeedData, NewEditScale);

            var input = NewEditScaleInput.With(
                scaleSlug: Slug.Of("H0m"),
                ratio: 97M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var scale = dbContext.Scales.First(it => it.Slug == Slug.Of("H0m"));
            scale.Should().NotBeNull();
            scale.Ratio.ToDecimal().Should().Be(97M);
        }

        private EditScaleUseCase NewEditScale(ScaleService scaleService, EditScaleOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditScaleUseCase(outputPort, scaleService, unitOfWork);
        }
    }
}
