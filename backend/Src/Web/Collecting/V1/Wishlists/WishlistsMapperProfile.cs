using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.Requests;
using TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem;
using TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById;
using TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists
{
    public class WishlistsMapperProfile : Profile
    {
        public WishlistsMapperProfile()
        {
            CreateMap<AddItemToWishlistRequest, AddItemToWishlistInput>();
            CreateMap<CreateWishlistRequest, CreateWishlistInput>();
            CreateMap<DeleteWishlistRequest, DeleteWishlistInput>();
            CreateMap<EditWishlistRequest, EditWishlistInput>();
            CreateMap<EditWishlistItemRequest, EditWishlistItemInput>();
            CreateMap<GetWishlistByIdRequest, GetWishlistByIdInput>();
            CreateMap<GetWishlistsByOwnerRequest, GetWishlistsByOwnerInput>();
            CreateMap<RemoveItemFromWishlistRequest, RemoveItemFromWishlistInput>();
            CreateMap<BudgetRequest, BudgetInput>();
        }
    }
}
