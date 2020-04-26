using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    public sealed class GetBrandsListHandler : UseCaseHandler<IGetBrandsListUseCase, GetBrandsListRequest, GetBrandsListInput>
    {
        public GetBrandsListHandler(IGetBrandsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
