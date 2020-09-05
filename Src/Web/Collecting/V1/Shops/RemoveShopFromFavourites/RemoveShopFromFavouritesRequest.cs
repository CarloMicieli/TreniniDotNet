using System;
using System.Text.Json.Serialization;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { set; get; }

        [JsonIgnore]
        public Guid? ShopId { set; get; }
    }
}
