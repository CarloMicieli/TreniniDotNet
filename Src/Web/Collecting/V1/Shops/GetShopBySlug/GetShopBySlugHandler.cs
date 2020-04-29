using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug
{
    public class GetShopBySlugHandler : UseCaseHandler<IGetShopBySlugUseCase, GetShopBySlugRequest, GetShopBySlugInput>
    {
        public GetShopBySlugHandler(IGetShopBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}