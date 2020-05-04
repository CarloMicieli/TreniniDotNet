using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public class GetCatalogItemBySlugUseCaseTests : CatalogUseCaseTests<GetCatalogItemBySlugUseCase, GetCatalogItemBySlugOutput, GetCatalogItemBySlugOutputPort>
    {
        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturnNotFound_WhenTheCatalogItemDoesNotExist()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewGetCatalogItemBySlug);

            await useCase.Execute(new GetCatalogItemBySlugInput(Slug.Of("not-found")));

            outputPort.CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument("The catalog item 'not-found' was not found");
        }

        [Fact]
        public async Task GetCatalogItemBySlug_ShouldReturnTheCatalogItem_WhenItExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewGetCatalogItemBySlug);

            await useCase.Execute(new GetCatalogItemBySlugInput(Slug.Of("acme-60458")));

            outputPort.ShouldHaveStandardOutput();
        }

        private GetCatalogItemBySlugUseCase NewGetCatalogItemBySlug(CatalogItemService catalogItemService, IGetCatalogItemBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetCatalogItemBySlugUseCase(catalogItemService, outputPort);
        }

    }
}
