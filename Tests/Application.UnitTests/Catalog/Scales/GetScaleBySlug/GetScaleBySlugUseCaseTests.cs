using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public class GetScaleBySlugUseCaseTests : ScaleUseCaseTests<GetScaleBySlugUseCase, GetScaleBySlugInput, GetScaleBySlugOutput, GetScaleBySlugOutputPort>
    {
        [Fact]
        public async Task GetScaleBySlug_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputNotFound_WhenScaleDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("not-found")));

            outputPort.ShouldHaveScaleNotFoundMessage("The 'not-found' scale was not found");
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputScale_WhenScaleExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("H0")));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;

            output.Should().NotBeNull();
            output.Scale.Should().NotBeNull();
            output.Scale.Slug.Should().Be(Slug.Of("H0"));
        }

        private GetScaleBySlugUseCase CreateUseCase(
            GetScaleBySlugOutputPort outputPort,
            ScalesService scalesService,
            IUnitOfWork unitOfWork) =>
            new GetScaleBySlugUseCase(new GetScaleBySlugInputValidator(), outputPort, scalesService);
    }
}
