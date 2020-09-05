using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public class GetLatestCatalogItemsUseCaseTests :
        CatalogItemUseCaseTests<GetLatestCatalogItemsUseCase, GetLatestCatalogItemsInput, GetLatestCatalogItemsOutput, GetLatestCatalogItemsOutputPort>
    {
        [Fact]
        public async Task GetLatestCatalogItems_ShouldReturnLatestCatalogItems()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetLatestCatalogItemsInput(new Page(0, 5)));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;

            output.Results.Should().HaveCount(5);
        }

        private GetLatestCatalogItemsUseCase CreateUseCase(
            IGetLatestCatalogItemsOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new GetLatestCatalogItemsUseCase(new GetLatestCatalogItemsInputValidator(), outputPort, catalogItemsService);
    }
}
