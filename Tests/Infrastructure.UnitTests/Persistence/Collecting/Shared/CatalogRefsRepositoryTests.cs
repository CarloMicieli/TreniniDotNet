using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Persistence.Collecting.Common;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Shared
{
    public class CatalogRefsRepositoryTests : CollectionRepositoryUnitTests<ICatalogRefsRepository>
    {
        public CatalogRefsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static ICatalogRefsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new CatalogRefsRepository(databaseContext);

        [Fact]
        public async Task CatalogRefsRepository_ShouldFindCatalogItemInformation()
        {
            ArrangeCatalogData();

            var itemRef1 = await Repository.GetBySlugAsync(Slug.Of("acme-123456"));
            var itemRef2 = await Repository.GetBySlugAsync(Slug.Of("not found"));

            itemRef1.Should().NotBeNull();
            itemRef1.CatalogItemId.Should().Be(Acme_123456.CatalogItemId);

            itemRef2.Should().BeNull();
        }
    }
}
