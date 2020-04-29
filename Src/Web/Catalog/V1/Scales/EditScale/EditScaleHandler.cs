using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.EditScale
{
    public sealed class EditScaleHandler : UseCaseHandler<IEditScaleUseCase, EditScaleRequest, EditScaleInput>
    {
        public EditScaleHandler(IEditScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
