using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;
using TreniniDotNet.TestHelpers.SeedData.Collecting;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public sealed class ShopsRepository : IShopsRepository
    {
        private InMemoryContext Context { get; }

        public ShopsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<Shop>> GetShopsAsync(Page page)
        {
            var result = Context.Shops.Skip(page.Start).Take(page.Limit);
            return Task.FromResult(new PaginatedResult<Shop>(page, result));
        }

        public Task<ShopId> AddAsync(Shop catalogItem)
        {
            Context.Shops.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Shop brand)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(ShopId id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Shop shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(ShopId shopId)
        {
            var result = Context.Shops.Any(it => it.Id == shopId);
            return Task.FromResult(result);
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var result = Context.Shops.Any(it => it.Slug == slug);
            return Task.FromResult(result);
        }

        public Task<Shop> GetBySlugAsync(Slug slug)
        {
            var result = Context.Shops.FirstOrDefault(it => it.Slug == slug);
            return Task.FromResult(result);
        }

        public Task AddShopToFavouritesAsync(Owner owner, ShopId shopId)
        {
            Context.ShopFavourites.Add(new ShopFavourite
            {
                Owner = owner,
                ShopId = shopId,
                Shop = Context.Shops.FirstOrDefault(it => it.Id == shopId)
            });
            return Task.CompletedTask;
        }

        public Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId)
        {
            Context.ShopFavourites
                .RemoveAll(it => it.Owner == owner && it.ShopId == shopId);
            return Task.CompletedTask;
        }

        public Task<List<Shop>> GetFavouriteShopsAsync(Owner owner)
        {
            var results = Context.ShopFavourites
                .Where(it => it.Owner == owner)
                .Select(it => it.Shop)
                .ToList();
            return Task.FromResult(results);
        }
    }
}
