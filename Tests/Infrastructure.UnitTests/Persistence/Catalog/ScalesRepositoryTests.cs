using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Catalog
{
    public class ScalesRepositoryTests : EfRepositoryUnitTests<ScalesRepository>
    {
        public ScalesRepositoryTests()
            : base(context => new ScalesRepository(context))
        {
        }

        [Fact]
        public async Task ScalesRepository_AddAsync_ShouldCreateANewScale()
        {
            var testScale = CatalogSeedData.Scales.ScaleH0();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);

                var id = await repo.AddAsync(testScale);
                await context.SaveChangesAsync();

                id.Should().Be(testScale.Id);
            }

            await using (var context = NewDbContext())
            {
                var found = await context.Scales
                    .AnyAsync(it => it.Id == testScale.Id);

                found.Should().BeTrue();
            }
        }

        [Fact]
        public async Task ScalesRepository_GetBySlugAsync_ShouldFindOneScaleBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var scale1 = await repo.GetBySlugAsync(Slug.Of("H0"));
            var scale2 = await repo.GetBySlugAsync(Slug.Of("not found"));

            scale1.Should().NotBeNull();
            scale1?.Slug.Should().Be(Slug.Of("H0"));

            scale2.Should().BeNull();
        }

        [Fact]
        public async Task ScalesRepository_ExistsAsync_ShouldFindOneScaleBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var found1 = await repo.ExistsAsync(Slug.Of("H0"));
            var found2 = await repo.ExistsAsync(Slug.Of("not found"));

            found1.Should().BeTrue();
            found2.Should().BeFalse();
        }
    }
}