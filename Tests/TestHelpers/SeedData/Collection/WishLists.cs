using System.Collections.Generic;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.TestHelpers.SeedData.Collection
{
    public sealed class WishLists
    {
        public IList<IWishList> All()
        {
            return new List<IWishList>();
        }
    }
}