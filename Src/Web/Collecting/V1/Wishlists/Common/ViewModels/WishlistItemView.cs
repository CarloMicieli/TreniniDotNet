using System;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public sealed class WishlistItemView
    {
        private readonly WishlistItem _item;

        public WishlistItemView(WishlistItem item)
        {
            _item = item;

            CatalogItem = new CatalogItemView(item.CatalogItem);

            if (item.Price is null)
            {
                Price = null;
            }
            else
            {
                Price = new PriceView(item.Price);
            }
        }

        public Guid ItemId => _item.Id;

        public string Priority => _item.Priority.ToString();

        public DateTime AddedDate => _item.AddedDate.ToDateTimeUnspecified();

        public PriceView? Price { get; }

        public CatalogItemView CatalogItem { get; }

        public string? Notes => _item.Notes;
    }
}
