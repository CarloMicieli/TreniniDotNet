using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopBySlug
{
    public class GetShopBySlugHandler : UseCaseHandler<IGetShopBySlugUseCase, GetShopBySlugRequest, GetShopBySlugInput>
    {
        public GetShopBySlugHandler(IGetShopBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}