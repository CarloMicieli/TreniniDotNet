using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.EditBrand
{
    public sealed class EditBrandHandler : UseCaseHandler<IEditBrandUseCase, EditBrandRequest, EditBrandInput>
    {
        public EditBrandHandler(IEditBrandUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
