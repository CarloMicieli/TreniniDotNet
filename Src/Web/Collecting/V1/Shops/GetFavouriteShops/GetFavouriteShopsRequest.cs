using System.Text.Json.Serialization;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { set; get; }
    }
}
