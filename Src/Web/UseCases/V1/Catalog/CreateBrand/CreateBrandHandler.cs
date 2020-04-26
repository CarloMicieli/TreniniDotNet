using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandHandler : UseCaseHandler<ICreateBrandUseCase, CreateBrandRequest, CreateBrandInput>
    {
        public CreateBrandHandler(ICreateBrandUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
