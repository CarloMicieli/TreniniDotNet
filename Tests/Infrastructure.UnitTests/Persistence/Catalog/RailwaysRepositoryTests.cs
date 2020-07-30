using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Catalog
{
    public class RailwaysRepositoryTests : EfRepositoryUnitTests<RailwaysRepository>
    {
        public RailwaysRepositoryTests()
            : base(context => new RailwaysRepository(context))
        {
        }

        [Fact]
        public async Task RailwaysRepository_AddAsync_ShouldCreateANewRailway()
        {
            var railway = TestRailway();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);

                var id = await repo.AddAsync(railway);
                await context.SaveChangesAsync();

                id.Should().Be(railway.Id);
            }

            await using (var context = NewDbContext())
            {
                var found = await context.Railways
                    .FirstAsync(it => it.Id == railway.Id);

                found.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task RailwaysRepository_UpdateAsync_ShouldUpdateRailways()
        {
            Railway railway = null;
            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                railway = context.Railways.First();

                var modified = railway.With(name: railway.Name + "(2)");
                await repo.UpdateAsync(modified);

                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var found = await context.Railways
                    .FirstAsync(it => it.Id == railway.Id);

                found.Name.Should().Be(railway.Name + "(2)");
            }
        }

        [Fact]
        public async Task RailwaysRepository_GetBySlugAsync_ShouldFindOneRailwayBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var railway1 = await repo.GetBySlugAsync(Slug.Of("FS"));
            var railway2 = await repo.GetBySlugAsync(Slug.Of("not found"));

            railway1.Should().NotBeNull();
            railway1?.Slug.Should().Be(Slug.Of("FS"));

            railway2.Should().BeNull();
        }

        [Fact]
        public async Task RailwaysRepository_ExistsAsync_ShouldFindOneRailwayBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var found1 = await repo.ExistsAsync(Slug.Of("FS"));
            var found2 = await repo.ExistsAsync(Slug.Of("not found"));

            found1.Should().BeTrue();
            found2.Should().BeFalse();
        }

        private static Railway TestRailway()
        {
            return CatalogSeedData.Railways.New()
                .Id(new Guid("b9e62d18-06e3-4404-9183-6c3a3b89c683"))
                .Name("Test railway")
                .CompanyName("Ferrovie dello Stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .WebsiteUrl(new Uri("https://www.trenitalia.com"))
                .Build();
        }
    }
}