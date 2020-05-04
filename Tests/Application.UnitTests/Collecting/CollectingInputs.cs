using System;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Application.Collecting.Shops;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;

namespace TreniniDotNet.Application.Collecting
{
    public static class CollectingInputs
    {
        public static class AddItemToCollection
        {
            public static readonly AddItemToCollectionInput Empty = With();

            public static AddItemToCollectionInput With(
                Guid? id = null,
                string owner = null,
                string catalogItem = null,
                DateTime? addedDate = null,
                decimal? price = null,
                string condition = null,
                string shop = null,
                string notes = null) =>
                new AddItemToCollectionInput(
                    id ?? Guid.Empty,
                    owner,
                    catalogItem,
                    addedDate ?? DateTime.MinValue,
                    price ?? 0M,
                    condition,
                    shop,
                    notes);
        }

        public static class AddItemToWishlist
        {
            public static readonly AddItemToWishlistInput Empty = With();

            public static AddItemToWishlistInput With(
                string owner = null,
                Guid? id = null,
                string catalogItem = null,
                decimal? price = null,
                DateTime? addedDate = null,
                string priority = null,
                string notes = null) =>
                new AddItemToWishlistInput(
                    owner,
                    id ?? Guid.Empty,
                    catalogItem,
                    addedDate ?? DateTime.Now,
                    price,
                    priority,
                    notes);
        }

        public static class AddShopToFavourites
        {
            public static readonly AddShopToFavouritesInput Empty = With();

            public static AddShopToFavouritesInput With() => null;
        }

        public static class CreateCollection
        {
            public static readonly CreateCollectionInput Empty = With();

            public static CreateCollectionInput With(string owner = null, string notes = null) =>
                new CreateCollectionInput(owner, notes);
        }

        public static class CreateShop
        {
            public static readonly CreateShopInput Empty = With();

            public static CreateShopInput With(
                string name = null, string websiteUrl = null,
                string emailAddress = null, ShopAddressInput shopAddress = null,
                string phoneNumber = null) =>
                new CreateShopInput(name, websiteUrl, emailAddress, shopAddress, phoneNumber);
        }

        public static class CreateWishlist
        {
            public static readonly CreateWishlistInput Empty = With();

            public static CreateWishlistInput With(string owner = null, string listName = null, string visibility = null) =>
                new CreateWishlistInput(owner, listName, visibility);
        }

        public static class DeleteWishlist
        {
            public static readonly DeleteWishlistInput Empty = With();

            public static DeleteWishlistInput With(Guid? id = null, string owner = null) =>
                new DeleteWishlistInput(owner, id ?? Guid.Empty);
        }

        public static class EditCollectionItem
        {
            public static readonly EditCollectionItemInput Empty = With();

            public static EditCollectionItemInput With(
                string owner = null,
                Guid? id = null, Guid? itemId = null,
                DateTime? addedDate = null,
                decimal? price = null,
                string condition = null,
                string shop = null,
                string notes = null) =>
                new EditCollectionItemInput(
                    owner,
                    id ?? Guid.Empty,
                    itemId ?? Guid.Empty,
                    addedDate ?? DateTime.MinValue,
                    price,
                    condition,
                    shop,
                    notes);
        }

        public static class EditWishlistItem
        {
            public static readonly EditWishlistItemInput Empty = With();

            public static EditWishlistItemInput With(
                string owner = null,
                Guid? id = null,
                Guid? itemId = null,
                DateTime? addedDate = null,
                decimal? price = null,
                string priority = null,
                string notes = null) => new EditWishlistItemInput(
                    owner,
                    id ?? Guid.Empty,
                    itemId ?? Guid.Empty,
                    addedDate,
                    price,
                    priority,
                    notes);
        }

        public static class RemoveItemFromCollection
        {
            public static readonly RemoveItemFromCollectionInput Empty = With();

            public static RemoveItemFromCollectionInput With(
                string owner = null,
                Guid? id = null,
                Guid? itemId = null,
                DateTime? removed = null,
                string notes = null) => new RemoveItemFromCollectionInput(
                    owner,
                    id ?? Guid.Empty,
                    itemId ?? Guid.Empty,
                    removed);
        }

        public static class RemoveItemFromWishlist
        {
            public static readonly RemoveItemFromWishlistInput Empty = With();

            public static RemoveItemFromWishlistInput With(
                string owner = null,
                Guid? id = null,
                Guid? itemId = null) => new RemoveItemFromWishlistInput(owner, id ?? Guid.Empty, itemId ?? Guid.Empty);
        }

        public static class RemoveShopFromFavourites
        {
            public static readonly RemoveShopFromFavouritesInput Empty = With();

            public static RemoveShopFromFavouritesInput With() => null;
        }

        public static class NewShopAddressInput
        {
            public static readonly ShopAddressInput Empty = With();

            public static ShopAddressInput With(
                string line1 = null,
                string line2 = null,
                string city = null,
                string region = null,
                string postalCode = null,
                string country = null) => new ShopAddressInput
                {
                    Line1 = line1,
                    Line2 = line2,
                    City = city,
                    Region = region,
                    PostalCode = postalCode,
                    Country = country
                };
        }
    }
}
