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

namespace TreniniDotNet.Application.TestInputs.Collection
{
    public static class CollectionInputs
    {
        public static class AddItemToCollection
        {
            public static AddItemToCollectionInput Empty => null;

            public static AddItemToCollectionInput With() => null;
        }

        public static class AddItemToWishlist
        {
            public static AddItemToWishlistInput Empty => null;

            public static AddItemToWishlistInput With() => null;
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
                string EmailAddress = null, ShopAddressInput Address = null,
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
            public static DeleteWishlistInput Empty => null;

            public static DeleteWishlistInput With() => null;
        }

        public static class EditCollectionItem
        {
            public static EditCollectionItemInput Empty => null;

            public static EditCollectionItemInput With() => null;
        }

        public static class EditWishlistItem
        {
            public static EditWishlistItemInput Empty => null;

            public static EditWishlistItemInput With() => null;
        }

        public static class RemoveItemFromCollection
        {
            public static RemoveItemFromCollectionInput Empty => null;

            public static RemoveItemFromCollectionInput With() => null;
        }

        public static class RemoveItemFromWishlist
        {
            public static RemoveItemFromWishlistInput Empty => null;

            public static RemoveItemFromWishlistInput With() => null;
        }

        public static class RemoveShopFromFavourites
        {
            public static RemoveShopFromFavouritesInput Empty => null;

            public static RemoveShopFromFavouritesInput With() => null;
        }
    }
}
