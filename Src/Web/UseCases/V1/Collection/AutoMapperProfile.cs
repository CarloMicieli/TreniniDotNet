using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;
using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;
using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;
using TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection;
using TreniniDotNet.Web.UseCases.V1.Collection.AddShopToFavourites;
using TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection;
using TreniniDotNet.Web.UseCases.V1.Collection.CreateShop;
using TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist;
using TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist;
using TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem;
using TreniniDotNet.Web.UseCases.V1.Collection.EditWishlistItem;
using TreniniDotNet.Web.UseCases.V1.Collection.GetFavouriteShops;
using TreniniDotNet.Web.UseCases.V1.Collection.GetUserCollection;
using TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById;
using TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsList;
using TreniniDotNet.Web.UseCases.V1.Collection.RemoveShopFromFavourites;

namespace TreniniDotNet.Web.UseCases.V1.Collection
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddItemToCollectionRequest, AddItemToCollectionInput>();
            CreateMap<AddItemToWishlistRequest, AddItemToWishlistInput>();
            CreateMap<AddShopToFavouritesRequest, AddShopToFavouritesInput>();
            CreateMap<CreateCollectionRequest, CreateCollectionInput>();
            CreateMap<CreateShopRequest, CreateShopInput>();
            CreateMap<CreateWishlistRequest, CreateWishlistInput>();
            CreateMap<DeleteWishlistRequest, DeleteWishlistInput>();
            CreateMap<EditCollectionItemRequest, EditCollectionItemInput>();
            CreateMap<EditWishlistItemRequest, EditWishlistItemInput>();
            CreateMap<GetFavouriteShopsRequest, GetFavouriteShopsInput>();
            CreateMap<GetUserCollectionRequest, GetUserCollectionInput>();
            CreateMap<GetWishlistByIdRequest, GetWishlistByIdInput>();
            CreateMap<GetWishlistsListRequest, GetWishlistsListInput>();
            CreateMap<RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesInput>();
        }
    }
}
