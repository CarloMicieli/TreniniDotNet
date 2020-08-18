using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public class GetCollectionStatisticsUseCaseTests : CollectionUseCaseTests<GetCollectionStatisticsUseCase, GetCollectionStatisticsInput, GetCollectionStatisticsOutput, GetCollectionStatisticsOutputPort>
    {
        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputValidationErrors_WhenInputIsInvalid()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewGetCollectionStatisticsInput.With("    "));

            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputAnError_WhenTheUserHasNoCollection()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewGetCollectionStatisticsInput.With("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(new Owner("George"));
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldOutputCollectionStatistics()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);
            var input = NewGetCollectionStatisticsInput.With("George");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var stats = outputPort.UseCaseOutput;
            stats.Statistics.Should().NotBeNull();
            stats.Statistics.Owner.Should().Be(new Owner("George"));
            stats.Statistics.TotalValue.Should().Be(Price.Euro(450M));
            stats.Statistics.CategoriesByYear.Should().HaveCount(1);
        }

        private GetCollectionStatisticsUseCase CreateUseCase(
            IGetCollectionStatisticsOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new GetCollectionStatisticsUseCase(new GetCollectionStatisticsInputValidator(), outputPort, collectionsService);
    }
}
