using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList
{
    public sealed class GetBrandsListHandler : UseCaseHandler<IGetBrandsListUseCase, GetBrandsListRequest, GetBrandsListInput>
    {
        public GetBrandsListHandler(IGetBrandsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
