using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.GetScalesList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScalesList
{
    public class GetScalesListHandler : UseCaseHandler<GetScalesListUseCase, GetScalesListRequest, GetScalesListInput>
    {
        public GetScalesListHandler(GetScalesListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
