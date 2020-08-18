using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public sealed class CreateShopOutput : IUseCaseOutput
    {
        public CreateShopOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
