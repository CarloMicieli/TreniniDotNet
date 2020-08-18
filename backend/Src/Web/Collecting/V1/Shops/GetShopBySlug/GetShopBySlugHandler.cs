using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    public class GetShopBySlugHandler : UseCaseHandler<GetShopBySlugUseCase, GetShopBySlugRequest, GetShopBySlugInput>
    {
        public GetShopBySlugHandler(GetShopBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}