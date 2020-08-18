using MediatR;
using System.Text.Json.Serialization;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { set; get; }

        public string? ListName { set; get; }

        public string? Visibility { set; get; }

        public BudgetRequest? Budget { set; get; }
    }
}
