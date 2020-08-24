using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsOutput : IUseCaseOutput
    {
        public List<Shop> FavouriteShops { get; }

        public Owner Owner { get; }

        public GetFavouriteShopsOutput(Owner owner, List<Shop> favouriteShops)
        {
            Owner = owner;
            FavouriteShops = favouriteShops;
        }
    }
}
