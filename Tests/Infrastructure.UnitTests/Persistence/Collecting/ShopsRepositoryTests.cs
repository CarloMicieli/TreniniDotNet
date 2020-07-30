using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Persistence.Collecting;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Collecting
{
    public class ShopsRepositoryTests : EfRepositoryUnitTests<ShopsRepository>
    {
        public ShopsRepositoryTests()
            : base(context => new ShopsRepository(context))
        {
        }

        [Fact]
        public async Task ShopsRepository_AddAsync_ShouldInsertNewShop()
        {
            var testShop = TestShop();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);

                var id = await repo.AddAsync(testShop);
                await context.SaveChangesAsync();

                id.Should().Be(testShop.Id);
            }

            await using (var context = NewDbContext())
            {
                var shop = await context.Shops
                    .FirstOrDefaultAsync(it => it.Id == testShop.Id);

                shop.Should().NotBeNull();
                shop.Name.Should().Be(testShop.Name);
            }
        }

        [Fact]
        public async Task ShopsRepository_ExistsAsync_ShouldCheckShopExistenceById()
        {
            var shopId = CollectingSeedData.Shops.NewTecnomodelTreni().Id;

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exist1 = await repo.ExistsAsync(shopId);
            var exist2 = await repo.ExistsAsync(ShopId.NewId());

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task ShopsRepository_ExistsAsync_ShouldCheckShopExistenceBySlug()
        {
            var shopSlug = CollectingSeedData.Shops.NewTecnomodelTreni().Slug;

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exist1 = await repo.ExistsAsync(shopSlug);
            var exist2 = await repo.ExistsAsync(Slug.Of("not found"));

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task ShopsRepository_GetBySlugAsync_ShouldCheckShopExistenceBySlug()
        {
            var shopSlug = CollectingSeedData.Shops.NewTecnomodelTreni().Slug;

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var shop1 = await repo.GetBySlugAsync(shopSlug);
            var shop2 = await repo.GetBySlugAsync(Slug.Of("not found"));

            shop1.Should().NotBeNull();
            shop2.Should().BeNull();
        }

        [Fact]
        public async Task ShopsRepository_AddShopToFavouritesAsync_ShouldAddShopsToFavourites()
        {
            var owner = new Owner("George");
            var shop = CollectingSeedData.Shops.NewTecnomodelTreni();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                await repo.AddShopToFavouritesAsync(owner, shop.Id);
                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var exists = await context.ShopFavourites
                    .AnyAsync(it => it.Owner == owner && it.Shop.Id == shop.Id);
                exists.Should().BeTrue();
            }
        }

        [Fact(Skip = "DbUpdateConcurrencyException")]
        public async Task ShopsRepository_RemoveShopFromFavouritesAsync_ShouldRemoveShopFromFavourites()
        {
            var owner = new Owner("George");
            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                await repo.RemoveFromFavouritesAsync(owner, shop.Id);
                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var exists = await context.ShopFavourites
                    .AnyAsync(it => it.Owner == owner && it.Shop.Id == shop.Id);
                exists.Should().BeFalse();
            }
        }

        private static Shop TestShop()
        {
            return CollectingSeedData.Shops.New()
                .Id(new Guid("dcba5221-cb2b-4961-976d-c6df34c2c6db"))
                .Name("Test Shop")
                .WebsiteUrl(new Uri("https://www.testshop.com"))
                .MailAddress(new MailAddress("mail@mail.com"))
                .PhoneNumber(PhoneNumber.Of("+39 555 123456"))
                .Build();
        }
    }
}
