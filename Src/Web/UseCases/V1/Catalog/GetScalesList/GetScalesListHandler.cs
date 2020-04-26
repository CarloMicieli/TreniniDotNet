using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
{
    public class GetScalesListHandler : UseCaseHandler<IGetScalesListUseCase, GetScalesListRequest, GetScalesListInput>
    {
        public GetScalesListHandler(IGetScalesListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
