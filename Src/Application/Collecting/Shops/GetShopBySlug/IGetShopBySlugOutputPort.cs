using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public interface IGetShopBySlugOutputPort : IOutputPortStandard<GetShopBySlugOutput>
    {
        void ShopNotFound(Slug slug);
    }
}
