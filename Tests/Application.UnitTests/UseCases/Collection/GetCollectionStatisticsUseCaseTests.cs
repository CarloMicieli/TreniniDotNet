using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using NodaMoney;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class GetCollectionStatisticsUseCaseTests : CollectionUseCaseTests<GetCollectionStatistics, GetCollectionStatisticsOutput, GetCollectionStatisticsOutputPort>
    {
        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputValidationErrors_WhenInputIsInvalid()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(InputWithOwner("    "));

            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenTheUserHasNoCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionStatistics);

            await useCase.Execute(InputWithOwner("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(new Owner("George"));
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputCollectionStatistics()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewGetCollectionStatistics);

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

        private GetCollectionStatistics NewGetCollectionStatistics(
            CollectionsService collectionService,
            GetCollectionStatisticsOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetCollectionStatistics(outputPort, collectionService);
        }
    }
}
