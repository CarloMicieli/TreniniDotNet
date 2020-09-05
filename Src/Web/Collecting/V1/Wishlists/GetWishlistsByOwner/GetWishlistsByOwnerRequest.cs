using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerRequest : IRequest
    {
        public string? Owner { set; get; }
        public string? Visibility { set; get; }
        public bool UserIsOwner { set; get; }
    }
}
