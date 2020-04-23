using FluentAssertions;
using NodaTime;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shared
{
    public class CatalogRefsRepositoryTests : RepositoryUnitTests<ICatalogRefsRepository>
    {
        public CatalogRefsRepositoryTests(SqliteDatabaseFixture fixture) 
            : base(fixture, CreateRepository)
        {
        }

        private static ICatalogRefsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new CatalogRefsRepository(databaseContext);


        [Fact]
        public Task CatalogRefsRepository_ShouldFindCatalogItemInformation()
        {
            false.Should().BeTrue("Implement me");
            return Task.CompletedTask;
        }
    }
}
