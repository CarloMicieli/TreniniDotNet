using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IWishlistsFactory _wishlistsFactory;

        public WishlistsRepository(IDatabaseContext dbContext, IWishlistsFactory wishlistsFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
        }

        public Task<WishlistId> AddAsync(IWishlist wishList)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(WishlistId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistAsync(WishlistId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IWishlist?> GetByIdAsync(WishlistId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IWishlistInfo>> GetByOwnerAsync(Owner owner, Visibility visibility)
        {
            throw new System.NotImplementedException();
        }

        #region [ Query / Commands ]



        #endregion
    }
}
