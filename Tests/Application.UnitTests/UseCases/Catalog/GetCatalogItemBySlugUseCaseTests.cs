using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetCatalogItemBySlugUseCaseTests : CatalogUseCaseTests<GetCatalogItemBySlug, GetCatalogItemBySlugOutput, GetCatalogItemBySlugOutputPort>
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

        private GetCatalogItemBySlug NewGetCatalogItemBySlug(CatalogItemService catalogItemService, IGetCatalogItemBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetCatalogItemBySlug(catalogItemService, outputPort);
        }

    }
}