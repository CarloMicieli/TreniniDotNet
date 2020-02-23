using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.InMemory;
using TreniniDotNet.Application.InMemory.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateRailwayTests : UseCaseTests<CreateRailwayInput, CreateRailwayInputValidator>
    {
        [Fact]
        public async Task CreateRailway_ShouldValidateInput()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var outputPortMock = new Mock<ICreateRailwayOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => validationFailures = o);

            var useCase = NewCreateRailwayUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(new CreateRailwayInput(null, null, null, null, null, null));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<List<ValidationFailure>>()), Times.Once);
            Assert.True(validationFailures.Count > 0);
        }

        [Fact]
        public async Task CreateRailway_ShouldOutputAnError_WhenInputIsNull()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            string message = "";
            var outputPortMock = new Mock<ICreateRailwayOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateRailwayUseCase(railwayRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", message);
        }

        [Fact]
        public async Task CreateRailway_ShouldNotCreateANewRailway_WhenRailwayAlreadyExists()
        {
            var railwayRepository = new RailwayRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<ICreateRailwayOutputPort>();
            outputPortMock.Setup(h => h.RailwayAlreadyExists(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateRailwayUseCase(railwayRepository, outputPortMock.Object);

            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "active", DateTime.Now.AddDays(-1), null); 

            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.RailwayAlreadyExists(It.IsAny<string>()), Times.Once);
            Assert.Equal("Railway 'DB' already exists", message);
        }

        [Fact]
        public async Task CreateRailway_Should_CreateANewRailway()
        {
            var railwayRepository = new RailwayRepository(new InMemoryContext());

            CreateRailwayOutput output = null;
            var outputPortMock = new Mock<ICreateRailwayOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<CreateRailwayOutput>()))
                .Callback<CreateRailwayOutput>(o => output = o);

            var useCase = NewCreateRailwayUseCase(railwayRepository, outputPortMock.Object);

            var input = new CreateRailwayInput("DB", "Die Bahn", "DE", "active", DateTime.Now.AddDays(-1), null);

            await useCase.Execute(input);

            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("db"), output!.Slug);

            Assert.NotNull(railwayRepository.GetBy(output!.Slug));
        }

        private CreateRailway NewCreateRailwayUseCase(
            IRailwaysRepository repo,
            ICreateRailwayOutputPort outputPort)
        {
            return NewCreateRailwayUseCase(repo, outputPort, UnitOfWork().Object);
        }

        private CreateRailway NewCreateRailwayUseCase(
            IRailwaysRepository repo, 
            ICreateRailwayOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            var railwayFactory = new RailwaysFactory();
            var railwayService = new RailwayService(repo);

            var useCase = new CreateRailway(NewValidator(), outputPort, railwayService, unitOfWork);
            return useCase;
        }
    }
}
