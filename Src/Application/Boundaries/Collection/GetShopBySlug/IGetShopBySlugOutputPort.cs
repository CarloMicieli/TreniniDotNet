using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug
{
    public interface IGetShopBySlugOutputPort : IOutputPortStandard<GetShopBySlugOutput>
    {
        void ShopNotFound(Slug slug);
    }
}
