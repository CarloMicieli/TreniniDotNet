using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public class GetCatalogItemBySlugUseCaseTests :
        CatalogItemUseCaseTests<GetCatalogItemBySlugUseCase, GetCatalogItemBySlugInput, GetCatalogItemBySlugOutput, GetCatalogItemBySlugOutputPort>
    {
        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturnNotFound_WhenTheCatalogItemDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetCatalogItemBySlugInput(Slug.Of("not-found")));

            outputPort.CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument("The catalog item 'not-found' was not found");
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturnTheCatalogItem_WhenItExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetCatalogItemBySlugInput(Slug.Of("acme-60458")));

            outputPort.ShouldHaveStandardOutput();
        }

        private GetCatalogItemBySlugUseCase CreateUseCase(
            IGetCatalogItemBySlugOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new GetCatalogItemBySlugUseCase(new GetCatalogItemBySlugInputValidator(), outputPort, catalogItemsService);

    }
}
