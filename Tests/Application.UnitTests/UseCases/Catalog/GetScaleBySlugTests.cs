using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Application.InMemory;
using TreniniDotNet.Application.InMemory.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScaleBySlugTests : UseCaseTests<GetScaleBySlugInput, GetScaleBySlugInputValidator>
    {
        [Fact]
        public async Task GetScaleBySlug_ShouldValidateInput()
        {
            var scaleRepository = new ScaleRepository(new InMemoryContext());

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var outputPortMock = new Mock<IGetScaleBySlugOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => validationFailures = o);

            var useCase = NewGetScaleBySlugUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Empty));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<List<ValidationFailure>>()), Times.Once);
            Assert.True(validationFailures.Count > 0);
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var scaleRepository = new ScaleRepository(new InMemoryContext());

            string message = "";
            var outputPortMock = new Mock<IGetScaleBySlugOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewGetScaleBySlugUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", message);
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputNotFound_WhenScaleDoesNotExist()
        {
            var scaleRepository = new ScaleRepository(new InMemoryContext());

            string message = "";
            var outputPortMock = new Mock<IGetScaleBySlugOutputPort>();
            outputPortMock.Setup(h => h.ScaleNotFound(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewGetScaleBySlugUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("not-found")));

            outputPortMock.Verify(outputPort => outputPort.ScaleNotFound(It.IsAny<string>()), Times.Once);
            Assert.Equal("The 'not-found' scale was not found", message);
        }

        [Fact]
        public async Task GetScaleBySlug_ShouldOutputScale_WhenScaleExists()
        {
            var scaleRepository = new ScaleRepository(InMemoryContext.WithCatalogSeedData());

            GetScaleBySlugOutput output = null;
            var outputPortMock = new Mock<IGetScaleBySlugOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<GetScaleBySlugOutput>()))
                .Callback<GetScaleBySlugOutput>(rv => output = rv);

            var useCase = NewGetScaleBySlugUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(new GetScaleBySlugInput(Slug.Of("H0")));

            outputPortMock.Verify(outputPort => outputPort.Standard(It.IsAny<GetScaleBySlugOutput>()), Times.Once);
            Assert.NotNull(output);
            Assert.NotNull(output.Scale);
            Assert.Equal(Slug.Of("H0"), output.Scale.Slug);
        }

        private GetScaleBySlug NewGetScaleBySlugUseCase(ScaleRepository repo, IGetScaleBySlugOutputPort outputPort)
        {
            return new GetScaleBySlug(NewValidator(), outputPort, new ScaleService(repo));
        }
    }
}
