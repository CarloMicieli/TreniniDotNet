using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public class EditScaleUseCaseTests : ScaleUseCaseTests<EditScaleUseCase, EditScaleInput, EditScaleOutput, EditScaleOutputPort>
    {
        [Fact]
        public async Task EditScale_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditScaleInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditScale_ShouldOutputError_WhenScaleIsNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditScaleInput.With(scaleSlug: Slug.Of("H0m")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("H0m"));
        }

        [Fact]
        public async Task EditScale_ShouldEditScale()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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

        private EditScaleUseCase CreateUseCase(
            EditScaleOutputPort outputPort,
            ScalesService scalesService,
            IUnitOfWork unitOfWork) =>
            new EditScaleUseCase(new EditScaleInputValidator(), outputPort, scalesService, unitOfWork);
    }
}
