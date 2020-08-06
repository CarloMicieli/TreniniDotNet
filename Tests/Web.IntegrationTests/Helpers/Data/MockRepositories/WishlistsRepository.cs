using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.IntegrationTests.Helpers.Data.MockRepositories
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private InMemoryContext Context { get; }

        public WishlistsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<Wishlist>> GetAllAsync(Page page)
        {
            throw new System.NotImplementedException();
        }

        public Task<WishlistId> AddAsync(Wishlist catalogItem)
        {
            Context.WishLists.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Wishlist brand)
        {
            Context.WishLists.RemoveAll(it => it.Id == brand.Id);
            Context.WishLists.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(WishlistId id)
        {
            Context.WishLists.RemoveAll(it => it.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Wishlist wishlist)
        {
            Context.WishLists.RemoveAll(it => it.Id == wishlist.Id);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(WishlistId id)
        {
            var result = Context.WishLists.Any(it => it.Id == id);
            return Task.FromResult(result);
        }

        public Task<Wishlist> GetByIdAsync(WishlistId id)
        {
            var result = Context.WishLists.FirstOrDefault(it => it.Id == id);
            return Task.FromResult(result);
        }

        public Task<List<Wishlist>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility)
        {
            var results = Context.WishLists.Where(it => it.Owner == owner);

            if (visibility == VisibilityCriteria.Private)
            {
                results = results.Where(it => it.Visibility == Visibility.Private);
            }
            else if (visibility == VisibilityCriteria.Public)
            {
                results = results.Where(it => it.Visibility == Visibility.Public);
            }

            return Task.FromResult(results.ToList());
        }

        public Task<bool> ExistsAsync(Owner owner, string listName)
        {
            var result = Context.WishLists.Any(it =>
                it.Owner == owner && string.Equals(it.ListName, listName, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(result);
        }

        public Task<int> CountWishlistsAsync(Owner owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
