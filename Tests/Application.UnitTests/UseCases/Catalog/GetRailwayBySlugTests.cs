using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Application.InMemory;
using TreniniDotNet.Application.InMemory.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetRailwayBySlugTests : UseCaseTests<GetRailwayBySlugInput, GetRailwayBySlugInputValidator>
    {
        [Fact]
        public async Task GetRailwayBySlug_ShouldValidateInput()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var outputPortMock = new Mock<IGetRailwayBySlugOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => validationFailures = o);

            var useCase = NewGetRailwayBySlugUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Empty));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<List<ValidationFailure>>()), Times.Once);
            Assert.True(validationFailures.Count > 0);
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputAnError_WhenInputIsNull()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            string message = "";
            var outputPortMock = new Mock<IGetRailwayBySlugOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewGetRailwayBySlugUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", message);
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputNotFound_WhenRailwayDoesNotExist()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            string message = "";
            var outputPortMock = new Mock<IGetRailwayBySlugOutputPort>();
            outputPortMock.Setup(h => h.RailwayNotFound(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewGetRailwayBySlugUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("not-found")));

            outputPortMock.Verify(outputPort => outputPort.RailwayNotFound(It.IsAny<string>()), Times.Once);
            Assert.Equal("The 'not-found' railway was not found", message);
        }

        [Fact]
        public async Task GetRailwayBySlug_ShouldOutputRailway_WhenRailwayExists()
        {
            var railwayRepository = new RailwayRepository(InMemoryContext.WithCatalogSeedData());

            GetRailwayBySlugOutput output = null;
            var outputPortMock = new Mock<IGetRailwayBySlugOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<GetRailwayBySlugOutput>()))
                .Callback<GetRailwayBySlugOutput>(rv => output = rv);

            var useCase = NewGetRailwayBySlugUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(new GetRailwayBySlugInput(Slug.Of("db")));

            outputPortMock.Verify(outputPort => outputPort.Standard(It.IsAny<GetRailwayBySlugOutput>()), Times.Once);
            Assert.NotNull(output);
            Assert.Equal(Slug.Of("db"), output.Railway?.Slug);
        }

        private GetRailwayBySlug NewGetRailwayBySlugUseCase(RailwayRepository repo, IGetRailwayBySlugOutputPort outputPort)
        {
            return new GetRailwayBySlug(NewValidator(), outputPort, new RailwayService(repo));
        }
    }
}
