using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesInput : IUseCaseInput
    {
        public AddShopToFavouritesInput(string owner, Guid shopId)
        {
            Owner = owner;
            ShopId = shopId;
        }

        public string Owner { get; }
        public Guid ShopId { get; }
    }
}
