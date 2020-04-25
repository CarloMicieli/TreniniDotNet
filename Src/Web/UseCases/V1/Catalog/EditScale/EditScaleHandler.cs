using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditScale
{
    public sealed class EditScaleHandler : UseCaseHandler<IEditScaleUseCase, EditScaleRequest, EditScaleInput>
    {
        public EditScaleHandler(IEditScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
