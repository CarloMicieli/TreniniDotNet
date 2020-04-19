using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public sealed class WishlistItemsRepository : IWishlistItemsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IWishlistsFactory _wishlistsFactory;

        public WishlistItemsRepository(IDatabaseContext dbContext, IWishlistsFactory wishlistsFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
        }

        public Task<WishlistItemId> AddItemAsync(WishlistId id, IWishlistItem newItem)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteItemAsync(WishlistId id, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem)
        {
            throw new System.NotImplementedException();
        }

        public Task<IWishlistItem?> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef)
        {
            throw new System.NotImplementedException();
        }

        public Task<IWishlistItem?> GetItemByIdAsync(WishlistId id, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }


        #region [ Query / Commands ]



        #endregion
    }
}
