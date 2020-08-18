using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesInput : IUseCaseInput
    {
        public RemoveShopFromFavouritesInput(string owner, Guid shopId)
        {
            Owner = owner;
            ShopId = shopId;
        }

        public string Owner { get; }
        public Guid ShopId { get; }
    }
}
