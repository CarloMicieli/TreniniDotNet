using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
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
