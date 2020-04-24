using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopsList
{
    public class GetShopsListHandler : UseCaseHandler<IGetShopsListUseCase, GetShopsListRequest, GetShopsListInput>
    {
        public GetShopsListHandler(IGetShopsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}