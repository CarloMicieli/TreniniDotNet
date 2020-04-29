using System.Threading.Tasks;
using FluentAssertions;
using NodaMoney;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public class GetCollectingStatisticsUseCaseTests : CollectingUseCaseTests<GetCollectionStatisticsUseCase, GetCollectionStatisticsOutput, GetCollectionStatisticsOutputPort>
    {
        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputValidationErrors_WhenInputIsInvalid()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(InputWithOwner("    "));

            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenTheUserHasNoCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(InputWithOwner("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(new Owner("George"));
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputCollectionStatistics()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewGetCollectionStatistics);

            await useCase.Execute(InputWithOwner("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var stats = outputPort.UseCaseOutput;
            stats.Statistics.Should().NotBeNull();
            stats.Statistics.Owner.Should().Be(new Owner("George"));
            stats.Statistics.TotalValue.Should().Be(Money.Euro(450M));
            stats.Statistics.CategoriesByYear.Should().HaveCount(1);
        }

        private GetCollectionStatisticsInput InputWithOwner(string owner) =>
            new GetCollectionStatisticsInput(owner);

        private GetCollectionStatisticsUseCase NewGetCollectionStatistics(
            CollectionsService collectionService,
            GetCollectionStatisticsOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetCollectionStatisticsUseCase(outputPort, collectionService);
        }
    }
}
