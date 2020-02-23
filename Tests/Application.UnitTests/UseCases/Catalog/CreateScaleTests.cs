using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Application.InMemory.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateScaleTests
    {
        [Fact]
        public async Task CreateScale_ShouldValidateInput()
        {
            var scaleRepository = new ScaleRepository(new InMemoryContext());

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var outputPortMock = new Mock<ICreateScaleOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => validationFailures = o);

            var useCase = NewCreateScaleUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(new CreateScaleInput(null, null, null, null, null));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<List<ValidationFailure>>()), Times.Once);
            Assert.True(validationFailures.Count > 0);
        }

        [Fact]
        public async Task CreateScale_Should_CreateANewScale()
        {
            var scaleRepository = new ScaleRepository(new InMemoryContext());

            CreateScaleOutput output = null;
            var outputPortMock = new Mock<ICreateScaleOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<CreateScaleOutput>()))
                .Callback<CreateScaleOutput>(o => output = o);

            var useCase = NewCreateScaleUseCase(scaleRepository, outputPortMock.Object);

            var input = new CreateScaleInput(
                "H0",
                87M,
                16.5M,
                TrackGauge.Standard.ToString(),
                "notes");

            await useCase.Execute(input);

            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("h0"), output!.Slug);

            Assert.NotNull(scaleRepository.GetBy(output!.Slug));
        }

        [Fact]
        public async Task CreateScale_ShouldNotCreateANewScale_WhenScaleAlreadyExists()
        {
            var scaleRepository = new ScaleRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<ICreateScaleOutputPort>();
            outputPortMock.Setup(h => h.ScaleAlreadyExists(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateScaleUseCase(scaleRepository, outputPortMock.Object);

            var input = new CreateScaleInput(
                "H0",
                87M,
                16.5M,
                TrackGauge.Standard.ToString(),
                "notes");

            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.ScaleAlreadyExists(It.IsAny<string>()), Times.Once);
            Assert.Equal("Scale 'H0' already exists", message);
        }

        [Fact]
        public async Task CreateScale_ShouldOutputAnError_WhenInputIsNull()
        {
            var scaleRepository = new ScaleRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<ICreateScaleOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateScaleUseCase(scaleRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", message);
        }

        private CreateScale NewCreateScaleUseCase(IScalesRepository repo, ICreateScaleOutputPort outputPort)
        {
            var scaleFactory = new ScalesFactory();
            var scaleService = new ScaleService(repo);

            var useCase = new CreateScale(outputPort, scaleService, new UnitOfWork());
            return useCase;
        }
    }
}
