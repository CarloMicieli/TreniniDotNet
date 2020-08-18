using MediatR;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    public class GetShopBySlugRequest : IRequest
    {
        public GetShopBySlugRequest(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}