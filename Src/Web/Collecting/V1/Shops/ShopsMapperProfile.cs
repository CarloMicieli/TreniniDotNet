using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites;
using TreniniDotNet.Web.Collecting.V1.Shops.Common.Requests;
using TreniniDotNet.Web.Collecting.V1.Shops.CreateShop;
using TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops;
using TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug;
using TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList;
using TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites;

namespace TreniniDotNet.Web.Collecting.V1.Shops
{
    public class ShopsMapperProfile : Profile
    {
        public ShopsMapperProfile()
        {
            CreateMap<AddShopToFavouritesRequest, AddShopToFavouritesInput>();
            CreateMap<CreateShopRequest, CreateShopInput>();
            CreateMap<GetShopBySlugRequest, GetShopBySlugInput>();
            CreateMap<GetShopsListRequest, GetShopsListInput>();
            CreateMap<GetFavouriteShopsRequest, GetFavouriteShopsInput>();
            CreateMap<RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesInput>();
            CreateMap<AddressRequest, ShopAddressInput>();
        }
    }
}