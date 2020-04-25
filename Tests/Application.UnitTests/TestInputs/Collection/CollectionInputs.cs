using System;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;
using TreniniDotNet.Application.Boundaries.Common;

namespace TreniniDotNet.Application.TestInputs.Collection
{
    public static class CollectionInputs
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
                string EmailAddress = null, AddressInput Address = null,
                string PhoneNumber = null) =>
                new CreateShopInput(Name, WebsiteUrl, EmailAddress, Address, PhoneNumber);
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
    }
}
