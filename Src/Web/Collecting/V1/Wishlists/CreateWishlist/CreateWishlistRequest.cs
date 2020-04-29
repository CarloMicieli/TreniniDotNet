using MediatR;
using Newtonsoft.Json;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { set; get; }

        public string? ListName { set; get; }

        public string? Visibility { set; get; }
    }
}
