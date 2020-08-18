using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public interface IGetShopBySlugOutputPort : IStandardOutputPort<GetShopBySlugOutput>
    {
        void ShopNotFound(Slug slug);
    }
}
