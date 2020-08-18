using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shared
{
    public class CatalogItemRefsRepositoryTests : DapperRepositoryUnitTests<CatalogItemRefsRepository>
    {
        public CatalogItemRefsRepositoryTests()
            : base(unitOfWork => new CatalogItemRefsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task CatalogItemRefsRepository_GetCatalogItemAsync_ShouldFindCatalogItems()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            Database.ArrangeCatalogData(catalogItem);

            var itemRef1 = await Repository.GetCatalogItemAsync(catalogItem.Slug);
            var itemRef2 = await Repository.GetCatalogItemAsync(Slug.Of("not-found"));

            itemRef1.Should().NotBeNull();
            itemRef1?.Count.Should().Be(1);
            itemRef1?.BrandName.Should().Be("ACME");
            itemRef1?.ItemNumber.Should().Be(catalogItem.ItemNumber);
            itemRef1?.Category.Should().Be(CatalogItemCategory.Locomotives);

            itemRef2.Should().BeNull();
        }
    }
}