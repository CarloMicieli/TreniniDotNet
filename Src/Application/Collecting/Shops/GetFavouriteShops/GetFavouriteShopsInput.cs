using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsInput : IUseCaseInput
    {
        public GetFavouriteShopsInput(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; }
    }
}
