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

namespace TreniniDotNet.Application.TestInputs.Collecting
{
    public static class CollectingInputs
    {
        public static class AddItemToCollection
        {
            public static AddItemToCollectionInput Empty => With();

            public static AddItemToCollectionInput With(
                Guid? Id = null,
                string Owner = null,
                string CatalogItem = null,
                DateTime? AddedDate = null,
                decimal? Price = null,
                string Condition = null,
                string Shop = null,
                string Notes = null) =>
                new AddItemToCollectionInput(
                    Id ?? Guid.Empty,
                    Owner,
                    CatalogItem,
                    AddedDate ?? DateTime.MinValue,
                    Price ?? 0M,
                    Condition,
                    Shop,
                    Notes);
        }

        public static class AddItemToWishlist
        {
            public static AddItemToWishlistInput Empty => With();

            public static AddItemToWishlistInput With(
                string Owner = null,
                Guid? Id = null,
                string CatalogItem = null,
                decimal? Price = null,
                DateTime? AddedDate = null,
                string Priority = null,
                string Notes = null) =>
                new AddItemToWishlistInput(
                    Owner,
                    Id ?? Guid.Empty,
                    CatalogItem,
                    AddedDate ?? DateTime.Now,
                    Price,
                    Priority,
                    Notes);
        }

        public static class AddShopToFavourites
        {
            public static AddShopToFavouritesInput Empty => null;

            public static AddShopToFavouritesInput With() => null;
        }

        public static class CreateCollection
        {
            public static CreateCollectionInput Empty => With();

            public static CreateCollectionInput With(string Owner = null, string Notes = null) =>
                new CreateCollectionInput(Owner, Notes);
        }

        public static class CreateShop
        {
            public static CreateShopInput Empty => With();

            public static CreateShopInput With(
                string Name = null, string WebsiteUrl = null,
                string EmailAddress = null, ShopAddressInput shopAddress = null,
                string PhoneNumber = null) =>
                new CreateShopInput(Name, WebsiteUrl, EmailAddress, shopAddress, PhoneNumber);
        }

        public static class CreateWishlist
        {
            public static CreateWishlistInput Empty => With();

            public static CreateWishlistInput With(string Owner = null, string ListName = null, string Visibility = null) =>
                new CreateWishlistInput(Owner, ListName, Visibility);
        }

        public static class DeleteWishlist
        {
            public static DeleteWishlistInput Empty => With();

            public static DeleteWishlistInput With(Guid? Id = null, string Owner = null) =>
                new DeleteWishlistInput(Owner, Id ?? Guid.Empty);
        }

        public static class EditCollectionItem
        {
            public static EditCollectionItemInput Empty => With();

            public static EditCollectionItemInput With(
                string Owner = null,
                Guid? Id = null, Guid? ItemId = null,
                DateTime? AddedDate = null,
                decimal? Price = null,
                string Condition = null,
                string Shop = null,
                string Notes = null) =>
                new EditCollectionItemInput(
                    Owner,
                    Id ?? Guid.Empty,
                    ItemId ?? Guid.Empty,
                    AddedDate ?? DateTime.MinValue,
                    Price,
                    Condition,
                    Shop,
                    Notes);
        }

        public static class EditWishlistItem
        {
            public static EditWishlistItemInput Empty => With();

            public static EditWishlistItemInput With(
                string Owner = null,
                Guid? Id = null,
                Guid? ItemId = null,
                DateTime? AddedDate = null,
                decimal? Price = null,
                string Priority = null,
                string Notes = null) => new EditWishlistItemInput(
                    Owner,
                    Id ?? Guid.Empty,
                    ItemId ?? Guid.Empty,
                    AddedDate,
                    Price,
                    Priority,
                    Notes);
        }

        public static class RemoveItemFromCollection
        {
            public static RemoveItemFromCollectionInput Empty => With();

            public static RemoveItemFromCollectionInput With(
                string Owner = null,
                Guid? Id = null,
                Guid? ItemId = null,
                DateTime? Removed = null,
                string Notes = null) => new RemoveItemFromCollectionInput(
                    Owner,
                    Id ?? Guid.Empty,
                    ItemId ?? Guid.Empty,
                    Removed);
        }

        public static class RemoveItemFromWishlist
        {
            public static RemoveItemFromWishlistInput Empty => With();

            public static RemoveItemFromWishlistInput With(
                string Owner = null,
                Guid? Id = null,
                Guid? ItemId = null) => new RemoveItemFromWishlistInput(Owner, Id ?? Guid.Empty, ItemId ?? Guid.Empty);
        }

        public static class RemoveShopFromFavourites
        {
            public static RemoveShopFromFavouritesInput Empty => With();

            public static RemoveShopFromFavouritesInput With() => null;
        }

        public static class NewShopAddressInput
        {
            public static ShopAddressInput NewEmpty() => With();

            public static ShopAddressInput With(
                string Line1 = null,
                string Line2 = null,
                string City = null,
                string Region = null,
                string PostalCode = null,
                string Country = null) => new ShopAddressInput
                {
                    Line1 = Line1,
                    Line2 = Line2,
                    City = City,
                    Region = Region,
                    PostalCode = PostalCode,
                    Country = Country
                };
        }
    }
}
