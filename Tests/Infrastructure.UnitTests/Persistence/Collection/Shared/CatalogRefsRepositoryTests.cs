using FluentAssertions;
using NodaTime;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Infrastructure.Dapper;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shared
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
