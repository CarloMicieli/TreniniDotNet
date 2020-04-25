using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand
{
    public sealed class EditBrandHandler : UseCaseHandler<IEditBrandUseCase, EditBrandRequest, EditBrandInput>
    {
        public EditBrandHandler(IEditBrandUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
