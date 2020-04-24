using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerRequest : IRequest
    {
        public string? Owner { set; get; }
        public string? Visibility { set; get; }
        public bool UserIsOwner { set; get; }
    }
}
