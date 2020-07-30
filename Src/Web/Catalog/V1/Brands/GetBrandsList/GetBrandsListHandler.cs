using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList
{
    public sealed class GetBrandsListHandler : UseCaseHandler<GetBrandsListUseCase, GetBrandsListRequest, GetBrandsListInput>
    {
        public GetBrandsListHandler(GetBrandsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
