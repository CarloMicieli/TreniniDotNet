using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList
{
    public class GetShopsListHandler : UseCaseHandler<GetShopsListUseCase, GetShopsListRequest, GetShopsListInput>
    {
        public GetShopsListHandler(GetShopsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}