using System;
using System.Text.Json.Serialization;
using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { set; get; }

        public Guid ShopId { set; get; }
    }
}
