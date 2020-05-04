using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsUseCaseTests : CatalogUseCaseTests<GetLatestCatalogItemsUseCase, GetLatestCatalogItemsOutput, GetLatestCatalogItemsOutputPort>
    {
        [Fact]
        public async Task GetLatestCatalogItems_ShouldReturnLatestCatalogItems()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewGetLatestCatalogItems);

            await useCase.Execute(new GetLatestCatalogItemsInput(new Page(0, 5)));

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;

            output.Results.Should().HaveCount(5);
        }

        private GetLatestCatalogItemsUseCase NewGetLatestCatalogItems(CatalogItemService catalogItemService, IGetLatestCatalogItemsOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetLatestCatalogItemsUseCase(catalogItemService, outputPort);
        }
    }
}
