using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopBySlug
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