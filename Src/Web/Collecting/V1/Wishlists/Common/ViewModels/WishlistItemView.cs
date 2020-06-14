using System;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public sealed class WishlistItemView
    {
        private readonly IWishlistItem _item;

        public WishlistItemView(IWishlistItem item)
        {
            _item = item;

            CatalogItem = new CatalogItemView(item.CatalogItem);

            if (item.Price is null)
            {
                Price = null;
            }
            else
            {
                Price = new MoneyView(item.Price.Value);
            }

            if (item.Details is null)
            {
                Details = null;
            }
            else
            {
                Details = new CatalogItemDetailsView(item.Details);
            }
        }

        public Guid ItemId => _item.Id.ToGuid();

        public string Priority => _item.Priority.ToString();

        public DateTime AddedDate => _item.AddedDate.ToDateTimeUnspecified();

        public MoneyView? Price { get; }

        public CatalogItemView CatalogItem { get; }

        public CatalogItemDetailsView? Details { get; }

        public string? Notes => _item.Notes;
    }
}
