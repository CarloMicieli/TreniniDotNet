using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class WishlistsView
    {
        public WishlistsView(Owner owner, VisibilityCriteria visibility, IEnumerable<IWishlistInfo> lists)
        {
            Owner = owner.Value;
            Visibility = visibility.ToString();
            Lists = lists.Select(it => new WishlistInfoView(it)).ToList();
        }

        public string Owner { get; }
        public string Visibility { get; }
        public List<WishlistInfoView> Lists { get; }
    }
}
