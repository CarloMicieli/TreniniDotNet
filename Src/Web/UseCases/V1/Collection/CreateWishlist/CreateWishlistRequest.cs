using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
{
    public sealed class CreateWishlistRequest : IRequest
    {
        public string? Owner { set; get; }

        public string? ListName { set; get; }

        public string? Visibility { set; get; }
    }
}
