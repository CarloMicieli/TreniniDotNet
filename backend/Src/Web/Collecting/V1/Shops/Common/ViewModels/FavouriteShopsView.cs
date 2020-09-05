using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public sealed class FavouriteShopsView
    {
        public FavouriteShopsView(Owner owner, IEnumerable<ShopView> shops)
        {
            Owner = owner.Value;
            Shops = shops.ToList();
        }

        public string Owner { get; }
        public List<ShopView> Shops { get; }
    }
}
